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
                                Context.Crypto.Encrypt(model.Fields[key].Value, password)
                                :
                                model.Fields[key].Value,
                            Encrypted = model.Fields[key].Encrypted
                        });
            }

            Context.User = user;
            Context.Save();
        }

        /// <include file='Docs.xml' path='docs/members[@name="UserLogic"]/Get/*'/>
        public User Get(string password)
        {
            User user = new User();

            foreach (string key in Context.User.Fields.Keys)
            {
                user.Fields
                    .Add(
                        key,
                        new Field
                        {
                            Value =
                                Context.User.Fields[key].Encrypted && password != null
                                ?
                                Context.Crypto.Decrypt(Context.User.Fields[key].Value, password)
                                :
                                Context.User.Fields[key].Value,
                            Encrypted = Context.User.Fields[key].Encrypted
                        });
            }

            return user;
        }
    }
}
