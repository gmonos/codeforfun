﻿using Microsoft.Owin;
using Microsoft.Owin.Security;
using Nancy;
using Nancy.Extensions;
using Nancy.Owin;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using NancyResponce = Nancy.Response;

namespace Zags.Web.Nancy.Security
{
    public static class NancySecurityExtensions
    {
        /// <summary>
        ///     Gets the Microsoft owin authentication manager from the nancy context.
        /// </summary>
        /// <param name="context">The current nancy context.</param>
        /// <param name="throwOnNull">Throws an exception if the owin reqeust environment does not exist. Otherwise null will be returned.</param>
        /// <returns>An <see cref="IAuthenticationManager" /> if nancy is hosted in an owin pipeline and throwOnNull is false.
        /// Null if nancy is not hosted in an owin pipeline and throwOnNull is false.
        /// </returns>
        public static IAuthenticationManager GetAuthenticationManager(this NancyContext context, bool throwOnNull = false)
        {
            object requestEnvironment;
            context.Items.TryGetValue(NancyMiddleware.RequestEnvironmentKey, out requestEnvironment);
            var environment = requestEnvironment as IDictionary<string, object>;
            if (environment == null && throwOnNull)
            {
                throw new InvalidOperationException("OWIN environment not found. Is this an owin application?");
            }
            return environment != null ? new OwinContext(environment).Authentication : null;
        }

        /// <summary>
        ///     Get the user from the Micrososft owin user from the nancy context.
        /// </summary>
        /// <param name="context">The current nancy context.</param>
        /// <returns>Returns the current user for the request.</returns>
        public static ClaimsPrincipal GetOwinUser(this NancyContext context)
        {
            IAuthenticationManager authenticationManager = context.GetAuthenticationManager(true);
            return authenticationManager.User;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        public static void RequiresOwinAuthentication(this INancyModule module)
        {
            module.AddBeforeHookOrExecute(ctx =>
            {
                IAuthenticationManager auth = ctx.GetAuthenticationManager();
                return auth?.User?.Identity == null || !auth.User.Identity.IsAuthenticated ? HttpStatusCode.Unauthorized: (NancyResponce)null;
            }, "Requires Owin authentication");
        }
    }
}
