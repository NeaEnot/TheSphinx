using System;
using System.Windows;
using TheSphinx.Core.Logic;

namespace TheSphinx.GUI
{
    public partial class App : Application
    {
        internal static AccountLogic AccountLogic { get; private set; } = new AccountLogic();
        internal static PasswordController PasswordController { get; private set; } = new PasswordController(new TimeSpan(0, 5, 0));
    }
}
