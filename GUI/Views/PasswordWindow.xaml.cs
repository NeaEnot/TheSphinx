using System.Windows;

namespace GUI.Views
{
    public partial class PasswordWindow : Window
    {
        internal string Result { get; private set; }

        public PasswordWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Result = passwordBox.Password;
            DialogResult = true;
        }
    }
}
