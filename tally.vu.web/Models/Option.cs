using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tallyvu.Models
{
    public class Option : Entity<Guid>
    {
        public string Text { get; set; }
        public string Shortcut { get; set; }
    }
}