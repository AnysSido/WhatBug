using FluentValidation;
using System;

namespace WhatBug.Application.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithException<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Func<T, Exception> stateProvider)
        {
            return rule.WithState(x => stateProvider(x));
        }
    }
}