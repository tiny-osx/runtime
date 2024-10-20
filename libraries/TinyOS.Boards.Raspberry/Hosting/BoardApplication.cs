﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using TinyOS.Hosting;
using Microsoft.Extensions.DependencyInjection;

using TinyOS.Hardware;

namespace TinyOS.Boards
{
    
    public static class BoardApplication
    {        
        public static INetworkAdapterCollection NetworkAdapters = new NetworkAdapterCollection();
        
        public static HostApplicationBuilder CreateBuilder() => CreateBuilder(args: null);
         
        public static HostApplicationBuilder CreateBuilder(string[]? args)
        {
            var builder = Host.CreateEmptyApplicationBuilder(new HostApplicationBuilderSettings
            {
               Args = args
            });

            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
            
            //builder.Configuration.AddJsonFile("settings.json", optional:true);
            //builder.Configuration.AddJsonFile("/boot/settings.json", optional:true);
            
            builder.Services.AddNetworking();
            
            // if (builder.Environment.IsDevelopment())
            // {
                builder.Logging.AddDebug();
            //}
            
            return builder;
        }   
    }
}