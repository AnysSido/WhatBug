using System.Collections.Generic;

namespace WhatBug.Application.Common.Result
{
    public class Result
    {
        public bool Succeeded { get; }
        public IEnumerable<Error> Errors { get; }

        internal Result(bool succeeded, IEnumerable<Error> errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }

        public static Result Success()
        {
            return new Result(true, null);
        }

        public static Result Failure(IEnumerable<Error> errors)
        {
            return new Result(false, errors);
        }

        public static Result Failure(Error error)
        {
            return new Result(false, new Error[] { error });
        }
    }
}
