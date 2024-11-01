// -----------------------------------------------------------------------
// <copyright file="IRegisteredScriptInjector.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Pixsys.Library.Scripts.ScriptInjector.Models;

namespace Pixsys.Library.Scripts.ScriptInjector.Interfaces
{
    /// <summary>
    /// The registered script injector interface.
    /// </summary>
    public interface IRegisteredScriptInjector
    {
        /// <summary>
        /// Gets the registered scripts.
        /// </summary>
        /// <returns>The list of registered scripts.</returns>
        List<RegisteredScript> GetRegisteredScripts();

        /// <summary>
        /// Gets the registered paths.
        /// </summary>
        /// <returns>The list of registered paths.</returns>
        List<RegisteredPath> GetRegisteredPaths();

        /// <summary>
        /// Adds a script to the scripts injector.
        /// </summary>
        /// <param name="script">The script.</param>
        void AddScript(RegisteredScript script);

        /// <summary>
        /// Adds scripts to the scripts injector.
        /// </summary>
        /// <param name="scripts">The scripts.</param>
        void AddScripts(List<RegisteredScript> scripts);

        /// <summary>
        /// Adds a path to the scripts injector.
        /// </summary>
        /// <param name="path">The path.</param>
        void AddPath(RegisteredPath path);

        /// <summary>
        /// Adds paths to the scripts injector.
        /// </summary>
        /// <param name="paths">The paths.</param>
        void AddPaths(List<RegisteredPath> paths);
    }
}