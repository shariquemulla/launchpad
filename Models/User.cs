using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Models {

    public class User {
        // ------------------------------------------------------- get/set methods
        [Key]
        public string username {get; set;}
        public string password {get; set;}
        public string salt {get; set;}
    }
}
