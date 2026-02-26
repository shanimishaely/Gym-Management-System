using GymAPI;
using GymAPI.Core;
using GymAPI.Core.Repositories;
using GymAPI.Core.Services;
using GymAPI.Data.Repositories;
using GymAPI.Service;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GymAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// --- 1. äâãøú ôåìéñú CORS (äåñôä çãùä!) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLovable", policy =>
    {
        policy.AllowAnyOrigin() // îàôùø ìëì àúø (ëåìì Lovable) ìâùú
              .AllowAnyMethod() // îàôùø GET, POST, PUT, DELETE åëå'
              .AllowAnyHeader(); // îàôùø ìùìåç èå÷ðéí á-Header
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

// Dependency Injection
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IRegisterForLessonService, RegisterForLessonService>();
builder.Services.AddScoped<IRegisterForLessonRepository, RegisterForLessonRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<DataContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

var app = builder.Build();

// --- 2. ñãø ä-Middleware (çùåá îàåã!) ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// äôòìú ä-CORS - çééá ìáåà ìôðé Authentication å-Authorization!
app.UseCors("AllowLovable");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

//using GymAPI;
//using GymAPI.Core;
//using GymAPI.Core.Repositories;
//using GymAPI.Core.Services;
//using GymAPI.Data.Repositories;
//using GymAPI.Service;
//using Microsoft.OpenApi.Models;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using GymAPI.Data;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Name = "Authorization",
//        Description = "Bearer Authentication with JWT Token",
//        Type = SecuritySchemeType.Http
//    });
//    options.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Id = "Bearer",
//                    Type = ReferenceType.SecurityScheme
//                }
//            },
//            new List<string>()
//        }
//    });
//});

//builder.Services.AddScoped <ILessonService, LessonService>();
//builder.Services.AddScoped<ILessonRepository, LessonRepository>();

//builder.Services.AddScoped<IRegisterForLessonService, RegisterForLessonService>();
//builder.Services.AddScoped<IRegisterForLessonRepository, RegisterForLessonRepository>();

//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();

//builder.Services.AddAutoMapper(typeof(MappingProfile));
//builder.Services.AddDbContext<DataContext>();


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["JWT:Issuer"],
//            ValidAudience = builder.Configuration["JWT:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
//        };
//    });


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthentication();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
