using System;
using System.Collections.Generic;

namespace Lime.Domain
{
    public class SuggestionDto
    {
        public string Date { get; set; }
        public IList<string> StartTimes { get; set; } = new List<string>();
    }
}
