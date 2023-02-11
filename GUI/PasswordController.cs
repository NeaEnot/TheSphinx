using System;
using TheSphinx.GUI.Views;

namespace TheSphinx.GUI
{
    internal class PasswordController
    {
        private TimeSpan duration;
        private DateTime lastPasswordEnter;
        private string password = "";

        internal PasswordController(TimeSpan duration)
        {
            this.duration = duration;
        }

        internal string GetPassword(PasswordType type)
        {
            if (type == PasswordType.general)
            {
                PasswordWindow window = new PasswordWindow();

                if (window.ShowDialog() == true)
                    return window.Result;
                else
                    throw new OperationCanceledException();
            }
            else
            {
                if (DateTime.Now - lastPasswordEnter > duration)
                {
                    PasswordWindow window = new PasswordWindow();

                    if (window.ShowDialog() == true)
                    {
                        password = window.Result;
                        lastPasswordEnter = DateTime.Now;
                    }
                    else
                    {
                        password = "";
                    }
                }

                return password;
            }
        }

        internal void DropPassword()
        {
            lastPasswordEnter = DateTime.MinValue;
        }

        internal enum PasswordType
        {
            general,
            fields
        }
    }
}
