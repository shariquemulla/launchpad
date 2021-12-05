using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Models {

    public class Link {
        // ------------------------------------------------------- get/set methods
        [Key]
        public int linkId { get; set; }

        [Required]
        public int categoryId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name="Label")]
        public string label {get; set;}

        [Required]
        [Url]
        [MaxLength(300)]
        [Display(Name="URL")]
        public string href {get; set;}

        [Display(Name="Pinned")]
        public bool pinned {get; set;}
    }
}
