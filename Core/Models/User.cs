using System.Collections.Generic;

namespace TheSphinx.Core.Models
{
    /// <include file='Docs.xml' path='docs/members[@name="User"]/User/*'/>
    public class User
    {
        /// <include file='Docs.xml' path='docs/members[@name="User"]/Fields/*'/>
        public Dictionary<string, Field> Fields { get; set; }

        public User()
        {
            Fields = new Dictionary<string, Field>();
        }
    }
}
