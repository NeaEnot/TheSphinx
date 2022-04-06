using System;
using System.IO;
using System.Threading;
using TheSphinx.Core;
using TheSphinx.Core.Interfaces;
using YandexDisk.Client;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;

namespace YandexDisk
{
    /// <include file='Docs.xml' path='docs/members[@name="NetworkLogic"]/NetworkLogic/*'/>
    public class NetworkLogic : INetworkLogic
    {
        private IDiskApi diskApi;

        public void Connect(Func<string> getCode)
        {
            diskApi = new DiskHttpApi(Context.User.Fields["token"].Value);
        }

        public void Download(string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                    file.Delete();

                diskApi.Files.DownloadFileAsync(Context.User.Fields["path"].Value, path).Wait();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось скачать данные: " + ex.Message);
            }
        }

        public void Upload(string path)
        {
            try
            {
                diskApi.Files.UploadFileAsync(Context.User.Fields["path"].Value, true, path, CancellationToken.None).Wait();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось загрузить данные: " + ex.Message);
            }
        }
    }
}
