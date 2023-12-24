using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Banking
{
    public class CustomAuthorizationFilter : IActionFilter
    {
        
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Your custom authorization logic
            //var user = context.HttpContext.User;
           
            //string actionName = context.ActionDescriptor.RouteValues["action"];
            //string controllerName = context.Controller.GetType().Name;

            //// Retrieve the session variable
        
            //if ( (actionName != "Index" && controllerName != "AccountController")   )
            //{
            //    bool isAuthenticated = false;
            //    context.HttpContext.Session.SetString("Isauthenticated", isAuthenticated ? "true" : "false");
            //    var isAuthenticatedValue = context.HttpContext.Session.GetString("Isauthenticated");
            //    if (isAuthenticatedValue == "false")
            //    {
            //        context.Result = new RedirectResult("/Account/Index");
            //    }
            //}
            //else
            //{
                
            //}
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes
        }
    }
}
