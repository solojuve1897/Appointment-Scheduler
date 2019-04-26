using Lime.Common;
using Lime.Domain;
using System.Collections.Generic;

namespace Lime.Business
{
    public interface ISuggestionLogic
    {
        IList<SuggestionDto> GetSuggestions(SuggestionsQueryParams parameters);
    }
}
