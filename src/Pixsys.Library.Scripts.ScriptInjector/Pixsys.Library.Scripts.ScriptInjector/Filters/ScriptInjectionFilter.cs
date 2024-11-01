// -----------------------------------------------------------------------
// <copyright file="ScriptInjectionFilter.cs" company="Pixsys">
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
    /// The Script Injection Filter.
    /// </summary>
    /// <seealso cref="IResultFilter" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="ScriptInjectionFilter"/> class.
    /// </remarks>
    /// <param name="scriptInjector">The script injector.</param>
    public class ScriptInjectionFilter(IScriptInjector scriptInjector) : IResultFilter
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
                List<InjectionOptions>? scriptsSection = context.HttpContext.Items.ContainsKey(ContextItems.InjectedScriptsKey)
                    ? context.HttpContext.Items[ContextItems.InjectedScriptsKey] as List<InjectionOptions>
                : [];

                if (scriptInjector != null)
                {
                    foreach (InjectedPath path in scriptInjector.GetPaths())
                    {
                        scriptsSection?.Add(new InjectionOptions { Name = path.Name, Path = path.Path, UseInHttpRequest = true, });
                    }

                    foreach (InjectedScript script in scriptInjector.GetScripts())
                    {
                        scriptsSection?.Add(new InjectionOptions { Name = script.Name, Script = script.Script, UseInHttpRequest = true, });
                    }
                }

                context.HttpContext.Items[ContextItems.InjectedScriptsKey] = scriptsSection;
            }
        }
    }
}