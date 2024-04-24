#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ArtDealer.Models;

public class LikesAndLikers
{
    [Key]    
    public int LikesAndLikersId { get; set; } 
    // The IDs linking to the adjoining tables   
    public int UserId { get; set; }    
    public int PostId { get; set; }
    // Our navigation properties - don't forget the ?    
    public User? User { get; set; }    
    public Post? Wedding { get; set; }
}