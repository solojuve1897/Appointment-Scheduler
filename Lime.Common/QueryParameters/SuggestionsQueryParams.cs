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
        public string OfficehoursStart { get; set; }
        [Required]
        public string OfficehoursEnd { get; set; }
        [Required]
        public int MeetingLength { get; set; }
    }
}
