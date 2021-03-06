﻿using System;
using DevWarsztaty.API.Framework;
using DevWarsztaty.Messages.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawRabbit;
using RawRabbit.vNext;
using DevWarsztaty.API.Storage;
using DevWarsztaty.Messages.Events;
using DevWarsztaty.API.Handlers;

namespace DevWarsztaty.API
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddMvc();

			ConfigureRabbitMq(services);

			ConfigureDatabase(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			ConfigureHandlers(app);

			app.UseMvc(routes =>
			{
				//routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
			});
		}

		private void ConfigureRabbitMq(IServiceCollection services)
		{
			var options = new RabbitMqOptions();
			var section = Configuration.GetSection("rabbitmq");

			section.Bind(options);

			services.Configure<RabbitMqOptions>(section);

			var client = BusClientFactory.CreateDefault(options);
			services.AddSingleton<IBusClient>(client);

			services.AddScoped<IEventHandler<RecordCreated>, RecordCreatedHandler>();
			services.AddScoped<IEventHandler<CreateRecordFailed>, CreateRecordFailedHandler>();
		}

		private void ConfigureDatabase(IServiceCollection services)
		{
			services.AddSingleton<IStorage>(new InMemoryDb());
		}

		private void ConfigureHandlers(IApplicationBuilder app)
		{
			var client = app.ApplicationServices.GetService<IBusClient>();

			client.SubscribeAsync<RecordCreated>((msg, ctx) => app.ApplicationServices.GetService<IEventHandler<RecordCreated>>().HandleAsync(msg));
			client.SubscribeAsync<CreateRecordFailed>((msg, ctx) => app.ApplicationServices.GetService<IEventHandler<CreateRecordFailed>>().HandleAsync(msg));
		}
	}
}
