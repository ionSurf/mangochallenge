using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
// using System.Web.Http.Filters; // Microsoft.AspNet.WebApi.Core
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using LoggerService;
using Microsoft.Data.SqlClient;

namespace Filters.ExceptionFilters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context) {
            string msg = "";
            if ( context.Exception is SqlException ) {
                msg = "Database is down";
                context.ExceptionHandled = true;
            } else {
                msg = context.Exception.GetBaseException().Message;
            }

            ApiError apiError = new ApiError(msg);
            apiError.detail = "Exception" + context.Exception.StackTrace;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.HttpContext.Response.ContentType = "application/json";
            
            /*var contextFeature = context.HttpContext.Features.Get<IExceptionHandlerFeature>();
            if(contextFeature != null)
            { 
                //_logger.LogError($"Something went wrong: {contextFeature.Error}");

                context.Result = new JsonResult(apiError);
            }*/
            context.Result = new JsonResult(apiError);
            base.OnException(context);
        }
    }
}