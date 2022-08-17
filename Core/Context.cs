using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheSphinx.Core.Crypto;
using TheSphinx.Core.Helpers;
using TheSphinx.Core.Models;

namespace TheSphinx.Core
{
    internal class Context
    {
        private static Context instanse;
        internal static Context Instance
        {
            get
            {
                if (instanse == null)
                    instanse = new Context();

                return instanse;
            }
        }

        internal string StoragePassword { private get; set; }

        internal User User { get; set; }
        internal List<Account> Accounts { get; set; }

        internal ICrypto Crypto { get; private set; } = new CesarSequence((char)500);

        internal void Save()
        {
            using (StreamWriter writer = new StreamWriter("storage.dat"))
            {
                Storage storage = new Storage
                {
                    User = User,
                    Accounts = Accounts.Select(acc => acc.Clone()).ToList(),
                    CurrentId = IdHelper.currentId
                };

                string json = JsonConvert.SerializeObject(storage);
                string data = Crypto.Encrypt(json, StoragePassword);
                writer.Write(data);
            }
        }

        internal void Load()
        {
            FileInfo file = new FileInfo("storage.dat");
            if (!file.Exists)
            {
                User = new User();
                Accounts = new List<Account>();
                IdHelper.currentId = "0";

                Save();
            }
            else
            {
                try
                {
                    using (StreamReader reader = new StreamReader("storage.dat"))
                    {
                        string data = reader.ReadToEnd();
                        string json = Crypto.Decrypt(data, StoragePassword);
                        Storage restored = JsonConvert.DeserializeObject<Storage>(json);

                        User = restored.User;
                        Accounts = restored.Accounts;
                        IdHelper.currentId = restored.CurrentId;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Data could not be recovered, possibly an incorrect password.");
                }
            }
        }
    }
}
