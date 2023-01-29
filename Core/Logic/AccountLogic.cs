using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheSphinx.Core.Helpers;
using TheSphinx.Core.Models;

namespace TheSphinx.Core.Logic
{
    /// <include file='Docs.xml' path='docs/members[@name="AccountLogic"]/AccountLogic/*'/>
    public class AccountLogic
    {
        /// <include file='Docs.xml' path='docs/members[@name="AccountLogic"]/Create/*'/>
        public void Create(Account model, string password)
        {
            IdHelper.ToNextId();
            model.Id = IdHelper.currentId;

            Account account = new Account
            {
                Id = model.Id,
                Source = model.Source
            };

            foreach (string key in model.Fields.Keys)
            {
                account.Fields
                    .Add(
                        key, 
                        new Field
                        {
                            Value = 
                                model.Fields[key].Encrypted
                                ?
                                Encrypt(model.Fields[key].Value, password)
                                :
                                model.Fields[key].Value,
                            Encrypted = model.Fields[key].Encrypted
                        });
            }

            Context.Instance.Accounts.Add(account);
            Context.Instance.Save();
        }

        /// <include file='Docs.xml' path='docs/members[@name="AccountLogic"]/ReadAll/*'/>
        public List<Account> Read()
        {
            return
                Context.Instance.Accounts
                .Select(req => new Account
                {
                    Id = req.Id,
                    Source = req.Source
                })
                .ToList();
        }

        /// <include file='Docs.xml' path='docs/members[@name="AccountLogic"]/Read/*'/>
        public Account Read(string id, string password = null)
        {
            Account model = Context.Instance.Accounts.First(req => req.Id == id);

            Account account = new Account
            {
                Id = model.Id,
                Source = model.Source
            };

            foreach (string key in model.Fields.Keys)
            {
                account.Fields
                    .Add(
                        key,
                        new Field
                        {
                            Value =
                                model.Fields[key].Encrypted && password != null
                                ?
                                Decrypt(model.Fields[key].Value, password)
                                :
                                model.Fields[key].Value,
                            Encrypted = model.Fields[key].Encrypted
                        });
            }

            return account;
        }

        /// <include file='Docs.xml' path='docs/members[@name="AccountLogic"]/Update/*'/>
        public void Update(Account model, string password)
        {
            Account account = Context.Instance.Accounts.First(req => req.Id == model.Id);

            account.Source = model.Source;
            account.Fields.Clear();

            foreach (string key in model.Fields.Keys)
            {
                account.Fields
                    .Add(
                        key,
                        new Field
                        {
                            Value =
                                model.Fields[key].Encrypted
                                ?
                                Encrypt(model.Fields[key].Value, password)
                                :
                                model.Fields[key].Value,
                            Encrypted = model.Fields[key].Encrypted
                        });
            }

            Context.Instance.Save();
        }

        /// <include file='Docs.xml' path='docs/members[@name="AccountLogic"]/Delete/*'/>
        public void Delete(string id)
        {
            Account account = Context.Instance.Accounts.First(req => req.Id == id);
            Context.Instance.Accounts.Remove(account);
            Context.Instance.Save();
        }

        private static string Encrypt(string value, string password)
        {
            byte[] data = Encoding.Default.GetBytes(value);
            byte[] encoded = Context.Instance.Crypto.Encrypt(data, password);
            return Encoding.Default.GetString(encoded);
        }

        private static string Decrypt(string value, string password)
        {
            byte[] data = Encoding.Default.GetBytes(value);
            byte[] decoded = Context.Instance.Crypto.Decrypt(data, password);
            return Encoding.Default.GetString(decoded);
        }
    }
}
