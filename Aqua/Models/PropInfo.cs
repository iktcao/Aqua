using GalaSoft.MvvmLight;

namespace Aqua.Models
{
    public class PropInfo : ObservableObject
    {
        public string NameCN { get; set; }
        public string NameEN { get; set; }
        public string Unit { get; set; }
        public string Symbol { get; set; }
        public string Code { get; set; }

        private string _value;

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                this.RaisePropertyChanged("Value");
            }
        }
    }
}
