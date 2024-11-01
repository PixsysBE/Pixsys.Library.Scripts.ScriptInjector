// -----------------------------------------------------------------------
// <copyright file="RegisteredScriptInjector.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Pixsys.Library.Scripts.ScriptInjector.Interfaces;
using Pixsys.Library.Scripts.ScriptInjector.Models;

namespace Pixsys.Library.Scripts.ScriptInjector
{
    /// <summary>
    /// The registered script injector.
    /// </summary>
    /// <seealso cref="IRegisteredScriptInjector" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:Opening square brackets should be spaced correctly", Justification = "Reviewed.")]
    internal sealed class RegisteredScriptInjector : IRegisteredScriptInjector
    {
        private readonly List<RegisteredScript> registeredScripts = [];
        private readonly List<RegisteredPath> registeredPaths = [];

        /// <inheritdoc/>
        public List<RegisteredScript> GetRegisteredScripts()
        {
            return registeredScripts;
        }

        /// <inheritdoc/>
        public List<RegisteredPath> GetRegisteredPaths()
        {
            return registeredPaths;
        }

        /// <inheritdoc/>
        public void AddScript(RegisteredScript script)
        {
            if (!registeredScripts.Exists(x => x.Name == script.Name))
            {
                registeredScripts.Add(script);
            }
        }

        /// <inheritdoc/>
        public void AddScripts(List<RegisteredScript> scripts)
        {
            foreach (RegisteredScript script in scripts)
            {
                AddScript(script);
            }
        }

        /// <inheritdoc/>
        public void AddPath(RegisteredPath path)
        {
            if (!registeredPaths.Exists(x => x.Name == path.Name))
            {
                registeredPaths.Add(path);
            }
        }

        /// <inheritdoc/>
        public void AddPaths(List<RegisteredPath> paths)
        {
            foreach (RegisteredPath path in paths)
            {
                AddPath(path);
            }
        }
    }
}