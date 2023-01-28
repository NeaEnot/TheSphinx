using System;
using System.IO;
using System.Threading;
using TheSphinx.Core.Interfaces;
using TheSphinx.Core.Logic;
using TheSphinx.Core.Models;
using YandexDisk.Client;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;

namespace YandexDisk
{
    /// <include file='Docs.xml' path='docs/members[@name="NetworkLogic"]/NetworkLogic/*'/>
    public class NetworkLogic : INetworkLogic
    {
        private IDiskApi diskApi;
        private UserLogic userLogic;

        private Func<string> getPassword;

        /// <include file='Docs.xml' path='docs/members[@name="NetworkLogic"]/Constructor/*'/>
        public NetworkLogic(Func<string> getPassword)
        {
            userLogic = new UserLogic();
            this.getPassword = getPassword;
        }

        public string[] RequiredFields => new string[] { "path", "token" };

        public void Connect(Func<string> getCode)
        {
            User user = userLogic.Get(getPassword());

            if (diskApi == null)
                diskApi = new DiskHttpApi(user.Fields["token"].Value);
        }

        public void Download(string path)
        {
            try
            {
                User user = userLogic.Get(getPassword());

                FileInfo file = new FileInfo(path);
                if (file.Exists)
                    file.Delete();

                diskApi.Files.DownloadFileAsync(user.Fields["path"].Value, path).Wait();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to download data: " + ex.Message);
            }
        }

        public void Upload(string path)
        {
            try
            {
                User user = userLogic.Get(getPassword());
                diskApi.Files.UploadFileAsync(user.Fields["path"].Value, true, path, CancellationToken.None).Wait();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload data: " + ex.Message);
            }
        }
    }
}
