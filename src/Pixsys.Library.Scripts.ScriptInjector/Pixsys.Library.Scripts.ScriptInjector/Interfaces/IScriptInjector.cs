// -----------------------------------------------------------------------
// <copyright file="IScriptInjector.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Pixsys.Library.Scripts.ScriptInjector.Models;

namespace Pixsys.Library.Scripts.ScriptInjector.Interfaces
{
    /// <summary>
    /// The script injector interface.
    /// </summary>
    public interface IScriptInjector
    {
        /// <summary>
        /// Gets the injected scripts.
        /// </summary>
        /// <returns>The list of injected scripts.</returns>
        List<InjectedScript> GetScripts();

        /// <summary>
        /// Gets the injected paths.
        /// </summary>
        /// <returns>The list of injected paths.</returns>
        List<InjectedPath> GetPaths();

        /// <summary>
        /// Adds a script to the scripts injector.
        /// </summary>
        /// <param name="script">The script.</param>
        void AddScript(InjectedScript script);

        /// <summary>
        /// Adds scripts to the scripts injector.
        /// </summary>
        /// <param name="scripts">The scripts.</param>
        void AddScripts(List<InjectedScript> scripts);

        /// <summary>
        /// Adds a path to the scripts injector.
        /// </summary>
        /// <param name="path">The path.</param>
        void AddPath(InjectedPath path);

        /// <summary>
        /// Adds paths to the scripts injector.
        /// </summary>
        /// <param name="paths">The paths.</param>
        void AddPaths(List<InjectedPath> paths);

        /// <summary>
        /// Injects the registered script.
        /// </summary>
        /// <param name="name">The name.</param>
        void InjectRegisteredScript(string name);

        /// <summary>
        /// Injects the registered path.
        /// </summary>
        /// <param name="name">The name.</param>
        void InjectRegisteredPath(string name);
    }
}