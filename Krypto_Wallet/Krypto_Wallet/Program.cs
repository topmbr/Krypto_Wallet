using Krypto_Wallet.Data;
using Krypto_Wallet.Interfaces;
using Krypto_Wallet.Repositories;
using Krypto_Wallet.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// �������� DbContext ��� ����������� � ���� ������
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

// ����������� ������������ � ��������
builder.Services.AddScoped<ICryptoQueryRepository, CryptoQueryRepository>();
builder.Services.AddScoped<ICryptoQueryService, CryptoQueryService>();

// ���������� ������������
builder.Services.AddControllers();

// ���������� Swagger ��� ������������ API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ��������� ��������� ��������� ��������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
