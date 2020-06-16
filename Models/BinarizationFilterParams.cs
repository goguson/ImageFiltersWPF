using System;
using System.ComponentModel;

namespace ImageFiltersWPF.Models
{
    public class BinarizationFilterParams : FilterParamsBase, INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BinarizationFilterParams()
        {
            FilterName = "Binarization filter";
        }
        private float redParameter;
        public float RedParameter
        {
            get { return redParameter; }
            set
            {

                if (value >= 0.1 && value <= 1)
                {
                    redParameter = value;
                    OnPropertyChanged("RedParameter");
                }
                else
                {
                    redParameter = RedParameter_Default;
                    OnPropertyChanged("RedParameter");
                }

            }
        }

        public float RedParameter_Default
        {
            get { return 0.3f; }

        }
        private float greenParameter;

        public float GreenParameter
        {
            get { return greenParameter; }
            set
            {
                if (value >= 0.1 && value <= 1)
                {
                    greenParameter = value;
                    OnPropertyChanged("GreenParameter");
                }
                else
                {
                    greenParameter = GreenParameter_Default;
                    OnPropertyChanged("GreenParameter");
                }
            }
        }

        public float GreenParameter_Default
        {
            get { return 0.6f; }

        }
        private float blueParameter;

        public float BlueParameter
        {
            get { return blueParameter; }
            set
            {
                if (value >= 0.1 && value <= 1)
                {
                    blueParameter = value;
                    OnPropertyChanged("BlueParameter");
                }
                else
                {
                    blueParameter = BlueParameter_Default;
                    OnPropertyChanged("BlueParameter");
                }
            }
        }

        public float BlueParameter_Default
        {
            get { return 0.11f; }

        }
        private int precisionParameter;

        public int PrecisionParameter
        {
            get { return precisionParameter; }
            set
            {
                if (value >= 1 && value <= 96)
                {
                    precisionParameter = value;
                    OnPropertyChanged("PrecisionParameter");
                }
                else
                {
                    precisionParameter = PrecisionParameter_Default;
                    OnPropertyChanged("PrecisionParameter");
                }
            }
        }

        public int PrecisionParameter_Default
        {
            get { return 6; }

        }
        private float adjustmentParameter;



        public float AdjustmentParameter
        {
            get { return adjustmentParameter; }
            set
            {
                if (value >= 0.1 && value <= 80)
                {
                    adjustmentParameter = value;
                    OnPropertyChanged("AdjustmentParameter");
                }
                else
                {
                    adjustmentParameter = AdjustmentParameter_Default;
                    OnPropertyChanged("AdjustmentParameter");
                }
            }
        }

        public float AdjustmentParameter_Default
        {
            get { return 0.15f; }

        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

}