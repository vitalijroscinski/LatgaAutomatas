using LatgaAutomatasLibrary.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LatgaAutomatas.Models
{
    public class BillAmount : INotifyPropertyChanged
    {
        private Bill _bill = null!;
        private int _amount;

        public required Bill Bill
        {
            get => _bill;
            set
            {
                if (_bill == value)
                    return;

                _bill = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get => _amount;
            set
            {
                if (_amount == value)
                    return;

                _amount = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
