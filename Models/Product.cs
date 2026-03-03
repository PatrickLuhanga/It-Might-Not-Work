using System.ComponentModel.DataAnnotations;

namespace RePurpose.Models
{
    public class Product
    {
      
        [Key]
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Product Type")]
        public string ProductType { get; set; } // e.g., Laptop, Phone

        [Required]
        public string Description { get; set; }

        [Display(Name = "Working?")]
        public bool IsWorking { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; } // Store image path, not the image itself

        // Navigation property for parts
        public virtual ICollection<Part> Parts { get; set; }
       
    }
}
