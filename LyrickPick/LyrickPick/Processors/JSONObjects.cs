using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LyrickPick.Processors
{
    namespace BingSpellCheck
    {
        public class Rootobject
        {
            public string _type { get; set; }
            public Flaggedtoken[] flaggedTokens { get; set; }
        }

        public class Flaggedtoken
        {
            public int offset { get; set; }
            public string token { get; set; }
            public string type { get; set; }
            public Suggestion[] suggestions { get; set; }
        }

        public class Suggestion
        {
            public string suggestion { get; set; }
            public float score { get; set; }
        }
    }

    
}