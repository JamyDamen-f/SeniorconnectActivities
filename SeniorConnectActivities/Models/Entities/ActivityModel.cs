using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SeniorConnectActivities.Models.Entities
{
    public class ActivityModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Titel is vereist.")]
        public string Title { get; set; }
        [StringLength(200, ErrorMessage = "Beschrijving kan maar maximaal 200 karakters hebben.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Locatie is vereist.")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Start datum en tijd is vereist.")]
        public DateTime Start { get; set; }
        [Required(ErrorMessage = "Eind datum en tijd is vereist.")]
        public DateTime End { get; set; }
        [Required(ErrorMessage = "Aantal personen is vereist.")]
        [Range(1, int.MaxValue, ErrorMessage = "Moet minimaal 1 persoon zijn voor de activiteit.")]
        public int MaxParticipants { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public string? Url { get; set; }
    }
}
