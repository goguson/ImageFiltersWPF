using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFiltersWPF.Models
{
    public class GaussTransferModel
    {
        public byte[] ImageByteArray { get; set; }
        public GaussFilterParams Parameters { get; set; }
    }
}
