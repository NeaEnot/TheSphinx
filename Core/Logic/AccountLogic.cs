using System.Collections.Generic;
using System.Linq;
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
                                Context.Crypto.Encrypt(model.Fields[key].Value, password)
                                :
                                model.Fields[key].Value,
                            Encrypted = model.Fields[key].Encrypted
                        });
            }

            Context.Accounts.Add(account);
            Context.Save();
        }

        /// <include file='Docs.xml' path='docs/members[@name="AccountLogic"]/ReadAll/*'/>
        public List<Account> Read()
        {
            return
                Context.Accounts
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
            Account model = Context.Accounts.First(req => req.Id == id);

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
                                Context.Crypto.Decrypt(model.Fields[key].Value, password)
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
            Account account = Context.Accounts.First(req => req.Id == model.Id);

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
                                Context.Crypto.Encrypt(model.Fields[key].Value, password)
                                :
                                model.Fields[key].Value,
                            Encrypted = model.Fields[key].Encrypted
                        });
            }

            Context.Save();
        }

        /// <include file='Docs.xml' path='docs/members[@name="AccountLogic"]/Delete/*'/>
        public void Delete(string id)
        {
            Account account = Context.Accounts.First(req => req.Id == id);
            Context.Accounts.Remove(account);
            Context.Save();
        }
    }
}
