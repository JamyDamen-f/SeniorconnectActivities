using System.ComponentModel.DataAnnotations;

namespace SeniorConnectActivities.Models.Entities
{
    public class ActivityModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        [StringLength(200)]
        public int MaxParticipants { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Url { get; set; }


    }
}
