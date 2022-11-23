using GUI.ViewModels;
using System.Windows;
using TheSphinx.Core.Models;

namespace GUI.Views
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
            model.Fields.Add(new FieldViewModel(new Field(), ""));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void chbEncriped_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chbShow_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Account account = model.Convert();

            if (!string.IsNullOrEmpty(account.Id))
                App.AccountLogic.Create(account, App.PasswordController.GetPassword());
            else
                App.AccountLogic.Update(account, App.PasswordController.GetPassword());

            DialogResult = true;
        }
    }
}
