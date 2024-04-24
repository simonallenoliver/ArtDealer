using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ArtDealer.Models;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;


namespace ArtDealer.Controllers;
[SessionCheck]
public class PostController : Controller
{
    private readonly ILogger<PostController> _logger;

    private MyContext _context;

    public PostController(ILogger<PostController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    [SessionCheck]
    [HttpGet("posts")]
    public IActionResult Posts()
    {
        List<Post> PostsFromDB = _context.Posts.Include(w => w.Creator).Include(p => p.Likers).ThenInclude(p => p.User).OrderByDescending(p => p.CreatedAt).ToList();
        return View("Posts", PostsFromDB);
    }

    [SessionCheck]
    [HttpGet("posts/new")]
    public IActionResult MakePost()
    {
        return View("MakePost");
    }

    [HttpPost("posts/submit")]
    public IActionResult SubmitPost(Post newPost)
    {
        // validations checked
        if (!ModelState.IsValid)
        {
            return View("MakePost");
        }
        // if the validations go through, then we add our form data to the db
        // in our context file, it knows to associate our Post model with the Posts table in our db
        _context.Add(newPost);
        // ALWAYS SAVE CHANGES
        _context.SaveChanges();
        // then we redirect to our Posts page, in our Post controller
        return RedirectToAction("Posts");
    }

    // delete Post post route
    [HttpPost("posts/{postId}/delete")]
    public IActionResult DeletePost(int postId)
    {

        // first we check to make sure this exists in our database before we try to delete it
        Post? PostToDelete = _context.Posts.SingleOrDefault(w => w.PostId == postId);

        if (PostToDelete != null && PostToDelete.UserId == (int)HttpContext.Session.GetInt32("UserId"))
        {
            _context.Remove(PostToDelete);
            _context.SaveChanges();
        }
        return RedirectToAction("Posts");
    }

    // toggle like unlike post
    [HttpPost("posts/{postId}/toggle")]
    public IActionResult TogglePost(int postId)
    {
        int userId = (int)HttpContext.Session.GetInt32("UserId");

        LikesAndLikers? LikedPost = _context.LikesAndLikerss.SingleOrDefault(l => l.UserId == userId && l.PostId == postId);
        
        
        if(LikedPost == null)
        {
            LikesAndLikers newLike = new(){UserId = userId, PostId = postId};
            _context.Add(newLike);
        }
        else
        {
            _context.Remove(LikedPost);
        }
        _context.SaveChanges();

        // redirects back to same page
        return Redirect(HttpContext.Request.Headers.Referer);
    }

    // edit page
    [HttpGet("posts/{postId}/edit")]
    public IActionResult EditPost(int postId)
    {
        
        Post? OnePost = _context.Posts.FirstOrDefault(d => d.PostId == postId);
        if(OnePost == null)
        {
            return RedirectToAction("Posts");
        }
        return View("EditThisPost", OnePost);
    }

    // edit Post route
    [HttpPost("posts/{postId}/update")]
    public IActionResult UpdatePost(int postId, Post newPost)
    {
        Post? OnePost = _context.Posts.FirstOrDefault(d => d.PostId == postId);
        if (ModelState.IsValid)
        {
            OnePost.ImageUrl = newPost.ImageUrl;
            OnePost.ImageTitle = newPost.ImageTitle;
            OnePost.ImageMedium = newPost.ImageMedium;
            OnePost.ImageForSale = newPost.ImageForSale;

            OnePost.UpdatedAt = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction("Posts");

        } 
        else
        {
            return View("EditThisPost", newPost);
        }
    }

    // get one post
    [HttpGet("posts/{postId}")]
    public IActionResult ViewPost(int postId)
    {
        Post? OnePost = _context.Posts.Include(w => w.Creator).Include(p => p.Likers).ThenInclude(p => p.User).FirstOrDefault(p => p.PostId == postId);
        
        if (OnePost == null)
        {
            return RedirectToAction("Posts");
        }

        return View("ViewPost", OnePost);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}