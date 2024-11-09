using Krypto_Wallet.Data;
using Krypto_Wallet.Interfaces;
using Krypto_Wallet.Repositories;
using Krypto_Wallet.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Подключение к базе данных MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Регистрация сервисов и репозиториев
builder.Services.AddScoped<ICryptoPriceRepository, CryptoPriceRepository>();
builder.Services.AddSingleton<ICryptoCompareApiService, CryptoCompareApiService>(provider =>
    new CryptoCompareApiService("8e870f3144ea7e864b88d3690940a4b8b887c51dbcebb257e2e97e0d938ba576"));

// Добавление контроллеров и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Настройка среды разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
