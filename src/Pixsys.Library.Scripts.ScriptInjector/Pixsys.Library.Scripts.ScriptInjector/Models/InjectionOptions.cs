// -----------------------------------------------------------------------
// <copyright file="InjectionOptions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Scripts.ScriptInjector.Models
{
    /// <summary>
    /// Injection options model. Contains a path or a script.
    /// </summary>
    public class InjectionOptions
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        /// <value>
        /// The script.
        /// </value>
        public string? Script { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the script must be injected in the current http request.
        /// </summary>
        public bool UseInHttpRequest { get; set; }
    }
}