using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFiltersWPF.Models
{
    public class BinaryTransferModel
    {
        public byte[] ImageByteArray { get; set; }
        public BinarizationFilterParams Parameters { get; set; }
    }
}
