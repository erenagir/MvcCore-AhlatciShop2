using Ahlatci.Shop.Aplication.Exceptions;
using Ahlatci.Shop.Aplication.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Ahlatci.Shop.Api.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        //intercepter (bölücü araya girici )

        public void OnException(ExceptionContext context)
        {

            var result = new Result<dynamic>() { Success = false };

            if (context.Exception is NotFoundException notFoundException)
            {
                result.Errors = new List<string> { context.Exception.Message };
            }
            else if(context.Exception is ValidateException validateException)
            {
                result.Errors = validateException.ErrorMessage ;
            }
            else
            {
                result.Errors = new List<string>();
                {
                    result.Errors = new List<string> { context.Exception.InnerException != null ? context.Exception.InnerException.Message : context.Exception.Message };

                }
            }


            context.Result = new JsonResult(result);
            context.HttpContext.Response.StatusCode = 400;
            context.ExceptionHandled = true;
        }
    }
}
