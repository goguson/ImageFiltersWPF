using ImageFiltersWPF.Models;
using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageFiltersWPF.ViewModels.Services
{
    public class InOutService : IInOutService
    {
        ILogger logger;
        private readonly IXmlManagmentService xmlManager;
        private readonly IImageFormatCheckerService imageFormarChecker;
        private const string editedPhotoPrefix = "Edited_";
        private const string dataXmlPrefix = "Data_";
        public string BasePath { get; set; }
        public string PhotosPath { get; set; }

        public InOutService(ILogger<InOutService> logger, IXmlManagmentService xmlManager, IImageFormatCheckerService imageFormarChecker)
        {
            this.logger = logger;
            this.xmlManager = xmlManager;
            this.imageFormarChecker = imageFormarChecker;
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
                return false; // msgb image already added

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
                    CurrentPhotoPath = editedFileNameWithPath
                }, imageXmlDataPath); ;
                return true;
            }
            else
                return false;

        }
    }
}
