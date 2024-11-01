﻿// -----------------------------------------------------------------------
// <copyright file="RegisteredScript.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Scripts.ScriptInjector.Models
{
    /// <summary>
    /// Injected script model.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1206:Declaration keywords should follow order", Justification = "Reviewed.")]
    public class RegisteredScript : InjectedScript
    {
        /// <summary>
        /// Gets or sets a value indicating whether this script must be used on all pages.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use on all pages]; otherwise, <c>false</c>.
        /// </value>
        public required bool UseOnAllPages { get; set; }
    }
}