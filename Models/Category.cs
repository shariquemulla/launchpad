using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Models {

    public class Category {
        // ------------------------------------------------------- get/set methods
        [Key]
        public int categoryId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name="Category Name")]
        public string categoryName {get; set;}
    }
}
