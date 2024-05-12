using JWTHandsonAllCase.Core.Extentions;
using JWTHandsonAllCase.Core.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddJwtServices(builder.Configuration);

// Add services to the container.

builder.Services.AddAuthentication(j =>
{
    j.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    j.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    j.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["JWT:Audience"].ToString(),
        ValidIssuer = builder.Configuration["JWT:Issure"].ToString(),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SignInKey"])),
    };
   

});


builder.Services.AddControllers();
builder.Services.AddedServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme,
                },
                In = ParameterLocation.Header,
                Scheme = "Bearer",
                Name = "Authorization"
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(s =>
{
    s.AddPolicy("AllowOrigin", allow =>
    {
        allow.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<CheckBlackListTokenMiddleware>(); 
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowOrigin");

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
