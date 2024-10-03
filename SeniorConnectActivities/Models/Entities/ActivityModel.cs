using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace SeniorConnectActivities.Models.Entities
{
    public class ActivityModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Url { get; set; }


    }
}
