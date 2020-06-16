using ImageFiltersWPF.Models;
using ImageFiltersWPF.ViewModels.Enums;
using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.ViewModels.Services
{
    public class InOutService : IInOutService
    {
        private ILogger logger;
        private readonly IXmlManagmentService xmlManager;
        private readonly IImageFormatCheckerService imageFormarChecker;
        private readonly INotificationService notificationService;
        private const string editedPhotoPrefix = "Edited_";
        private const string dataXmlPrefix = "Data_";
        public string BasePath { get; set; }
        public string PhotosPath { get; set; }

        public InOutService(ILogger<InOutService> logger, IXmlManagmentService xmlManager, IImageFormatCheckerService imageFormarChecker, INotificationService notificationService)
        {
            this.logger = logger;
            this.xmlManager = xmlManager;
            this.imageFormarChecker = imageFormarChecker;
            this.notificationService = notificationService;
            BasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "ImageFiltersWPF");
            PhotosPath = Path.Combine(BasePath, "Photos");
        }


        public bool CopyFile(string sourcepath, string destinationPath)
        {
            logger.LogInformation($"CopyFile() | sourcePath: {sourcepath} | destinationPath: {destinationPath}");
            try
            {
                File.Copy(sourcepath, destinationPath, true);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }
        public byte[] BitmapSourceToByteArray(BitmapSource bitmapImage)
        {
            byte[] data;
            var encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

        public BitmapImage ByteArrayToBitmapImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
        public void CreateDirectory(string path)
        {
            logger.LogInformation($"CreateDirectory() path: {path}");
            Directory.CreateDirectory(path);
        }

        public IEnumerable<PhotoData> LoadPhotoData()
        {
            var folders = Directory.GetFileSystemEntries(PhotosPath);

            foreach (var photoFolder in folders)
            {
                PhotoData photoData;
                if (xmlManager.XmlDeserialize(Path.Combine(photoFolder, dataXmlPrefix + Path.GetFileName(photoFolder) + ".xml"), out photoData))
                    yield return photoData;
            }
        }

        public bool ImportImage(string sourcePath)
        {
            logger.LogInformation($"ImportPhoto()sourcePath: {sourcePath}");

            if (!File.Exists(sourcePath))
                return false;
            var extension = imageFormarChecker.GetImageExtensionType(Path.GetExtension(sourcePath));
            var destinationPath = Path.Combine(PhotosPath, Path.GetFileNameWithoutExtension(sourcePath));
            var fileName = Path.GetFileName(sourcePath);
            var originalfileNameWithPath = Path.Combine(destinationPath, fileName);
            var editedFileNameWithPath = Path.Combine(destinationPath, editedPhotoPrefix + fileName);
            var imageXmlDataPath = Path.Combine(destinationPath, dataXmlPrefix + Path.GetFileNameWithoutExtension(sourcePath) + ".xml");

            if (File.Exists(originalfileNameWithPath))
            {
                notificationService.ShowNotification(NotificationTypeEnum.Information, "Photo already added");
                return false;
            }

            logger.LogInformation($"ImportPhoto() originalfileNameWithPath: {originalfileNameWithPath}");
            logger.LogInformation($"ImportPhoto() editedFileNameWithPath: {editedFileNameWithPath}");

            CreateDirectory(destinationPath);

            if (CopyFile(sourcePath, originalfileNameWithPath) && CopyFile(sourcePath, editedFileNameWithPath))
            {
                xmlManager.XmlSerialize(new PhotoData()
                {
                    PhotoName = Path.GetFileNameWithoutExtension(sourcePath),
                    ImageFormat = extension,
                    OriginalPhotoPath = originalfileNameWithPath,
                    CurrentPhotoPath = editedFileNameWithPath,
                    ImageDataXmlPath = imageXmlDataPath,
                    DirectoryPath = destinationPath
                }, imageXmlDataPath);
                return true;
            }
            else
                return false;
        }

        public bool ExportImage(PhotoViewModel photoToExport)
        {
            if (!File.Exists(photoToExport.PhotoData.OriginalPhotoPath))
            {
                notificationService.ShowNotification(NotificationTypeEnum.Error, "Cannot save image, data is missing");
                return false;
            }
            if (File.Exists(photoToExport.PhotoData.CurrentPhotoPath))
                File.Delete(photoToExport.PhotoData.CurrentPhotoPath);

            if (File.Exists(photoToExport.PhotoData.ImageDataXmlPath))
                File.Delete(photoToExport.PhotoData.ImageDataXmlPath);

            using (var fileStream = new FileStream(photoToExport.PhotoData.CurrentPhotoPath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(photoToExport.CurrentImage));
                encoder.Save(fileStream);
            }
            xmlManager.XmlSerialize(photoToExport.PhotoData, photoToExport.PhotoData.ImageDataXmlPath);
            notificationService.ShowNotification(NotificationTypeEnum.Information, "Saved photo!");
            return true;
        }

        public bool DeleteImage(PhotoData imageData)
        {
            if (File.Exists(imageData.OriginalPhotoPath))
            {
                Directory.Delete(imageData.DirectoryPath, true);
                return true;
            }
            return false;
        }

        public BitmapImage LoadImage(string sourcePath)
        {
            var uriSource = new Uri(sourcePath);
            var imgTemp = new BitmapImage();
            imgTemp.BeginInit();
            imgTemp.CacheOption = BitmapCacheOption.OnLoad;
            imgTemp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            imgTemp.UriSource = uriSource;
            imgTemp.EndInit();

            return imgTemp;
        }
    }
}