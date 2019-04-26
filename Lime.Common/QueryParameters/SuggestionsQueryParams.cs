using System.ComponentModel.DataAnnotations;

namespace Lime.Common
{
    public class SuggestionsQueryParams
    {
        [Required]
        public string[] Employees { get; set; }
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        [Required]
        public string OfficeHoursStart { get; set; }
        [Required]
        public string OfficeHoursEnd { get; set; }
        [Required]
        public int MeetingLength { get; set; }
    }
}
