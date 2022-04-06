using System.Collections.Generic;

namespace TheSphinx.Core.Models
{
    /// <include file='Docs.xml' path='docs/members[@name="User"]/User/*'/>
    public class User
    {
        /// <include file='Docs.xml' path='docs/members[@name="User"]/Dict/*'/>
        public Dictionary<string, Field> Dict { get; set; }

        public User()
        {
            Dict = new Dictionary<string, Field>();
        }
    }
}
