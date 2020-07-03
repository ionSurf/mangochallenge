using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Filters.ActionFilters {
    public class UserValidationFilterAttribute : IActionFilter {
        public void OnActionExecuting( ActionExecutingContext context ) {
            var param = context.ActionArguments.SingleOrDefault( p => p.Value is User );
            if ( param.Value == null ) {
                context.Result = new BadRequestObjectResult("User is null");
            }

            if ( !context.ModelState.IsValid) {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted( ActionExecutedContext context ) {

        }
    }
}