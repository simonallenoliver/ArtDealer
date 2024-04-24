#pragma warning disable CS8618

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArtDealer.Models;

public class LogUser
{
    [Required]
    [Display(Name ="Email: ")]
    [EmailAddress]
    public string LogEmail { get; set; }

    [Required]
    [Display(Name ="Password: ")]
    [DataType(DataType.Password)]
    public string LogPassword { get; set; }
}
