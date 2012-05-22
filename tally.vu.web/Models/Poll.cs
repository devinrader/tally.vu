using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace tallyvu.Models
{
    public class Poll : Entity<Guid>
    {
        public Poll()
        {
            this.Votes = new List<Vote>();
        }

        public string Title { get; set; }
        public string PhoneNumber { get; set; }
        public IList<Option> Options { get; set; }
        public IList<Vote> Votes { get; set; }
    }
}