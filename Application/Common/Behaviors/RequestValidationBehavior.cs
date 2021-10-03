using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Models;

namespace WhatBug.Application.Common.Behaviors
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Response
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /*
         *  Validation rules can either contain an exception (for exceptional issues e.g. invalid ID) 
         *  or an error message e.g. 'Name cannot be empty', typically used to inform the user on the screen.
         *  This behavior handles both.
         */
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var ex = validationResults.SelectMany(r => r.Errors)
                .Where(e => e.CustomState is Exception)
                .Select(e => e.CustomState as Exception)
                .FirstOrDefault();

            if (ex != null)
                throw ex;

            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null)
                .Select(f => new ValidationError(f.ErrorMessage, f.PropertyName, f.AttemptedValue)).ToList();

            if (failures.Count != 0)
            {
                var responseType = typeof(TResponse);
                if (responseType.IsGenericType)
                {
                    // If the generic Response<T> is used for this command/query then we need to
                    // use reflection to build the response object as we don't know what type
                    // T will be at runtime.
                    var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                    var parameters = new object[] { false, failures, default };
                    var response = Activator.CreateInstance(responseType, flags, null, parameters, null); // TODO: Write tests for this

                    return response as TResponse;
                }
               
                return Response.Failure(failures) as TResponse;
            }

            return await next();
        }
    }
}
