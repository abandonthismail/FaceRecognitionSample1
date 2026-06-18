using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FaceRecognitionSample1.Properties;
using FaceRecognitionSample1.Repositories;
using FaceRecognitionSample1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace FaceRecognitionSample1.ViewModels
{
    public partial class UserManagementViewModel : ObservableObject
    {
        private readonly IUserRepository _userRepository;
        private readonly IDialogService _dialogService;
        private readonly IDispatcherService _dispatcherService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<UserItemViewModel> Users { get; } = new();
        private List<UserItemViewModel> _allUsersSource = new();

        #region Search Condition Properties
        [ObservableProperty] private string _searchText = string.Empty;
        [ObservableProperty] private DateTime? _startDate = DateTime.Today.AddMonths(-1);
        [ObservableProperty] private DateTime? _endDate = DateTime.Today;
        #endregion

        #region State Properties
        [ObservableProperty] private bool _isLoading;
        [ObservableProperty] private bool _isAllSelected;
        private bool _isUpdatingSelectionLoop;
        #endregion

        public string DeleteButtonText => string.Format(Resources.DeleteButtonFormat, Users.Count(x => x.IsSelected));
        public string TotalCountText => string.Format(Resources.TotalCountFormat, Users.Count);

        /// <summary>
        /// Initializes a new instance of the UserManagementViewModel.
        /// </summary>
        /// <param name="userRepository">The repository for user data access.</param>
        /// <param name="dialogService">The service for displaying dialogs.</param>
        public UserManagementViewModel(
            IUserRepository userRepository,
            IDialogService dialogService,
            IDispatcherService dispatcherService,
            INavigationService navigationService)
        {
            _userRepository = userRepository;
            _dialogService = dialogService;
            _dispatcherService = dispatcherService;
            _navigationService = navigationService;

            _ = InitializeDataAsync();
        }

        private async Task InitializeDataAsync()
        {
            IsLoading = true;
            try
            {
                var entities = await _userRepository.GetAllAsync();

                _dispatcherService.Invoke(() =>
                {
                    _allUsersSource.Clear();
                    foreach (var entity in entities)
                    {
                        _allUsersSource.Add(new UserItemViewModel(OnItemSelectionChanged)
                        {
                            UserId = entity.UserId,
                            UserName = entity.UserName,
                            CreatedAt = entity.CreatedAt,
                        });
                    }
                    Search(); // UI update
                });
            }
            catch (Exception ex) // Added error handling
            {
                // In production, log the exception details here using a logging framework
                _dialogService.ShowError($"Failed to load user data.\n{ex.Message}", "Data Load Error");
            }
            finally
            {
                IsLoading = false;
            }
        }

        #region Selection Logic
        partial void OnIsAllSelectedChanged(bool value)
        {
            if (_isUpdatingSelectionLoop) return;

            _isUpdatingSelectionLoop = true;
            foreach (var user in Users)
            {
                user.IsSelected = value;
            }
            _isUpdatingSelectionLoop = false;

            UpdateDeleteButtonState();
        }

        private void OnItemSelectionChanged()
        {
            if (_isUpdatingSelectionLoop) return;

            _isUpdatingSelectionLoop = true;
            IsAllSelected = Users.Count > 0 && Users.All(x => x.IsSelected);
            _isUpdatingSelectionLoop = false;

            UpdateDeleteButtonState();
        }

        private void UpdateDeleteButtonState()
        {
            OnPropertyChanged(nameof(DeleteButtonText));
            DeleteSelectedUsersCommand.NotifyCanExecuteChanged();
        }
        #endregion

        #region Commands
        [RelayCommand]
        private void Search()
        {
            // Ensure UI bound collection is modified exclusively on the UI thread
            _dispatcherService.Invoke(() =>
            {
                Users.Clear();

                var filtered = ApplySearchFilters(_allUsersSource);

                foreach (var user in filtered)
                {
                    Users.Add(user);
                }

                UpdateDeleteButtonState();
                OnPropertyChanged(nameof(TotalCountText));
            });
        }

        [RelayCommand(CanExecute = nameof(CanDeleteSelected))]
        private async Task DeleteSelectedUsers()
        {
            var targetCount = Users.Count(x => x.IsSelected);
            var message = string.Format(Resources.DeleteConfirmMessageFormat, targetCount);
            var isConfirmed = _dialogService.ShowConfirmation(message, Resources.DeleteConfirmTitle);

            if (!isConfirmed) return;

            IsLoading = true;
            try
            {
                var targetIds = Users.Where(x => x.IsSelected).Select(x => x.UserId).ToList();
                await _userRepository.DeleteAsync(targetIds);

                _dispatcherService.Invoke(() =>
                {
                    _allUsersSource.RemoveAll(x => targetIds.Contains(x.UserId));
                    Search();
                });

                var successMessage = string.Format(Resources.DeleteCompleteMessageFormat, targetCount);
                _dialogService.ShowInformation(successMessage, Resources.CompleteTitle);
            }
            catch (Exception ex) // Added error handling for deletion
            {
                _dialogService.ShowError($"Failed to delete users.\n{ex.Message}", "Deletion Error");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void ClearSearch()
        {
            SearchText = string.Empty;
            StartDate = DateTime.Today.AddMonths(-1);
            EndDate = DateTime.Today;
            Search();
        }

        private bool CanDeleteSelected() => Users.Any(x => x.IsSelected);

        [RelayCommand]
        private void CreateUser() { /* Navigation logic */ }

        [RelayCommand]
        private void EditUser(UserItemViewModel targetUser) { /* Navigation logic */ }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Applies the current search criteria to the provided data source.
        /// </summary>
        /// <param name="source">The original list of users.</param>
        /// <returns>An enumerable of users matching the criteria.</returns>
        private IEnumerable<UserItemViewModel> ApplySearchFilters(IEnumerable<UserItemViewModel> source)
        {
            var filtered = source.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(u => u.UserId.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                               u.UserName.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            if (StartDate.HasValue)
            {
                filtered = filtered.Where(u => u.CreatedAt.Date >= StartDate.Value.Date);
            }

            if (EndDate.HasValue)
            {
                filtered = filtered.Where(u => u.CreatedAt.Date <= EndDate.Value.Date);
            }

            return filtered;
        }

        /// <summary>
        /// Command to navigate back to the main face recognition screen.
        /// </summary>
        [RelayCommand]
        private void GoBack()
        {
            // Navigates to the Home/FaceRecognition view model
            _navigationService.NavigateTo<FaceRecognitionViewModel>();
        }
        #endregion
    }
}