using Microsoft.EntityFrameworkCore;
using WizardsoftTest.Server.Interfaces;
using WizardsoftTest.Server.Repositories;
using AppContext = WizardsoftTest.Server.Database.AppContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppContext>(options => options.UseSqlite("Data Source=Database/app.db"));
builder.Services.AddTransient<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();