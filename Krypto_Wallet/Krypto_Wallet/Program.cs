using Krypto_Wallet.Data;
using Krypto_Wallet.Interfaces;
using Krypto_Wallet.Repositories;
using Krypto_Wallet.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавить DbContext для подключения к базе данных
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация репозиториев и сервисов
builder.Services.AddScoped<ICryptoQueryRepository, CryptoQueryRepository>();
builder.Services.AddScoped<ICryptoQueryService, CryptoQueryService>();

// Добавление контроллеров
builder.Services.AddControllers();

// Добавление Swagger для документации API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Настройка конвейера обработки запросов
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
