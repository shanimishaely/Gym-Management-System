using GymAPI;
using GymAPI.Core;
using GymAPI.Core.Repositories;
using GymAPI.Core.Services;
using GymAPI.Data.Repositories;
using GymAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped <ILessonService, LessonService>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();

builder.Services.AddScoped<IRegisterForLessonService, RegisterForLessonService>();
builder.Services.AddScoped<IRegisterForLessonRepository, RegisterForLessonRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<DataContext>();


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
