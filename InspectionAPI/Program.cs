using InspectionAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//ConnectionsStrings
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings: DefaultConnection"]);
});

// Enable Cors
var myAllowSpecificOrings = "_myAllowSpecificOrings";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrings,
        builder =>
        {
            builder.WithOrigins("http://localhost:5078")
            .AllowAnyMethod()
            .AllowAnyHeader();

        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
