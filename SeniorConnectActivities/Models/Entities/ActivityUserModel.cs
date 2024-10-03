namespace SeniorConnectActivities.Models.Entities
{

    public class ActivityUserModel
    {
        public UserModel UserId { get; set; }
        public ActivityModel ActivityId { get; set; }
        public bool IsCreator { get; set; }

        public ActivityUserModel()
        {
            UserId = new UserModel();
            ActivityId = new ActivityModel();
        }
    }
}
