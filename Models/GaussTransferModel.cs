using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFiltersWPF.Models
{
    /// <summary>
    /// class used for trasnfering image and fitler data between client and server
    /// </summary>
    public class GaussTransferModel
    {///
        public byte[] ImageByteArray { get; set; }
        public GaussFilterParams Parameters { get; set; }
    }
}
