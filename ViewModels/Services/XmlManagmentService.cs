using ImageFiltersWPF.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFiltersWPF.ViewModels.Services
{
    public class XmlManagmentService : IXmlManagmentService
    {
        public bool XmlDeserialize<T>(T model, string sourcePath)
        {
            throw new NotImplementedException();
        }

        public bool XmlSerialize<T>(T model, string destinationPath)
        {
            throw new NotImplementedException();
        }
    }
}
