using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.AdministrationService.Configuration.API.Adapters.Controllers
{
    public class ConfigurationModule : NancyModule
    {

        public ConfigurationModule() : base("/configuration")
        {
            Get["variablesets"] = parameters =>
            {
                return null;
            };

            Get["variablesets/{id}"] = parameters =>
            {
                return null;
            };

            Put["variablesets/{id}"] = parameters =>
            {
                return null;
            };

            Delete["variablesets/{id}"] = parameters =>
            {
                return null;
            };

            Post["variablesets"] = parameters =>
            {
                return null;
            };

            Get["variablesets/{id}/variables"] = parameters =>
            {
                return null;
            };

            Post["variablesets/{id}/variables"] = parameters =>
            {
                return null;
            };

            Get["variablesets/{id}/variables/{id}"] = parameters =>
            {
                return null;
            };

            Put["variablesets/{id}/variables/{id}"] = parameters =>
            {
                return null;
            };

            Delete["variablesets/{id}/variables/{id}"] = parameters =>
            {
                return null;
            };

            Get["variablesets/{id}/variables/names"] = parameters =>
            {
                return null;
            };

        }
    }
}
