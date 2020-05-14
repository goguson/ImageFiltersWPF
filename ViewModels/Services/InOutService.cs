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
        public string BasePath { get; set; }
        public InOutService(ILogger logger)
        {
            this.logger = logger;
        }
        public bool CopyFile(string path)
        {
            throw new NotImplementedException();
        }

        public bool CreateDirectory(string path)
        {
            if (Directory.Exists(path))
                return true;
        }

        public bool RemoveDirectory(string path)
        {
            throw new NotImplementedException();
        }

        public bool RemoveDirectoryWithFiles(string path)
        {
            throw new NotImplementedException();
        }
    }
}
