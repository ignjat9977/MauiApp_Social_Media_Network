using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Common
{
    public class MProp<T> : INotifyPropertyChanged
    {
        private T _value;
        private string _error;
        private Action _onValueChanged;

        public T Value 
        {
            get => _value;

            set
            {
                _value = value;
                NotifyPropertyChanged();
                _onValueChanged?.Invoke();

            }
        }
        public bool HasError => !string.IsNullOrEmpty(_error);
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(HasError));

            }
        }
        public Action OnValueChanged { set => _onValueChanged = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
