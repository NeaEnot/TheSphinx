using System;
using TheSphinx.Core;

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

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal void ChangeStoragePassword()
        {
            string storagePass = Console.ReadLine();
            Context.Load();

            Context.StoragePassword = storagePass;
            Context.Save();
            Context.Load();
        }

        internal void ChangeFieldsPassword()
        {
            string fieldsPass = Console.ReadLine();
            Context.Load();

            Context.FieldsPassword = fieldsPass;
            Context.Save();
            Context.Load();
        }
    }
}
