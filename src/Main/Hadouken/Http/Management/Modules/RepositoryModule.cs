﻿using System.Linq;
using Hadouken.Fx.JsonRpc;
using Hadouken.Plugins;
using Nancy;
using Nancy.Security;

namespace Hadouken.Http.Management.Modules
{
    public class RepositoryModule : NancyModule
    {
        public RepositoryModule(IPluginEngine pluginEngine, IJsonSerializer jsonSerializer)
            : base("repository")
        {
            this.RequiresAuthentication();

            Get["/"] = _ =>
            {
                var installedPlugins = pluginEngine.GetAll().Select(p => p.Manifest.Name).ToArray();
                return View["Index", new {InstalledPlugins = jsonSerializer.Serialize(installedPlugins)}];
            };

            Get["/install/{pluginId}"] = _ => View["Install"];
        }
    }
}
