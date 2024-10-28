﻿namespace SeniorConnectActivities.Models
{
    public class AddActivitiesViewModel
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Start {  get; set; }
        public DateTime End { get; set; }
        public int MaxParticipants { get; set; }
        public string Description { get; set; }

    }
}
