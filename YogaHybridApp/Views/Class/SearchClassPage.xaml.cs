using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using YogaHybridApp.Database;
using YogaHybridApp.Objects;

namespace YogaHybridApp.Views.Class
{
    public partial class SearchClassPage : ContentPage
    {
        private readonly DatabaseConnect _database;
        private List<ClassInstanceViewModel> allClassViewModels;
        public ObservableCollection<ClassInstanceViewModel> FilteredClassInstances { get; set; }
        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                OnPropertyChanged(nameof(IsDatePickerEnabled));
            }
        }
        public bool IsDatePickerEnabled => true; // Always enabled, but can be customized
        public ICommand NavigateToClassDetailsCommand { get; }

        public SearchClassPage()
        {
            InitializeComponent();
            _database = new DatabaseConnect();
            FilteredClassInstances = new ObservableCollection<ClassInstanceViewModel>();
            allClassViewModels = new List<ClassInstanceViewModel>();
            SelectedDate = null; // Initially null to show all classes
            NavigateToClassDetailsCommand = new Command<ClassInstanceViewModel>(async (vm) => await Navigation.PushAsync(new ClassDetailsPage(vm)));
            BindingContext = this;
            LoadAllClasses();
        }

        private async void LoadAllClasses()
        {
            try
            {
                //System.Diagnostics.Debug.WriteLine("Debugging Firebase data...");
                //await _database.DebugClassInstancesAsync(); // Debug raw data
                System.Diagnostics.Debug.WriteLine("Loading all class instances...");
                var classInstances = await _database.GetAllClassInstancesAsync();
                System.Diagnostics.Debug.WriteLine($"Fetched {classInstances?.Count ?? 0} class instances.");

                if (classInstances == null || !classInstances.Any())
                {
                    System.Diagnostics.Debug.WriteLine("No class instances found in Firebase.");
                    await DisplayAlert("Info", "No classes found in the database.", "OK");
                    return;
                }

                allClassViewModels.Clear();
                foreach (var classInstance in classInstances)
                {
                    System.Diagnostics.Debug.WriteLine($"Processing class: {classInstance.ClassInstanceId}, Date: {classInstance.DateTime:MM/dd/yyyy HH:mm}");
                    var teacher = await _database.GetTeacherByIdAsync(classInstance.TeacherId);
                    var viewModel = new ClassInstanceViewModel
                    {
                        ClassInstanceId = classInstance.ClassInstanceId,
                        CourseId = classInstance.CourseId,
                        Date = classInstance.DateTime,
                        TeacherName = teacher?.Name ?? "Unknown Teacher",
                        Comment = classInstance.Comment,
                        Teacher = teacher,
                        Course = null // Course not needed
                    };
                    allClassViewModels.Add(viewModel);
                }

                System.Diagnostics.Debug.WriteLine($"Loaded {allClassViewModels.Count} view models.");
                UpdateFilteredClasses();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading classes: {ex}");
                await DisplayAlert("Error", $"Failed to load classes: {ex.Message}", "OK");
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(SelectedDate))
            {
                System.Diagnostics.Debug.WriteLine($"Filter changed: Date={SelectedDate?.ToString("MM/dd/yyyy") ?? "null"}");
                UpdateFilteredClasses();
            }
        }

        private void UpdateFilteredClasses()
        {
            System.Diagnostics.Debug.WriteLine($"Filtering classes: Date={SelectedDate?.ToString("MM/dd/yyyy") ?? "null"}");
            var filtered = allClassViewModels.Where(vm =>
            {
                bool dateMatch = !SelectedDate.HasValue || vm.Date.Date == SelectedDate.Value.Date;
                System.Diagnostics.Debug.WriteLine($"Class {vm.ClassInstanceId}: DateMatch={dateMatch}, ClassDate={vm.Date:MM/dd/yyyy}");
                return dateMatch;
            }).ToList();

            System.Diagnostics.Debug.WriteLine($"Filtered {filtered.Count} classes.");
            FilteredClassInstances.Clear();
            foreach (var vm in filtered)
            {
                FilteredClassInstances.Add(vm);
            }

            if (!FilteredClassInstances.Any())
            {
                System.Diagnostics.Debug.WriteLine("No classes match the filter criteria.");
            }
        }
    }
}