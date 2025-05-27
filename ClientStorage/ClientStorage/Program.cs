using ClientStorage.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register DbContext with PostgresSQL
builder.Services.AddDbContext<ClientDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Register classes and interfaces
builder.Services.AddScoped<ClientRepository>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Client Storage v1");
    c.RoutePrefix = "swagger";
});

// Migrate Database


app.Run();

