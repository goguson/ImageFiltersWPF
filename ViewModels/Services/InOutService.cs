using ImageFiltersWPF.Models;
using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageFiltersWPF.ViewModels.Services
{
    public class InOutService : I_InOutService
    {
        ILogger logger;
        private readonly IXmlManagmentService xmlManager;

        public string BasePath { get; set; }
        public string PhotosPath { get; set; }

        public InOutService(ILogger<InOutService> logger)
        {
            this.logger = logger;
            this.xmlManager = xmlManager;
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

        public bool ImportImage(string sourcePath)
        {
            logger.LogInformation($"ImportPhoto()sourcePath: {sourcePath}");

            if (!File.Exists(sourcePath))
                return false;

            var destinationPath = Path.Combine(PhotosPath, Path.GetFileNameWithoutExtension(sourcePath));
            var fileName = Path.GetFileName(sourcePath);
            var originalfileNameWithPath = Path.Combine(destinationPath, fileName);
            var editedFileNameWithPath = Path.Combine(destinationPath, "edited_" + fileName);
            var imageXmlDataPath = Path.Combine(destinationPath, "data_" + fileName);
            logger.LogInformation($"ImportPhoto() originalfileNameWithPath: {originalfileNameWithPath}");
            logger.LogInformation($"ImportPhoto() editedFileNameWithPath: {editedFileNameWithPath}");

            CreateDirectory(destinationPath);

            if (CopyFile(sourcePath, originalfileNameWithPath) && CopyFile(sourcePath, editedFileNameWithPath))
            {
                return true;
            }
            else
                return false;
            
        }
    }
}
