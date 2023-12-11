using Chat.Api.Configurations;
using Chat.Api.Middlewares;
using Chat.Domain.Mappers;
using Chat.Infrastructure;

using Shared.Repository;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureHttp();
// TODO check if connection string exists
builder.Services.ConfigureEfDb<ChatMessageContext>(builder.Configuration.GetConnectionString("ChatMessageConnectionString"));
builder.Services.ConfigureAutoMapper(typeof(ChatMessageProfiles).Assembly);
builder.Services.ConfigureServices();
builder.Services.ConfigureOptionSettings(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using IServiceScope scope = app.Services.CreateScope();
    IServiceProvider services = scope.ServiceProvider;

    ChatMessageContext context = services.GetRequiredService<ChatMessageContext>();
    context.Database.EnsureCreated();
    await TestDbInitializer.Initialize(context);
}

app.UseMiddleware<ChatErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
