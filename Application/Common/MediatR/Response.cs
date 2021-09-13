using System.Collections.Generic;
using WhatBug.Application.Common.Models;

namespace WhatBug.Application.Common.MediatR
{
    public class Response<T> : Response
    {
        public T Result { get; }

        internal Response(bool succeeded, IEnumerable<ValidationError> errors, T result)
            : base(succeeded, errors)
        {
            Result = result;
        }

        public static Response<T> Success(T result)
        {
            return new Response<T>(true, null, result);
        }

        public static new Response<T> Failure(IEnumerable<ValidationError> errors)
        {
            return new Response<T>(false, errors, default);
        }

        public static new Response<T> Failure(ValidationError error)
        {
            return new Response<T>(false, new ValidationError[] { error }, default);
        }
    }

    public class Response
    {
        public bool Succeeded { get; }
        public IEnumerable<ValidationError> ValidationErrors { get; }

        internal Response(bool succeeded, IEnumerable<ValidationError> errors)
        {
            Succeeded = succeeded;
            ValidationErrors = errors;
        }
        public static Response Success()
        {
            return new Response(true, null);
        }

        public static Response Failure(IEnumerable<ValidationError> errors)
        {
            return new Response(false, errors);
        }

        public static Response Failure(ValidationError error)
        {
            return new Response(false, new ValidationError[] { error });
        }
    }
}
