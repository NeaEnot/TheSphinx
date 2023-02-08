using System.Text;
using TheSphinx.Core.Models;

namespace TheSphinx.Core.Logic
{
    /// <include file='Docs.xml' path='docs/members[@name="UserLogic"]/UserLogic/*'/>
    public class UserLogic
    {
        /// <include file='Docs.xml' path='docs/members[@name="UserLogic"]/Set/*'/>
        public void Set(User model, string password)
        {
            User user = new User();

            foreach (string key in model.Fields.Keys)
            {
                user.Fields
                    .Add(
                        key,
                        new Field
                        {
                            Value =
                                model.Fields[key].Encrypted
                                ?
                                Context.Instance.CryptoFields.Encrypt(model.Fields[key].Value, password)
                                :
                                model.Fields[key].Value,
                            Encrypted = model.Fields[key].Encrypted
                        });
            }

            Context.Instance.User = user;
            Context.Instance.Save();
        }

        /// <include file='Docs.xml' path='docs/members[@name="UserLogic"]/Get/*'/>
        public User Get(string password)
        {
            User user = new User();

            foreach (string key in Context.Instance.User.Fields.Keys)
            {
                user.Fields
                    .Add(
                        key,
                        new Field
                        {
                            Value =
                                Context.Instance.User.Fields[key].Encrypted && password != null
                                ?
                                Context.Instance.CryptoFields.Decrypt(Context.Instance.User.Fields[key].Value, password)
                                :
                                Context.Instance.User.Fields[key].Value,
                            Encrypted = Context.Instance.User.Fields[key].Encrypted
                        });
            }

            return user;
        }
    }
}
