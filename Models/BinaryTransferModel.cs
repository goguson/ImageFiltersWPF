using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFiltersWPF.Models
{
    /// <summary>
    /// Class is used for transfering filtera and image data between client and server
    /// </summary>
    public class BinaryTransferModel
    {
        public byte[] ImageByteArray { get; set; }
        public BinarizationFilterParams Parameters { get; set; }
    }
}
