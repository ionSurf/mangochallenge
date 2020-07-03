using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;

namespace Entities.Models {
    [Table("portrait")]
    public class Portrait : IEntity {
        [Column("PortraitId")]
        public Guid Id {get;set;}
        
        [Required(ErrorMessage = "Title is required")]
        [StringLength(60, ErrorMessage = "Title can't be longer than 50 characters")]
        public string Title {get;set;}

        [Required(ErrorMessage = "Description is required")]
        [StringLength(60, ErrorMessage = "Description can't be longer than 150 characters")]
        public string Description {get;set;}
        
        [Required(ErrorMessage = "URL is required")]
        [StringLength(100, ErrorMessage = "URL can't be longer than 150 characters")]
        public string URL {get;set;}

        //public ICollection<Portrait> Portraits { get; set; }
    }
}