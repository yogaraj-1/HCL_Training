using MovieTicketBooking.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.

// OpenAPI - Development only
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// 1. HTTPS Redirection
app.UseHttpsRedirection();

// 2. Endpoint Metadata Diagnostics
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<EndpointMetadataMiddleware>();
}

// 3. Authentication
app.UseAuthentication();

// 4. Authentication and Authorization Diagnostics
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<AuthenticationDiagnosticsMiddleware>();

    app.UseMiddleware<AuthorizationDiagnosticsMiddleware>();
}

// 5. Authorization
app.UseAuthorization();

// 6. Map Controllers
app.MapControllers();

// 7. Run Application
app.Run();