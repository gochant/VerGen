// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics.Contracts;
using EnvDTE;

namespace VerGen.Tool.Extensions
{
    internal static class SourceControlExtenstions
    {
        public static bool CheckOutItemIfNeeded(this SourceControl sourceControl, string itemName)
        {
            Contract.Requires(sourceControl != null);
            Contract.Requires(!string.IsNullOrWhiteSpace(itemName));

            if (sourceControl.IsItemUnderSCC(itemName) && !sourceControl.IsItemCheckedOut(itemName))
            {
                return sourceControl.CheckOutItem(itemName);
            }

            return false;
        }
    }
}
