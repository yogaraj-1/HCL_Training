
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Register controllers
builder.Services.AddControllers();

// Register OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure OpenAPI and Scalar
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Configure HTTP pipeline
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
