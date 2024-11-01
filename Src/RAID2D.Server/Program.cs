using RAID2D.Server.Hubs;
using RAID2D.Server.Observers;
using RAID2D.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddSingleton<GameStateSubject>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseRouting();

app.MapHub<GameHub>($"/{SharedConstants.ServerHub}");

app.MapGet("/", () => "Hello World!");

app.Run();

