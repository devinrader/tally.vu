using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tallyvu.Models
{
    public class Vote : Entity<Guid>
    {
        public string From { get; set; }
        public string Value { get; set; }
    }
}