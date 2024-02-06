using Microsoft.EntityFrameworkCore;
using _3CSharpProj.Entities.Contexts;
using _3CSharpProj.Entities.Models;
using _3CSharpProj.Entities.Repositories;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My ASPC API",
        Description = "Je cr�� mon API",
        TermsOfService = new Uri("https://contoso.com/terms"),
        Contact = new OpenApiContact
            { Name = "JC FONTAINE", Url = new Uri("https://supinfo.com") }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddDbContext<IBayContext>(options =>
{
    options
        .UseLazyLoadingProxies(false)
        .UseChangeTrackingProxies(false)
        .UseSqlServer(builder.Configuration.GetConnectionString("localhost"));
});

builder.Services.AddScoped<DbContext, IBayContext>();
builder.Services.AddCors(options => 
    options.AddDefaultPolicy(policyBuilder => 
        policyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.Run();