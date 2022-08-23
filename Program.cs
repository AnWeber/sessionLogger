using sessionLogger;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((ctx, builder) =>
                  {
                    builder.AddConfiguration(ctx.Configuration.GetSection("Logging"));
                    builder.AddFile(o => o.RootPath = ctx.HostingEnvironment.ContentRootPath);
                  })
    .ConfigureServices(services =>
    {
      services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

