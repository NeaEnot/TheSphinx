using System.Collections.Generic;
using System.ComponentModel;
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

        public List<FieldViewModel> Fields { get; set; }

        internal AccountViewModel(Account account)
        {
            this.account = account;
            Fields = new List<FieldViewModel>();

            foreach (KeyValuePair<string, Field> field in account.Fields)
                Fields.Add(new FieldViewModel(field.Value, field.Key));
        }

        internal Account Convert()
        {
            Account account = new Account
            {
                Id = this.account.Id,
                Source = this.account.Source
            };

            foreach (FieldViewModel field in Fields)
                account.Fields.Add(field.Key, field.Field);

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
