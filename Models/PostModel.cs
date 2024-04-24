#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace ArtDealer.Models;



public class Post
{    
    [Key]

    public int PostId { get; set; }    

    [Required]
    [Display(Name = "Image (URL format please): ")]
    public string ImageUrl { get; set; }

    [Required]
    [Display(Name = "Title: ")]
    public string ImageTitle { get; set; }  

    [Required]
    [Display(Name = "Medium: ")]
    public string ImageMedium { get; set; }

    [Required]
    [Display(Name = "For Sale? ")]
    public bool ImageForSale { get; set; }

    public DateTime CreatedAt {get; set; } = DateTime.Now;
    public DateTime UpdatedAt {get; set; } = DateTime.Now;



    // This is the ID we will use to know which User made the post
    // This name should match the name of the key from the User table (UserId)
    public int UserId { get; set; }

    // Our navigation property to track which User made this Post
    // It is VERY important to include the ? on the datatype or this won't work!
    public User? Creator { get; set; }


    // Our navigation property to our LikesAndLikers class
    // Notice there is NO reference to the User class
    public List<LikesAndLikers> Likers { get; set; } = new List<LikesAndLikers>();


}