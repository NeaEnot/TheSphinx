using GUI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using TheSphinx.Core.Models;

namespace TheSphinx.GUI.ViewModels
{
    internal class UserViewModel
    {
        public IEnumerable<FieldViewModel> Fields { get; set; }

        internal UserViewModel(User user, string[] requiredFields)
        {
            Fields = new List<FieldViewModel>();

            foreach (KeyValuePair<string, Field> field in user.Fields)
                if (requiredFields.Count(req => req == field.Key) == 0)
                    user.Fields.Clear();

            foreach (KeyValuePair<string, Field> field in user.Fields)
                (Fields as List<FieldViewModel>).Add(new FieldViewModel(field.Value, field.Key));

            foreach (string requiredField in requiredFields)
                if (Fields.Count(req => req.Key == requiredField) == 0)
                    (Fields as List<FieldViewModel>).Add(new FieldViewModel(new Field { Encrypted = false, Value = "" }, requiredField));

        }

        internal User Convert()
        {
            User user = new User();

            foreach (FieldViewModel field in Fields)
                user.Fields.Add(field.Key, field.Convert());

            return user;
        }
    }
}
