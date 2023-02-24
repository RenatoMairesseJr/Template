using Infrastructure.Application.Automapper;
using Infrastructure.Application.DependencyInjection;
using Infrastructure.Application.Identity;
using Infrastructure.Application.Swagger;
using Presentation.Middleware.ContentSecurityPolicy;
using Presentation.Middleware.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var frontEndList = builder.Configuration["FrontEndBaseUrl"].Split(';');

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        corsBuilder => corsBuilder//.WithOrigins(frontEndList)
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .WithExposedHeaders("content-security-policy", "Content-Length", "Access-Control-Allow-Origin", "TotalRecords", "Origin")
            //.AllowCredentials()
            .AllowAnyHeader());
});


//Add Infrstructure application services
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.SetupIoC(builder.Configuration);
builder.Services.AddSwaggerDocumentService(builder.Configuration);
builder.Services.AutoMapperConfig();

var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
    app.AddCsp(builder.Configuration);

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();