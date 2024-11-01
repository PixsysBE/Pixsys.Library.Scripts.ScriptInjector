// -----------------------------------------------------------------------
// <copyright file="ScriptInjectorMiddleware.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using NetEscapades.AspNetCore.SecurityHeaders;
using Pixsys.Library.Scripts.ScriptInjector.Constants;
using Pixsys.Library.Scripts.ScriptInjector.Models;
using System.Text;

namespace Pixsys.Library.Scripts.ScriptInjector.Middlewares
{
    /// <summary>
    /// The script injector middleware.
    /// </summary>
    /// <param name="next">The request delegate.</param>
    public class ScriptInjectorMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate next = next;

        /// <summary>
        /// Invokes asynchronously.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:Keywords should be spaced correctly", Justification = "Reviewed.")]
        public async Task InvokeAsync(HttpContext context)
        {
            // Intercepte la réponse
            Stream originalBodyStream = context.Response.Body;

            await using (MemoryStream memoryStream = new())
            {
                context.Response.Body = memoryStream;

                // Appelle le middleware suivant
                await next(context);

                // Vérifie si la réponse est de type HTML
                if (context.Response.ContentType?.Contains("text/html") == true)
                {
                    // Lit la réponse
                    _ = memoryStream.Seek(0, SeekOrigin.Begin);
                    string responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
                    string nonce = context.GetNonce();
                    StringBuilder scripts = new();

                    if (context.Items[ContextItems.InjectedScriptsKey] is List<InjectionOptions> injectedScripts)
                    {
                        foreach (InjectionOptions script in injectedScripts)
                        {
                            _ = scripts.Append(!string.IsNullOrWhiteSpace(script.Path)
                                ? $"<script src=\"{script.Path}\" nonce=\"{nonce}\"></script>"
                                : $"<script nonce=\"{nonce}\">{script.Script}</script>");
                        }
                    }

                    if (context.Items[ContextItems.RegisteredScriptsKey] is List<InjectionOptions> registeredScripts)
                    {
                        foreach (InjectionOptions script in registeredScripts.Where(x => x.UseInHttpRequest))
                        {
                            _ = scripts.Append(!string.IsNullOrWhiteSpace(script.Path)
                                ? $"<script src=\"{script.Path}\" nonce=\"{nonce}\"></script>"
                                : $"<script nonce=\"{nonce}\">{script.Script}</script>");
                        }
                    }

                    responseBody = responseBody.Replace("</body>", $"{scripts}</body>");

                    byte[] modifiedBody = Encoding.UTF8.GetBytes(responseBody);
                    await originalBodyStream.WriteAsync(modifiedBody);
                }
                else
                {
                    _ = memoryStream.Seek(0, SeekOrigin.Begin);
                    await memoryStream.CopyToAsync(originalBodyStream);
                }
            }

            context.Response.Body = originalBodyStream;
        }
    }
}