using System;
using TheSphinx.Core;
using TheSphinx.Core.Models;

namespace TheSphinx.TheSphinxConsole.Logics
{
    internal class AccauntLogic
    {
        internal string CreateAccaunt(string source)
        {
            try
            {
                Account model = new Account();
                model.Source = source;

                Context.Accounts.Add(model);

                return model.Id;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}