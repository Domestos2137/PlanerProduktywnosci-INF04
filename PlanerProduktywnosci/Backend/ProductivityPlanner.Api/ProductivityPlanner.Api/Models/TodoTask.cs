using System.ComponentModel.DataAnnotations;

namespace ProductivityPlanner.Api.Models
{
    public class TodoTask
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }

        [Required]
        public string Status { get; set; } = "Nowe"; 

        [Required]
        public string Priority { get; set; } = "Normalny"; 

        public string Category { get; set; }
    }
}