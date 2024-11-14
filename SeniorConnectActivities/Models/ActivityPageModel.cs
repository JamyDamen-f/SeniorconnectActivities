using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SeniorConnectActivities.Controllers;
using SeniorConnectActivities.Models.Entities;
using SeniorConnectActivitiesCore;

namespace SeniorConnectActivities.Models
{
    public class ActivityPageModel : PageModel
    {
        [BindProperty]
        public ActivityModel Activity { get; set; }

        public void OnGet(ActivityModel model)
        {
            Activity.Id = model.Id;
            Activity.Title = model.Title;
            Activity.Description = model.Description;
            Activity.Location = model.Location;
            Activity.Start = model.Start;
            Activity.End = model.End;
            Activity.MaxParticipants = model.MaxParticipants;
            Activity.Created = model.Created;
            Activity.LastUpdated = model.LastUpdated;
            Activity.Url = model.Url;
        }

        /*public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Activity.Id == 0)
            {
                activitiesController.AddEntity(Activity);
            }
            else
            {
                activitiesController.EditEntity(Activity);
            }

            RedirectToAction("Index", "Activities");
        }*/
    }
}
