using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models {
    [Table("users")]
    public class User {
        [Column("UserId")]
        public Guid Id {get;set;}
        
        [Required(ErrorMessage = "Username is required")]
        [StringLength(60, ErrorMessage = "Username can't be longer than 50 characters")]
        public string UserName {get;set;}
        
        //[JsonIgnore]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(60, ErrorMessage = "Password can't be longer than 150 characters")]
        public string Password {get;set;}
        

        //public ICollection<Portrait> Portraits { get; set; }
    }
}