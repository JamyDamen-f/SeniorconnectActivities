using System.ComponentModel.DataAnnotations;

namespace SeniorConnectActivities.Models
{
    public class AddActivitiesViewModel
    {
        [Required(ErrorMessage = "Titel is vereist.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Locatie is vereist.")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Start datum en tijd zijn vereist.")]
        [DataType(DataType.DateTime)]
        public DateTime Start {  get; set; }
        [Required(ErrorMessage = "Eind datum en tijd zijn vereist.")]
        [DataType(DataType.DateTime)] 
        public DateTime End { get; set; }
        [Required(ErrorMessage = "Aantal deelnemers is vereist.")]
        [Range(1, int.MaxValue, ErrorMessage = "Aantal deelnemers moet minimaal 1 zijn")]
        public int MaxParticipants { get; set; }
        [StringLength(200, ErrorMessage = "Beschrijving kan niet meer dan 200 characters bevatten.")]
        public string Description { get; set; }

    }
}
