using System.ComponentModel;
using System.Xml.Serialization;

namespace ImageFiltersWPF.Models
{
    [XmlInclude(typeof(GaussFilterParams))]
    public abstract class FilterParamsBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string filterName;

        public string FilterName
        {
            get { return filterName; }
            set { filterName = value; OnPropertyChanged(nameof(FilterName)); }
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
