using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Common.Mapping
{
    public static class Helpers
    {
        public static void ApplyMappingsFromAssembly(this Profile profile, Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodName = nameof(IMapFrom<Object>.Mapping);
                var interfaceName = typeof(IMapFrom<>).GetGenericTypeDefinition().Name;

                // type.GetMethod doesn't include default method body from the interface.
                // If there is no method implementation in the class we instead use the default method body in the interface.
                var methodInfo = type.GetMethod(methodName) ?? instance.GetType().GetInterface(interfaceName)?.GetMethod(methodName);

                methodInfo?.Invoke(instance, new object[] { profile });
            }
        }
    }
}
