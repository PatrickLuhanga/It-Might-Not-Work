using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RePurpose.Models
{
    public class Part
    {
        [Key]
        public int PartID { get; set; }

        [Required]
        [Display(Name = "Part Type")]
        public string PartType { get; set; } // e.g., Screen, Battery

        [Required]
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        [Display(Name = "Condition")]
        public string Condition { get; set; } // Working/Non-Working

        [Display(Name = "Estimated Value")]
        public decimal EstimatedValue { get; set; }
    }
}
