using System.Windows;
using System.Windows.Controls;

namespace TheSphinx.GUI.Views
{
    public partial class BindedPasswordBox : UserControl
    {
        public string Password
        {
            get => pb.Password;
            set
            {
                pb.Password = value;
                SetValue(PasswordProperty, value);
            }
        }

        public static readonly DependencyProperty PasswordProperty;

        public BindedPasswordBox()
        {
            InitializeComponent();
        }

        static BindedPasswordBox()
        {
            PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(BindedPasswordBox));
        }
    }
}
