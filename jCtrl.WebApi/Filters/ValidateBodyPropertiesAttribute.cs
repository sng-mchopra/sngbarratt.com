﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace jCtrl.WebApi.Filters

/* EXAMPLE OF USE

    [HttpPost]
    [ValidateBodyPropertiesAttribute(typeof(CreateCartItemBindingModel),"cartItem")]                
    public async Task<IHttpActionResult> CreateCartItemAsync([FromUri] Guid custId, [FromBody] JObject cartItem)

    */
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ValidateBodyPropertiesAttribute : ActionFilterAttribute
    {

        private readonly Type entityType;

        private readonly string parameterName;

        public ValidateBodyPropertiesAttribute(Type entityType, string parameterName)
        {
            this.entityType = entityType;
            this.parameterName = parameterName;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var props = this.entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var jObject = (JObject)actionContext.ActionArguments[this.parameterName];

            if (jObject == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    string.Format("Could not get values from body, parameter name: {0}", this.parameterName));
                return;
            }

            var invalidProps = jObject.Properties().Select(p => p.Name).Where(
            propName => !props.Any(pr => pr.Name.Equals(propName, StringComparison.OrdinalIgnoreCase))).ToList();

            if (invalidProps.Any())
            {
                var invalidPropsStr = invalidProps.Aggregate((curr, next) => curr + ", " + next);
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    string.Format("Request contains invalid parameters: {0}", invalidPropsStr));
            }
        }

        
    }
}
