using System;
using System.Linq;
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

        internal string GetAccauntFields(string id)
        {
            string answer = "";

            Account model = Context.Accounts.FirstOrDefault(req => req.Id == id);

            if (model == null)
            {
                return "";
            }
            else
            {
                answer = $"Source:\t{model.Source}\n";

                foreach (string key in model.Fields.Keys)
                    answer += $"{key}:\t{model.Fields[key]}\n";

                return answer;
            }
        }
    }
}