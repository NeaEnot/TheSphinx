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
            ICrypto crypto = new CesarSequence((char)500);

            using (StreamWriter writer = new StreamWriter("storage.dat"))
            {
                Storage storage = new Storage
                {
                    User = User,
                    Accounts = Accounts.Select(acc => acc.Clone()).ToList(),
                    CurrentId = IdHelper.currentId
                };

                foreach (Field field in storage.User.Fields.Values)
                    if (field.Encrypted)
                        field.Value = crypto.Encrypt(field.Value, FieldsPassword);

                foreach (Account acc in storage.Accounts)
                    foreach (Field field in acc.Fields.Values)
                        if (field.Encrypted)
                            field.Value = crypto.Encrypt(field.Value, FieldsPassword);

                string json = JsonConvert.SerializeObject(storage);
                string data = crypto.Encrypt(json, StoragePassword);
                writer.Write(data);
            }
        }

        /// <include file='Docs.xml' path='docs/members[@name="Context"]/Load/*'/>
        public static void Load()
        {
            ICrypto crypto = new CesarSequence((char)500);

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
                        string json = crypto.Decrypt(data, StoragePassword);
                        Storage restored = JsonConvert.DeserializeObject<Storage>(json);

                        foreach (Field field in restored.User.Fields.Values)
                            if (field.Encrypted)
                                field.Value = crypto.Decrypt(field.Value, FieldsPassword);

                        foreach (Account acc in restored.Accounts)
                            foreach (Field field in acc.Fields.Values)
                                if (field.Encrypted)
                                    field.Value = crypto.Decrypt(field.Value, FieldsPassword);

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
