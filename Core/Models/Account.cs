using System.Collections.Generic;

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
    }
}
