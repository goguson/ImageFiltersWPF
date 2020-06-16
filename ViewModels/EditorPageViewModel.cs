using ImageFiltersWPF.Models;
using ImageFiltersWPF.ViewModels.Interfaces;
using ImageFiltersWPF.ViewModels.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;

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
            set { gaussFilterParams = value; OnPropertyChanged(nameof(GaussFilterParams)); }
        }
        private BinarizationFilterParams binarizationFilterParams;

        public BinarizationFilterParams BinarizationFilterParams
        {
            get { return binarizationFilterParams; }
            set { binarizationFilterParams = value; OnPropertyChanged(nameof(BinarizationFilterParams)); }
        }
        private bool serverModeEnabled;

        public bool ServerModeEnabled
        {
            get { return serverModeEnabled; }
            set { serverModeEnabled = value; OnPropertyChanged(nameof(ServerModeEnabled)); }
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
            BinarizationFilterParams = new BinarizationFilterParams()
            {
                RedParameter = 0.3f,
                GreenParameter = 0.6f,
                BlueParameter = 0.11f,
                PrecisionParameter = 6,
                AdjustmentParameter = 0.15f
            };
            CurrentFilters = new ObservableCollection<FilterParamsBase>();
            InitializeCommands();
            NavigationService = navigationService;
            this.ioService = ioService;
        }

        private void InitializeCommands()
        {
            AddGaussFilterCommand = new RelayCommand(async x =>
            {
                var filter = GaussFilterParams.Clone() as GaussFilterParams;
                CurrentFilters.Add(filter);
                EditedImage.PhotoData.CurrentFilters.Add(filter);
                if (ServerModeEnabled)
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                        var transferObject = new GaussTransferModel()
                        {
                            ImageByteArray = ioService.BitmapSourceToByteArray(EditedImage.CurrentImage),
                            Parameters = filter
                        };
                        var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44337/weatherforecast/GetGauss");
                        request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "application/json");
                        var response = await client.SendAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();
                            var resultImg = JsonConvert.DeserializeObject<GaussTransferModel>(jsonString);
                            EditedImage.CurrentImage = ioService.ByteArrayToBitmapImage(resultImg.ImageByteArray);
                        }
                    }
                }
                else

                    EditedImage.CurrentImage = imageFilterService.ApplyFilter(EditedImage.CurrentImage, filter);
            });

            AddBinarizationFilterCommand = new RelayCommand(async x =>
            {
                var filter = BinarizationFilterParams.Clone() as BinarizationFilterParams;
                CurrentFilters.Add(filter);
                EditedImage.PhotoData.CurrentFilters.Add(filter);

                if (ServerModeEnabled)
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                        var transferObject = new BinaryTransferModel()
                        {
                            ImageByteArray = ioService.BitmapSourceToByteArray(EditedImage.CurrentImage),
                            Parameters = filter
                        };
                        var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44337/weatherforecast/GetBinarization");
                        request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "application/json");
                        var response = await client.SendAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();
                            var resultImg = JsonConvert.DeserializeObject<BinaryTransferModel>(jsonString);
                            EditedImage.CurrentImage = ioService.ByteArrayToBitmapImage(resultImg.ImageByteArray);
                        }
                    }
                }
                else
                    EditedImage.CurrentImage = imageFilterService.ApplyFilter(EditedImage.CurrentImage, filter);
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