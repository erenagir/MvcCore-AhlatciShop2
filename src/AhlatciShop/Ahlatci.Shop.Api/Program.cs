using Ahlatci.Shop.Aplication.Services.Abstraction;
using Ahlatci.Shop.Aplication.Services.Implementation;
using Ahlatci.Shop.Persistence.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AhlatciContext>(opt =>
{
    // opt.UseSqlServer(builder.Configuration["ConnectionStrings:AhlatciShop"]); t�m okumalarda yap�labilir 
    //connection s�n�f�na �zel
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AhlatciShop"));
});
// business service Registiration
builder.Services.AddScoped<ICategoryService,CategorySevice>();

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
