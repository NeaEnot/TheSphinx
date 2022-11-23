using System.ComponentModel;
using System.Runtime.CompilerServices;
using TheSphinx.Core.Models;

namespace GUI.ViewModels
{
    internal class FieldViewModel : INotifyPropertyChanged
    {
        public Field Field { get; private set; }

        private string key;
        public string Key
        {
            get => key;
            set
            {
                key = value;
                OnPropertyChanged("Key");
            }
        }

        public string Value
        {
            get => Field.Value;
            set
            {
                Field.Value = value;
                OnPropertyChanged("Value");
            }
        }

        public bool Encrypted
        {
            get => Field.Encrypted;
            set
            {
                Field.Encrypted = value;
                OnPropertyChanged("Encrypted");
            }
        }

        internal FieldViewModel(Field field, string key)
        {
            Field = field;
            this.key = key;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
