using System;
using TheSphinx.Core;
using TheSphinx.Core.Models;

namespace TheSphinx.TheSphinxConsole.Logics
{
    internal class UserLogic
    {
        internal bool Enter()
        {
            try
            {
                string storagePass = Console.ReadLine();
                string fieldsPass = Console.ReadLine();

                Context.StoragePassword = storagePass;
                Context.FieldsPassword = fieldsPass;

                Context.Load();

                Console.WriteLine("Done");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal void ChangeStoragePassword(string pass)
        {
            Context.Load();
            Context.StoragePassword = pass;

            Context.Save();
            Context.Load();

            Console.WriteLine("Done");
        }

        internal void ChangeFieldsPassword(string pass)
        {
            Context.Load();
            Context.FieldsPassword = pass;

            Context.Save();
            Context.Load();

            Console.WriteLine("Done");
        }

        internal void ChangeUserField(string prms)
        {
            string fieldName = prms.Split(' ')[0];
            string fieldEnc = prms.Split(' ')[1];
            string fieldValue = prms.Remove(0, (fieldName + " " + fieldEnc + " ").Length);

            Field field = new Field
            {
                Encrypted = fieldEnc == "+",
                Value = fieldValue
            };

            if (Context.User.Fields.ContainsKey(fieldName))
                Context.User.Fields[fieldName] = field;
            else
                Context.User.Fields.Add(fieldName, field);

            Console.WriteLine("Done");
        }

        internal void DeleteUserField(string name)
        {
            if (Context.User.Fields.ContainsKey(name))
            {
                Context.User.Fields.Remove(name);
                Console.WriteLine("Done");
            }
        }
    }
}
