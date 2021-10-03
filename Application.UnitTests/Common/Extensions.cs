using AutoMapper.Internal;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace WhatBug.Application.UnitTests.Common
{
    internal static class Extensions
    {
        /*
         *  FluentValidation extension method based on ShouldHaveValidationErrorFor method.
         *  See https://github.com/FluentValidation/FluentValidation/blob/fb1e9392dc0b97ed2b60bcaa3cef425a413c1a6a/src/FluentValidation/TestHelper/TestValidationResult.cs#L34
         */
        public static IEnumerable<ValidationFailure> ShouldHaveExceptionFor<T, TProperty>(
            this TestValidationResult<T> result, Expression<Func<T, TProperty>> memberAccessor, Type exceptionType)
            where T : class
        {
            string propertyName = ValidatorOptions.Global.PropertyNameResolver(typeof(T), memberAccessor.GetMember(), memberAccessor);

            var failures = result.Errors.Where(x =>
                 Regex.Replace(x.PropertyName, @"\[.*\]", string.Empty) == propertyName && x.CustomState.GetType() == exceptionType).ToArray();

            if (failures.Any())
                return failures;

            throw new ValidationTestException("Expected exception.");
        }
    }
}