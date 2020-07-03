using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repositories;
using Microsoft.EntityFrameworkCore;

namespace Filters.ActionFilters
{
    public class PortraitValidateEntityExistsAttribute : IAsyncActionFilter 
    {
        //private readonly RepositoryBase<T> _context;
        private readonly IRepositoryWrapper _context;
 
        //public ValidateEntityExistsAttribute(RepositoryBase<T> context)
        public PortraitValidateEntityExistsAttribute(IRepositoryWrapper context)
        {
            _context = context;
        }
 
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Guid id = Guid.Empty;
 
            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (Guid)context.ActionArguments["id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }

            //var entity = _context.FindByCondition( e => e.Id.Equals( id ) ).FirstOrDefaultAsync();

            // Could be reused by setting conventions on _context.<TProperty>.GetByIdAsync( id ) ?
            var entity = await _context.Portrait.GetPortraitByIdAsync( id );

            if(entity == null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("entity", entity);
            }

            await next();
        }

    }
}