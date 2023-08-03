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
using Microsoft.EntityFrameworkCore;
using Serilog;

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

builder.Services.AddControllers(opt=>
{
    opt.Filters.Add(new ExceptionHandlerFilter());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//UWork registiration

builder.Services.AddScoped<IUWork, UWork>();
//repository Registiration

builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
//db context Registirationn
builder.Services.AddDbContext<AhlatciContext>(opt =>
{
    // opt.UseSqlServer(builder.Configuration["ConnectionStrings:AhlatciShop"]); tüm okumalarda yapýlabilir 
    //connection sýnýfýna özel
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AhlatciShop"));
});

// business service Registiration
builder.Services.AddScoped<ICategoryService,CategorySevice>();


//automapper
builder.Services.AddAutoMapper(typeof(DomainToDtoModel),typeof(ViewModelToDomain));

//Fluendvalidation istekle gönderilen modele ait Proportlerin istenilen formatta olup olmadýðýný kontrol eder
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateCategoryValidator));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
