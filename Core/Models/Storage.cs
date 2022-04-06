using System.Collections.Generic;

namespace TheSphinx.Core.Models
{
    internal class Storage
    {
        public User User { get; set; }
        public List<Account> Accounts { get; set; }
        public string CurrentId { get; set; }
    }
}
