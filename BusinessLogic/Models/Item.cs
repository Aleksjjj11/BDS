using System.ComponentModel;
using System.Runtime.CompilerServices;
using BusinessLogic.Annotations;

namespace BusinessLogic.Models
{
    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private long _valueFirstParameter;
        public long ValueFirstParameter
        {
            get => _valueFirstParameter;
            set
            {
                _valueFirstParameter = value;
                OnPropertyChanged();
            }
        }
        private long _valueSecondParameter;
        public long ValueSecondParameter
        {
            get => _valueSecondParameter;
            set
            {
                _valueSecondParameter = value;
                OnPropertyChanged();
            }
        }
        private long _valueThirdParameter;
        public long ValueThirdParameter
        {
            get => _valueThirdParameter;
            set
            {
                _valueThirdParameter = value;
                OnPropertyChanged();
            }
        }
    }
}
