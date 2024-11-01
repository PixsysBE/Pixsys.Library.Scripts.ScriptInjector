// -----------------------------------------------------------------------
// <copyright file="ScriptInjectCodeTagHelper.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Pixsys.Library.Scripts.ScriptInjector.Constants;
using Pixsys.Library.Scripts.ScriptInjector.Models;

namespace Pixsys.Library.Scripts.ScriptInjector.TagHelpers
{
    /// <summary>
    /// A <see cref="TagHelper"/> to inject code.
    /// </summary>
    /// <seealso cref="TagHelper" />
    [HtmlTargetElement("script-inject-code")]
    public class ScriptInjectCodeTagHelper : TagHelper
    {
        /// <summary>
        /// Gets or sets the view context.
        /// </summary>
        /// <value>
        /// The view context.
        /// </value>
        [ViewContext]
        public ViewContext? ViewContext { get; set; }

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

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:Opening square brackets should be spaced correctly", Justification = "Reviewed.")]
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null; // Avoids generation of an additional HTML tag

            if (ViewContext != null)
            {
                List<InjectionOptions>? scriptsSection = ViewContext.HttpContext.Items.ContainsKey(ContextItems.InjectedScriptsKey)
                    ? ViewContext.HttpContext.Items[ContextItems.InjectedScriptsKey] as List<InjectionOptions>
                : [];

                if (scriptsSection?.Exists(x => x.Name == Name) == false)
                {
                    scriptsSection.Add(new InjectionOptions { Name = Name, Script = Script });
                }

                ViewContext.HttpContext.Items[ContextItems.InjectedScriptsKey] = scriptsSection;
            }
        }
    }
}