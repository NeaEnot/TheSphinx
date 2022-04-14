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
    }
}
