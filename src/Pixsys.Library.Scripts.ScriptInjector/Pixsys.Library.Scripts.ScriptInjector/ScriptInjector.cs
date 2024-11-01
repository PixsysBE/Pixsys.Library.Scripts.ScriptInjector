// -----------------------------------------------------------------------
// <copyright file="ScriptInjector.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Pixsys.Library.Scripts.ScriptInjector.Interfaces;
using Pixsys.Library.Scripts.ScriptInjector.Models;

namespace Pixsys.Library.Scripts.ScriptInjector
{
    /// <summary>
    /// The script injector.
    /// </summary>
    /// <param name="registeredScriptInjector">The registered script injector.</param>
    /// <seealso cref="IScriptInjector" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:Opening square brackets should be spaced correctly", Justification = "Reviewed.")]
    public class ScriptInjector(IRegisteredScriptInjector registeredScriptInjector) : IScriptInjector
    {
        private readonly List<InjectedScript> scripts = [];
        private readonly List<InjectedPath> paths = [];

        /// <inheritdoc/>
        public List<InjectedScript> GetScripts()
        {
            return scripts;
        }

        /// <inheritdoc/>
        public List<InjectedPath> GetPaths()
        {
            return paths;
        }

        /// <inheritdoc/>
        public void AddScript(InjectedScript script)
        {
            if (!scripts.Exists(x => x.Name == script.Name))
            {
                scripts.Add(script);
            }
        }

        /// <inheritdoc/>
        public void AddScripts(List<InjectedScript> scripts)
        {
            foreach (InjectedScript script in scripts)
            {
                AddScript(script);
            }
        }

        /// <inheritdoc/>
        public void AddPath(InjectedPath path)
        {
            if (!paths.Exists(x => x.Name == path.Name))
            {
                paths.Add(path);
            }
        }

        /// <inheritdoc/>
        public void AddPaths(List<InjectedPath> paths)
        {
            foreach (InjectedPath path in paths)
            {
                AddPath(path);
            }
        }

        /// <inheritdoc/>
        public void InjectRegisteredScript(string name)
        {
            if (registeredScriptInjector != null)
            {
                List<RegisteredScript> registeredScripts = registeredScriptInjector.GetRegisteredScripts();
                RegisteredScript? registeredScript = registeredScripts?.Find(x => x.Name == name && !x.UseOnAllPages);
                if (registeredScript != null)
                {
                    AddScript(new InjectedScript { Name = registeredScript.Name, Script = registeredScript.Script });
                }
            }
        }

        /// <inheritdoc/>
        public void InjectRegisteredPath(string name)
        {
            if (registeredScriptInjector != null)
            {
                List<RegisteredPath> registeredPaths = registeredScriptInjector.GetRegisteredPaths();
                RegisteredPath? registeredPath = registeredPaths?.Find(x => x.Name == name && !x.UseOnAllPages);
                if (registeredPath != null)
                {
                    AddPath(new InjectedPath { Name = registeredPath.Name, Path = registeredPath.Path });
                }
            }
        }
    }
}