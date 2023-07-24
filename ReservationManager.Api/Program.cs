using ReservationManager.Api.Filters;
using ReservationManager.Domain.Models.ConfigurationModels.Interfaces;
using ReservationManager.Infrastructure.Email.Interfaces;
using ReservationManager.Persistence.DataContext;
using ReservationManager.Repository.IRepositories;
using ReservationManager.Repository.Repositories;
using ReservationManager.Service.IServices;
using ReservationManager.Service.Services;
using System.Text.Json;
using ReservationManager.Domain.Models.ConfigurationModels.Concretes;
using ReservationManager.Infrastructure.Email.Concrete;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new TransactionScopeFilter());
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

#region DbContext 
builder.Services.AddDbContext<ReservationManagerDbContext>();
#endregion

#region Repositories DI Configuration
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<ICommonRepository, CommonRepository>();
#endregion

#region Services DI Configuration
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IBaseService, BaseService>();
#endregion

#region Smtp Information Model DI Configuration

builder.Services.AddSingleton<ISmtpConfigurationModel, SmtpConfigurationModel>(opt =>
{
    return builder.Configuration.GetSection("SmtpInformation").Get<SmtpConfigurationModel>();
});
builder.Services.AddSingleton<IEmailSender, EmailSender>();
#endregion

var app = builder.Build();

#region Exception Handler Zone
app.UseExceptionHandler(a => a.Run(async context =>
{
    string content = JsonSerializer.Serialize(
        new
        {
            IsSuccess = false,
            Message = "Üzgünüz, Ýþlem Sýrasýnda Teknik Sorunlar Oluþmuþtur."
        });
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(content);
}));
#endregion

app.UseAuthorization();

app.MapControllers();

app.Run();
