using ImageFiltersWPF.Models;
using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ImageFiltersWPF.ViewModels
{
    public class EditorPageViewModel : INotifyPropertyChanged, IParameters
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ILogger<EditorPageViewModel> logger;
        private readonly IImageFilterService imageFilterService;
        private FilterParamsBase filterParamsBase;

        public FilterParamsBase SelectedFilter
        {
            get { return filterParamsBase; }
            set { filterParamsBase = value; OnPropertyChanged(nameof(SelectedFilter)); }
        }

        private ObservableCollection<FilterParamsBase> currentFilters;

        public ObservableCollection<FilterParamsBase> CurrentFilters
        {
            get { return currentFilters; }
            set { currentFilters = value; OnPropertyChanged(nameof(CurrentFilters)); }
        }

        private PhotoViewModel editedImage;

        public PhotoViewModel EditedImage
        {
            get { return editedImage; }
            set
            {
                editedImage = value;
                OnPropertyChanged(nameof(EditedImage));
            }
        }

        private GaussFilterParams gaussFilterParams;
        private readonly IInOutService ioService;

        public GaussFilterParams GaussFilterParams
        {
            get { return gaussFilterParams; }
            set { gaussFilterParams = value; }
        }

        public RelayCommand AddGaussFilterCommand { get; set; }
        public RelayCommand AddBinarizationFilterCommand { get; set; }
        public RelayCommand SavePhotoCommand { get; set; }
        public RelayCommand CancelPhotoCommand { get; set; }
        public RelayCommand RemoveSelectedFilter { get; set; }
        public INavigationService NavigationService { get; }

        public EditorPageViewModel(ILogger<EditorPageViewModel> logger, IImageFilterService imageFilterService, INavigationService navigationService, IInOutService ioService)
        {
            this.logger = logger;
            this.imageFilterService = imageFilterService;
            GaussFilterParams = new GaussFilterParams()
            {
                Mid = 0.2f,
                MidTop = 0.2f,
                LeftMid = 0.2f,
                RightMid = 0.2f,
                MidBot = 0.2f
            };
            CurrentFilters = new ObservableCollection<FilterParamsBase>();
            InitializeCommands();
            NavigationService = navigationService;
            this.ioService = ioService;
        }

        private void InitializeCommands()
        {
            AddGaussFilterCommand = new RelayCommand(x =>
            {
                var filter = GaussFilterParams.Clone() as GaussFilterParams;
                CurrentFilters.Add(filter);
                EditedImage.PhotoData.CurrentFilters.Add(filter);

                EditedImage.CurrentImage = imageFilterService.ApplyFilter(EditedImage.CurrentImage, filter);
            });

            AddBinarizationFilterCommand = new RelayCommand(x =>
            {
            });

            SavePhotoCommand = new RelayCommand(x =>
            {
                ioService.ExportImage(editedImage);
                NavigationService.MoveToPage(Enums.PageEnum.galleryPage);
            });

            CancelPhotoCommand = new RelayCommand(x =>
            {
                NavigationService.MoveToPage(Enums.PageEnum.galleryPage);
            });
            RemoveSelectedFilter = new RelayCommand(x =>
            {
                if (SelectedFilter == null)
                    return;
                var filterToRemove = SelectedFilter;
                CurrentFilters.Remove(filterToRemove);
                EditedImage.PhotoData.CurrentFilters.Remove(filterToRemove);
                imageFilterService.ReApplyFilters(editedImage);
                SelectedFilter = CurrentFilters.FirstOrDefault();
            });
        }

        private void OnPropertyChanged(string propertyName)
        {
            logger.LogInformation($"OnPropertyChanged({propertyName})");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Activate(object parameter)
        {
            if (parameter is PhotoViewModel)
            {
                EditedImage = parameter as PhotoViewModel;
                foreach (var filter in EditedImage.PhotoData.CurrentFilters)
                {
                    CurrentFilters.Add(filter);
                }
            }
        }
    }
}