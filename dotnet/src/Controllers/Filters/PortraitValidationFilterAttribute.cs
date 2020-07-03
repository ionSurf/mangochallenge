using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;

namespace Filters.ActionFilters {
    public class PortraitValidationFilterAttribute<T> : IActionFilter {
        public void OnActionExecuting( ActionExecutingContext context ) {
            var param  = context.ActionArguments.SingleOrDefault( p => p.Value is T );
            
            if (param.Value == null)
                context.Result = new BadRequestObjectResult( "Portrait is null" );

            if ( !context.ModelState.IsValid )
                context.Result = new BadRequestObjectResult( context.ModelState );
        }

        public void OnActionExecuted( ActionExecutedContext context ) {
            
        }
    }
}