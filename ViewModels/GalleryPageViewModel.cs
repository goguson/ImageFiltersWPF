using ImageFiltersWPF.ViewModels.Enums;
using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ImageFiltersWPF.ViewModels
{
    public class GalleryPageViewModel : INotifyPropertyChanged
    {
        private readonly ILogger<GalleryPageViewModel> logger;
        private readonly INavigationService navigationService;
        private readonly IInOutService inOutService;
        private readonly IPhotoViewModelFactory photoViewModelFactory;
        private readonly INotificationService notificationService;

        public event PropertyChangedEventHandler PropertyChanged;

        private PhotoViewModel selectedPhoto;

        public PhotoViewModel SelectedPhoto
        {
            get { return selectedPhoto; }
            set { selectedPhoto = value; OnPropertyChanged(nameof(SelectedPhoto)); }
        }

        private ObservableCollection<PhotoViewModel> photos;


        public ObservableCollection<PhotoViewModel> Photos
        {
            get { return photos; }
            set { photos = value; }
        }


        public RelayCommand AddNewImageCommand { get; set; }
        public RelayCommand EditImageCommand { get; set; }
        public RelayCommand DeleteImageCommand { get; set; }
        public RelayCommand OnLoad { get; set; }

        public GalleryPageViewModel(ILogger<GalleryPageViewModel> logger, INavigationService navigationService, IInOutService inOutService, IPhotoViewModelFactory photoViewModelFactory, INotificationService notificationService)
        {
            this.logger = logger;
            this.navigationService = navigationService;
            this.inOutService = inOutService;
            this.photoViewModelFactory = photoViewModelFactory;
            this.notificationService = notificationService;
            Photos = new ObservableCollection<PhotoViewModel>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            AddNewImageCommand = new RelayCommand((o) =>
            {
                OpenFileDialog dialog_window = new OpenFileDialog();
                dialog_window.Filter = "Image files|*.bmp;*.jpg;*.png;| PNG files|*.png|JPEG files|*.jpg";
                dialog_window.FilterIndex = 0;
                dialog_window.DefaultExt = "png";
                if (dialog_window.ShowDialog() != true)
                {
                    notificationService.ShowNotification(NotificationTypeEnum.Error, " Could not select photo");
                }
                var sourceFilePath = dialog_window.FileName;
                inOutService.ImportImage(sourceFilePath);
                RefreshImageList();
            });

            EditImageCommand = new RelayCommand((o) =>
            {
                navigationService.MoveToPage(PageEnum.editorPage, selectedPhoto.Clone() as PhotoViewModel);
            });

            OnLoad = new RelayCommand((o) =>
            {
                LoadImageList();
            });
            DeleteImageCommand = new RelayCommand((o) =>
            {
                if (inOutService.DeleteImage(SelectedPhoto.PhotoData))
                {
                    SelectedPhoto = null;
                    RefreshImageList();
                    notificationService.ShowNotification(NotificationTypeEnum.Information, "Deleted photo!");
                }
                notificationService.ShowNotification(NotificationTypeEnum.Error, "Error while deleting photo!");
            });
        }
        private void LoadImageList()
        {
            Photos.Clear();
            var data = inOutService.LoadPhotoData();
            foreach (var photoData in data)
                Photos.Add(photoViewModelFactory.CreatePhotoViewModel(photoData));
        }
        private void RefreshImageList()
        {
            LoadImageList();
        }
        private void OnPropertyChanged(string propertyName)
        {
            logger.LogInformation($"OnPropertyChanged({propertyName})");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
