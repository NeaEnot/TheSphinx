using System.Collections.Generic;
using TheSphinx.Core.Helpers;

namespace TheSphinx.Core.Models
{
    /// <include file='Docs.xml' path='docs/members[@name="Account"]/Account/*'/>
    public class Account
    {
        /// <include file='Docs.xml' path='docs/members[@name="Account"]/Id/*'/>
        public string Id { get; set; }
        /// <include file='Docs.xml' path='docs/members[@name="Account"]/Source/*'/>
        public string Source { get; set; }
        /// <include file='Docs.xml' path='docs/members[@name="Account"]/Fields/*'/>
        public Dictionary<string, Field> Fields { get; set; }

        public Account()
        {
            Fields = new Dictionary<string, Field>();
        }

        internal Account Clone()
        {
            Account acc = new Account
            {
                Id = Id,
                Source = Source,
                Fields = new Dictionary<string, Field>()
            };

            foreach (string key in Fields.Keys)
                acc.Fields.Add(key, new Field { Value = Fields[key].Value, Encrypted = Fields[key].Encrypted });

            return acc;
        }
    }
}
