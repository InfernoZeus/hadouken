﻿using System.ServiceModel;
using Hadouken.Fx.ServiceModel;
using Nancy;

namespace Hadouken.Http.Management.Modules
{
    public class JsonRpcModule : NancyModule
    {
        public JsonRpcModule()
        {
            Post["/jsonrpc"] = _ =>
            {
                var plugin = Request.Form.pluginId;
                var json = Request.Form.json;
                var binding = new NetNamedPipeBinding();
                var endpoint = new EndpointAddress(string.Format("net.pipe://localhost/hadouken.plugins.{0}", plugin));

                using (var factory = new ChannelFactory<IPluginService>(binding, endpoint))
                {
                    var channel = factory.CreateChannel();
                    return channel.HandleJsonRpc(json);
                }
            };
        }
    }
}
