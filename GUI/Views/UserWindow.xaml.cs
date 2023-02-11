using System.Windows;
using TheSphinx.Core.Models;
using TheSphinx.GUI.ViewModels;

namespace TheSphinx.GUI.Views
{
    public partial class UserWindow : Window
    {
        private UserViewModel model;

        public UserWindow(User user)
        {
            InitializeComponent();

            //model = new UserViewModel(user, App.NetworkLogic.RequiredFields);
            DataContext = model;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            User user = model.Convert();
            App.UserLogic.Set(user, App.PasswordController.GetPassword(PasswordController.PasswordType.fields));

            DialogResult = true;
        }
    }
}
