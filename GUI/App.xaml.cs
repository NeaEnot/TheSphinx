using System;
using System.Windows;
using TheSphinx.Core.Logic;

namespace TheSphinx.GUI
{
    public partial class App : Application
    {
        internal static AccountLogic AccountLogic { get; private set; } = new AccountLogic();
        internal static UserLogic UserLogic { get; private set; } = new UserLogic();

        //internal static INetworkLogic NetworkLogic { get; private set; } = new NetworkLogic(() => PasswordController.GetPassword(PasswordController.PasswordType.fields));

        internal static PasswordController PasswordController { get; private set; } = new PasswordController(new TimeSpan(0, 5, 0));
    }
}
