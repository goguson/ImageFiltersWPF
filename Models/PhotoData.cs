using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.Models
{
    public class PhotoData
    {
		private BitmapSource image;

		public BitmapSource Image
		{
			get { return image; }
			set { image = value; }
		}

		public List<string> CurrentFilters { get; set; }

		private string sourcePath;

		public string SourcePath
		{
			get { return sourcePath; }
			set { sourcePath = value; }
		}


	}
}
