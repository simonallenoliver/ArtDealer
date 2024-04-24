#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
// this using statement allows use to use NotMapped
using System.ComponentModel.DataAnnotations.Schema;
namespace ArtDealer.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    // our custom validation UniqueEmail (drop the attribute part)
    [UniqueEmail]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage ="password must be at least 8 characters")]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [NotMapped]
    [Compare("Password")]
    public string PasswordConfirm { get; set; }

    // one to many navigation property
    public List<Post> AllPosts { get; set; } = new List<Post>();

    // Our navigation property to our LikesAndLikers class
    // Notice there is NO reference to the Post class   
    public List<LikesAndLikers> Likes { get; set; } = new List<LikesAndLikers>();
}

// custom email validation
public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
    	// Though we have Required as a validation, sometimes we make it here anyways
    	// In which case we must first verify the value is not null before we proceed
        if(value == null)
        {
    	    // If it was, return the required error
            return new ValidationResult("Email is required!");
        }
    
    	// This will connect us to our database since we are not in our Controller
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        // Check to see if there are any records of this email in our database
    	if(_context.Users.Any(e => e.Email == value.ToString()))
        {
    	    // If yes, throw an error
            return new ValidationResult("Email must be unique!");
        } else {
    	    // If no, proceed
            return ValidationResult.Success;
        }
    }
}