using System;
using System.ComponentModel;

namespace ImageFiltersWPF.Models
{
    public class GaussFilterParams : FilterParamsBase, INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private float leftTop;

        public float LeftTop
        {
            get { return leftTop; }
            set
            {
                if (value >= 0 && value <= 1)
                    leftTop = value;
                else
                    leftTop = 0;
                OnPropertyChanged(nameof(LeftTop));

            }
        }
        public GaussFilterParams()
        {
            FilterName = "Gauss filter";
        }

        private float midTop;

        public float MidTop
        {
            get { return midTop; }
            set
            {
                if (value >= 0 && value <= 1)
                    midTop = value;
                else
                    midTop = 0.2f;
                OnPropertyChanged(nameof(MidTop));
            }
        }

        private float rightTop;

        public float RightTop
        {
            get { return rightTop; }
            set
            {
                if (value >= 0 && value <= 1)
                    rightTop = value;
                else
                    rightTop = 0;
                OnPropertyChanged(nameof(RightTop));
            }
        }

        private float leftMid;

        public float LeftMid
        {
            get { return leftMid; }
            set
            {
                if (value >= 0 && value <= 1)
                    leftMid = value;
                else
                    leftMid = 0.2f;
                OnPropertyChanged(nameof(LeftMid));
            }
        }

        private float mid;

        public float Mid
        {
            get { return mid; }
            set
            {
                if (value >= 0 && value <= 1)
                    mid = value;
                else
                    mid = 0.2f;
                OnPropertyChanged(nameof(Mid));
            }
        }

        private float rightMid;

        public float RightMid
        {
            get { return rightMid; }
            set
            {
                if (value >= 0 && value <= 1)
                    rightMid = value;
                else
                    rightMid = 0.2f;
                OnPropertyChanged(nameof(RightMid));
            }
        }

        private float leftBot;

        public float LeftBot
        {
            get { return leftBot; }
            set
            {
                if (value >= 0 && value <= 1)
                    leftBot = value;
                else
                    leftBot = 0;
                OnPropertyChanged(nameof(LeftBot));
            }
        }

        private float midBot;

        public float MidBot
        {
            get { return midBot; }
            set
            {
                if (value >= 0 && value <= 1)
                    midBot = value;
                else
                    midBot = 0.2f;
                OnPropertyChanged(nameof(MidBot));
            }
        }

        private float rightBot;



        public float RightBot
        {
            get { return rightBot; }
            set
            {
                if (value >= 0 && value <= 1)
                    rightBot = value;
                else
                    rightBot = 0;
                OnPropertyChanged(nameof(RightBot));
            }
        }

        public float KernelSum
        {
            get { return LeftTop + LeftMid + LeftBot + RightTop + RightMid + RightBot + MidTop + Mid + MidBot; }

        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

