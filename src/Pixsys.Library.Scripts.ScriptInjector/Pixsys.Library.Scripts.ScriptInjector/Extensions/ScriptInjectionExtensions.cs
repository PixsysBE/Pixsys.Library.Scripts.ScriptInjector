// -----------------------------------------------------------------------
// <copyright file="ScriptInjectionExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pixsys.Library.Scripts.ScriptInjector.Filters;
using Pixsys.Library.Scripts.ScriptInjector.Interfaces;
using Pixsys.Library.Scripts.ScriptInjector.Middlewares;
using Pixsys.Library.Scripts.ScriptInjector.Models;

#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace Pixsys.Library.Scripts.ScriptInjector
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Script Injections extensions.
    /// </summary>
    public static class ScriptInjectionExtensions
    {
        /// <summary>
        /// Registers scripts.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="scripts">The scripts.</param>
        /// <returns>The updated application.</returns>
        public static WebApplication RegisterScripts(this WebApplication app, List<RegisteredScript> scripts)
        {
            IRegisteredScriptInjector? scriptInjector = GetRegisteredScriptInjector(app);
            scriptInjector?.AddScripts(scripts);
            return app;
        }

        /// <summary>
        /// Registers script paths.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="paths">The paths.</param>
        /// <returns>The updated application.</returns>
        public static WebApplication RegisterScriptPaths(this WebApplication app, List<RegisteredPath> paths)
        {
            IRegisteredScriptInjector? scriptInjector = GetRegisteredScriptInjector(app);
            scriptInjector?.AddPaths(paths);
            return app;
        }

        /// <summary>
        /// Adds the injection scripts filter.
        /// </summary>
        /// <param name="mvcBuilder">The MVC builder.</param>
        /// <returns>The updated builder.</returns>
        public static IMvcBuilder AddInjectionScriptsFilter(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.Services.TryAddSingleton<IRegisteredScriptInjector, RegisteredScriptInjector>();
            _ = mvcBuilder.AddMvcOptions(mvcOptions => mvcOptions.Filters.Add<RegisteredScriptInjectionFilter>());
            mvcBuilder.Services.TryAddScoped<IScriptInjector, ScriptInjector>();
            _ = mvcBuilder.AddMvcOptions(mvcOptions => mvcOptions.Filters.Add<ScriptInjectionFilter>());
            return mvcBuilder;
        }

        /// <summary>
        /// Registeres and uses the injection script middleware.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>The updated application.</returns>
        /// <exception cref="InvalidOperationException">Web application is null.</exception>
        public static WebApplication UseInjectionScript(this WebApplication app)
        {
            _ = app?.UseMiddleware<ScriptInjectorMiddleware>();
#pragma warning disable CS8603 // Possible null reference return.
            return app;
#pragma warning restore CS8603 // Possible null reference return.
        }

        private static IRegisteredScriptInjector? GetRegisteredScriptInjector(WebApplication app)
        {
            return app.Services.GetRequiredService<IRegisteredScriptInjector>();
        }
    }
}