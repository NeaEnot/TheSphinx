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

                Context.Save();
                Context.Load();

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

        internal string SetField(string prms)
        {
            string accId = prms.Split(' ')[0];
            string fieldName = prms.Split(' ')[1];
            string fieldEnc = prms.Split(' ')[2];
            string fieldValue = prms.Remove(0, (fieldName + " " + fieldEnc + " ").Length);

            string answer = "";

            Account model = Context.Accounts.FirstOrDefault(req => req.Id == accId);

            if (model == null)
            {
                return "";
            }
            else
            {
                if (model.Fields.ContainsKey(fieldName))
                    model.Fields[fieldName] = new Field { Value = fieldValue, Encrypted = fieldEnc == "+" };
                else
                    model.Fields.Add(fieldName, new Field { Value = fieldValue, Encrypted = fieldEnc == "+" });

                Context.Save();
                Context.Load();

                answer = $"Source: {model.Source}\tField: {fieldName}\tValue: {fieldValue}";

                return answer;
            }
        }

        internal string DeleteAcc(string id)
        {
            string answer = "";

            Account model = Context.Accounts.FirstOrDefault(req => req.Id == id);

            if (model == null)
            {
                return "";
            }
            else
            {
                Context.Accounts.Remove(model);

                answer = $"Accaunt {id} has been deleted";

                return answer;
            }
        }
    }
}