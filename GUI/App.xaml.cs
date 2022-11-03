using System.Windows;
using TheSphinx.Core.Logic;

namespace GUI
{
    public partial class App : Application
    {
        internal static AccountLogic AccountLogic { get; private set; } = new AccountLogic();
    }
}
