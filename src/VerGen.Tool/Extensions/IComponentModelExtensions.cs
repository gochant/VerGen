// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using Microsoft.VisualStudio.ComponentModelHost;

namespace VerGen.Tool.Extensions
{
    internal static class IComponentModelExtensions
    {
        public static object GetService(this IComponentModel componentModel, Type serviceType)
        {
            Contract.Requires(componentModel != null);
            Contract.Requires(serviceType != null);

            return typeof(IComponentModel).GetMethod("GetService")
                .MakeGenericMethod(serviceType)
                .Invoke(componentModel, null);
        }
    }
}
