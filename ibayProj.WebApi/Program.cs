using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using ibayProj.Entities.Contexts;
using ibayProj.Entities.Models;
using ibayProj.Entities.Repositories;
using ibayProj.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IBasicRepository<User>, UserRepository>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ASPC API 3proj",
        Description = "Adeline & Martel Clément",
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
builder.Services.AddCors(options => 
    options.AddDefaultPolicy(policyBuilder => 
        policyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your-issuer", // Mettez votre émetteur ici
            ValidAudience = "your-audience", // Mettez votre audience ici
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key")) // Mettez votre clé secrète ici
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();

/*
{
 "email": "test@test.com",
 "pseudo": "string",
 "password": "Pp:A<y!v6h7yo5{m"
}

"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InRlc3RAdGVzdC5jb20iLCJuYmYiOjE3MDczMDAzMDYsImV4cCI6MTcwNzMwMzkwNiwiaWF0IjoxNzA3MzAwMzA2fQ.KLgVSE__6INraxTc5Hskqeuj0GRAyI7jI895zA1FO9o"
*/