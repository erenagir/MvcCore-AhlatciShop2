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

            var result = new Result<dynamic>()
            {
                Errors = new List<string>() { context.Exception.Message },
                Success = false
            };
            context.Result = new JsonResult(result);
            
            context.ExceptionHandled = true;
        }
    }
}
