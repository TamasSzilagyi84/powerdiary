using System.Text.Json.Serialization;

using FluentValidation;

using Web.HttpAggregator.Configuration;
using Web.HttpAggregator.Middlewares;

string allowAll = "allowAll";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAll,
        policy  =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

builder.Services.ConfigureServices(builder.Configuration);
builder.Services.ConfigureOptionSettings(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    options.JsonSerializerOptions.DefaultIgnoreCondition =
        JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<WebHttpAggregatorErrorHandlingMiddleware>();

// TODO uncomment it when FE handles self signed certs
// app.UseHttpsRedirection();

app.UseCors(allowAll);

app.UseAuthorization();

app.MapControllers();

app.Run();
