using Ahlatci.Shop.Aplication.Exceptions;
using Ahlatci.Shop.Aplication.Models.RequestModels;
using Ahlatci.Shop.Aplication.Validators.Category;
using ArxOne.MrAdvice.Advice;
using AspectCore.DynamicProxy;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Behaviors
{
    public class ValidationBehavior : Attribute ,IMethodAdvice
    {

        private readonly Type _validatorType;
        public ValidationBehavior(Type validatorType)
        {
            _validatorType = validatorType;
        }
        public void Advise(MethodAdviceContext context)
        {//methot calışmadan önce devreye girecek kodlar

            if (context.Arguments.Any()) 
            {
                var requestModel = context.Arguments[0];
                var validateMetot = _validatorType.GetMethod("Validate", new Type[] { requestModel.GetType() });
                var validateInstance = Activator.CreateInstance(_validatorType);
                if (validateMetot != null)
                {
                    var validationResult = (ValidationResult)validateMetot.Invoke(validateInstance, new object[] { requestModel });

                    if (!validationResult.IsValid)
                    {

                        throw new ValidateException(validationResult);

                    }
                }


            }


           



            context.Proceed();
            
            

            //methot calıştıktan sonra devreye girecek kodlar
        }

        
    }
}
