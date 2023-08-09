using Ahlatci.Shop.Api.Filters;
using Ahlatci.Shop.Aplication.AutoMap;
using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Services.Implementation;
using Ahlatci.Shop.Aplication.Validators.Category;
using Ahlatci.Shop.Domain.Repositories;
using Ahlatci.Shop.Domain.UWork;
using Ahlatci.Shop.Persistence.Context;
using Ahlatci.Shop.Persistence.Repository;
using Ahlatci.Shop.Persistence.UWork;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//loging
var configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .Build();
Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

Log.Logger.Information("program start");
// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new ExceptionHandlerFilter());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JwtTokenWithIdentity", Version = "v1", Description = "JwtTokenWithIdentity test app" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
});

//UWork registiration

builder.Services.AddScoped<IUWork, UWork>();
//repository Registiration

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//db context Registirationn
builder.Services.AddDbContext<AhlatciContext>(opt =>
{
    // opt.UseSqlServer(builder.Configuration["ConnectionStrings:AhlatciShop"]); tüm okumalarda yapýlabilir 
    //connection sýnýfýna özel
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AhlatciShop"));
});

// business service Registiration
builder.Services.AddScoped<ICategoryService, CategorySevice>();
builder.Services.AddScoped<IAccountService, AccountService>();

//automapper
builder.Services.AddAutoMapper(typeof(DomainToDtoModel), typeof(ViewModelToDomain));

//Fluendvalidation istekle gönderilen modele ait Proportlerin istenilen formatta olup olmadýðýný kontrol eder
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateCategoryValidator));


// jwt kimlik doðrulama servisi ekleme
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = builder.Configuration["Jwt:Issuer"],
               ValidAudience = builder.Configuration["Jwt:Audiance"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]))
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

app.MapControllers();

app.Run();
