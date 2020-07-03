using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
public class PortraitForCreationDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(60, ErrorMessage = "Title can't be longer than 60 characters")]
    public string Title { get; set; }
 
    [Required(ErrorMessage = "Description is required")]
    [StringLength(60, ErrorMessage = "Description can't be longer than 150 characters")]
    public string Description { get; set; }
 
    [Required(ErrorMessage = "URL is required")]
    [StringLength(100, ErrorMessage = "URL cannot be loner then 150 characters")]
    public string URL { get; set; }
}
}