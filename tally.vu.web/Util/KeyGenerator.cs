﻿using System;
using System.Diagnostics.Contracts;
using System.Web;

namespace tallyvu
{
    public static class KeyGenerator
    {
        public static string Generate()
        {
            return Generate(Guid.NewGuid().ToString("D").Substring(24));
        }

        public static string Generate(string input)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(input));

            return HttpUtility.UrlEncode(input.Replace(" ", "_").Replace("-", "_").Replace("&", "and"));
        }
    }
}
