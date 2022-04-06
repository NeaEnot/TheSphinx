using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheSphinx.Core.Helpers;
using TheSphinx.Core.Models;

namespace TheSphinx.Core
{
    /// <include file='Docs.xml' path='docs/members[@name="Context"]/Context/*'/>
    public static class Context
    {
        /// <include file='Docs.xml' path='docs/members[@name="Context"]/StoragePassword/*'/>
        public static string StoragePassword { private get; set; }
        /// <include file='Docs.xml' path='docs/members[@name="Context"]/FieldsPassword/*'/>
        public static string FieldsPassword { private get; set; }

        /// <include file='Docs.xml' path='docs/members[@name="Context"]/User/*'/>
        public static User User { get; set; }
        /// <include file='Docs.xml' path='docs/members[@name="Context"]/Accounts/*'/>
        public static List<Account> Accounts { get; set; }

        /// <include file='Docs.xml' path='docs/members[@name="Context"]/Save/*'/>
        public static void Save()
        {
            using (StreamWriter writer = new StreamWriter("storage.dat"))
            {
                Storage storage = new Storage
                {
                    User = User,
                    Accounts = Accounts.Select(acc => acc.Clone()).ToList(),
                    CurrentId = IdHelper.currentId
                };

                // Зашифровываем отдельные поля

                string json = JsonConvert.SerializeObject(storage);

                // Зашифровываем json

                writer.Write(json);
            }
        }

        /// <include file='Docs.xml' path='docs/members[@name="Context"]/Load/*'/>
        public static void Load()
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
                        string json = reader.ReadToEnd();

                        // Расшифровываем json

                        Storage restored = JsonConvert.DeserializeObject<Storage>(json);

                        // Расшифровываем отдельные поля

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
