using BusinessLogic.Exstensions;
using DataAccess;
using GoogleLogin.Extentions;
using GoogleLogin.Middlewares;


var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var connStr = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddShopDbContext(connStr);
builder.Services.AddBusinessLogicServices();
builder.Services.AddMainServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowOrigins");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
