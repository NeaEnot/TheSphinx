using GUI.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TheSphinx.Core.Models;

namespace TheSphinx.GUI.Views
{
    public partial class AccountWindow : Window
    {
        private AccountViewModel model;

        public AccountWindow(Account account)
        {
            InitializeComponent();

            model = new AccountViewModel(account);
            DataContext = model;
        }

        private void btnAddField_Click(object sender, RoutedEventArgs e)
        {
            model.AddField();
            lbFields.Items.Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string key = (sender as Button).Tag as string;
            model.RemoveField(key);
            lbFields.Items.Refresh();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Account account = model.Convert();

            if (string.IsNullOrEmpty(account.Id))
                App.AccountLogic.Create(account, App.PasswordController.GetPassword(PasswordController.PasswordType.fields));
            else
                App.AccountLogic.Update(account, App.PasswordController.GetPassword(PasswordController.PasswordType.fields));

            DialogResult = true;
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            string key = (sender as Button).Tag as string;
            FieldViewModel field = model.Fields.First(req => req.Key == key);

            Clipboard.SetText(field.Value);
        }
    }
}
