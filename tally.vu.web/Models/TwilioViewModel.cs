﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tallyvu.Models
{
    public class TwilioViewModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
}