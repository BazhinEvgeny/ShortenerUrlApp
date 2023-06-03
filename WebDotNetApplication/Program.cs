using Microsoft.AspNetCore.Mvc;
using LiteDB;
using WebDotNetApplication.Middlewares;
using WebDotNetApplication.Services;
using IUrlHelper = WebDotNetApplication.Services.IUrlHelper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILiteDatabase, LiteDatabase>(_ => new LiteDatabase("shortner-service.db"));
builder.Services.AddScoped<IDbContext, DbContext>();
builder.Services.AddScoped<IUrlHelper, UrlHelper>();
builder.Services.AddScoped<IShortUrlService, ShortUrlService>();
builder.Services.Configure<MvcOptions>(cfg => cfg.Filters.Add<ExceptionHandlingMiddleware>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();