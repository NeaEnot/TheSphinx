using GUI.Views;
using System;

namespace GUI
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

        internal string GetPassword()
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
}
