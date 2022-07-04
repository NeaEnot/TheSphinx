using System;
using TheSphinx.Core;
using TheSphinx.Core.Interfaces;

namespace TheSphinx.TheSphinxConsole.Logics
{
    internal class NetworkLogic
    {
        private INetworkLogic networkLogic;

        internal NetworkLogic()
        {
            networkLogic = new YandexDisk.NetworkLogic();
        }

        internal string Upload()
        {
            try
            {
                networkLogic.Connect(() => "");
                networkLogic.Upload("storage.dat");
                return "OK";
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }

        internal string Download()
        {
            try
            {
                networkLogic.Connect(() => "");
                networkLogic.Download("storage.dat");

                Context.Load();

                return "OK";
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }
    }
}
