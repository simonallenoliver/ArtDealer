// Name this anything you want with the word "Attribute" at the end
namespace ArtDealer.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        // Check to see if we got back null
        if(userId == null)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
            context.Result = new RedirectToActionResult("Index", "User", null);
        }
    }
}