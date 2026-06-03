using Lego.Api.Endpoints;
using Lego.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddLego(builder.Configuration);

var app = builder.Build();
app.MapOpenApi();
app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Lego WebApi v1"));
app.UseHttpsRedirection();
app.MapLegoEndpoints();

app.Run();