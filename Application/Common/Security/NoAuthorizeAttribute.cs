using System;

namespace WhatBug.Application.Common.Security
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class NoAuthorizeAttribute : Attribute { }
}
