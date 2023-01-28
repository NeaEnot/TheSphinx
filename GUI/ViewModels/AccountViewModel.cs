using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TheSphinx.Core.Models;

namespace GUI.ViewModels
{
    internal class AccountViewModel : INotifyPropertyChanged
    {
        private Account account;

        public string Source
        {
            get => account.Source;
            set
            {
                account.Source = value;
                OnPropertyChanged("Source");
            }
        }

        public IEnumerable<FieldViewModel> Fields { get; set; }

        internal AccountViewModel(Account account)
        {
            this.account = account;
            Fields = new List<FieldViewModel>();

            foreach (KeyValuePair<string, Field> field in account.Fields)
                (Fields as List<FieldViewModel>).Add(new FieldViewModel(field.Value, field.Key));
        }

        internal void AddField()
        {
            (Fields as List<FieldViewModel>).Add(new FieldViewModel(new Field(), ""));
            OnPropertyChanged("Fields");
        }

        internal void RemoveField(string key)
        {
            FieldViewModel field = Fields.First(req => req.Key == key);
            (Fields as List<FieldViewModel>).Remove(field);
            OnPropertyChanged("Fields");
        }

        internal Account Convert()
        {
            Account account = new Account
            {
                Id = this.account.Id,
                Source = this.account.Source
            };

            foreach (FieldViewModel field in Fields)
                account.Fields.Add(field.Key, field.Convert());

            return account;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
