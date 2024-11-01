// -----------------------------------------------------------------------
// <copyright file="RegisteredScriptInjectionFilter.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pixsys.Library.Scripts.ScriptInjector.Constants;
using Pixsys.Library.Scripts.ScriptInjector.Interfaces;
using Pixsys.Library.Scripts.ScriptInjector.Models;

namespace Pixsys.Library.Scripts.ScriptInjector.Filters
{
    /// <summary>
    ///  The Registered Script Injection Filter.
    /// </summary>
    /// <param name="registeredScriptInjector">The script injector.</param>
    /// <seealso cref="IResultFilter" />
    internal sealed class RegisteredScriptInjectionFilter(IRegisteredScriptInjector registeredScriptInjector) : IResultFilter
    {
        /// <inheritdoc/>
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:Opening square brackets should be spaced correctly", Justification = "Reviewed.")]
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ViewResult viewResult)
            {
                List<InjectionOptions>? scriptsSection = context.HttpContext.Items.ContainsKey(ContextItems.RegisteredScriptsKey)
                    ? context.HttpContext.Items[ContextItems.RegisteredScriptsKey] as List<InjectionOptions>
                : [];

                if (registeredScriptInjector != null)
                {
                    foreach (RegisteredPath path in registeredScriptInjector.GetRegisteredPaths())
                    {
                        scriptsSection?.Add(new InjectionOptions { Name = path.Name, Path = path.Path, UseInHttpRequest = path.UseOnAllPages, });
                    }

                    foreach (RegisteredScript script in registeredScriptInjector.GetRegisteredScripts())
                    {
                        scriptsSection?.Add(new InjectionOptions { Name = script.Name, Script = script.Script, UseInHttpRequest = script.UseOnAllPages, });
                    }
                }

                context.HttpContext.Items[ContextItems.RegisteredScriptsKey] = scriptsSection;
            }
        }
    }
}