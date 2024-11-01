// -----------------------------------------------------------------------
// <copyright file="RegisteredPath.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Scripts.ScriptInjector.Models
{
    /// <summary>
    /// Registered path model.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1206:Declaration keywords should follow order", Justification = "Reviewed.")]
    public class RegisteredPath : InjectedPath
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