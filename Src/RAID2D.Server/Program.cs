using RAID2D.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseRouting();

app.MapHub<ChatHub>("/chathub");

app.MapGet("/", () => "Hello World!");

app.Run();
