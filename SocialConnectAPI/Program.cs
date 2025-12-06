using BAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Net.Http.Headers;
using MODEL.CommonConfig;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var appsettings = new AppSetting();
builder.Configuration.GetSection("AppSetting").Bind(appsettings);
ServiceManager.SetServiceInfo(builder.Services, appsettings);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins", policy =>
        policy.WithMethods("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS")
              .WithHeaders(HeaderNames.Accept, HeaderNames.ContentType, HeaderNames.Authorization)
              .SetIsOriginAllowed(origin => true)
              .AllowCredentials());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = "SocialConnectAPI",
        ValidAudience = "SocialConnectAPI",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes("^6L@QAWsW>eUk/q|X38fHu?#=6YTWWZWXw[0T/C^O84R48NC@9p{1,~)M?2s{so"))
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SocialConnectAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("MyAllowSpecificOrigins");

app.MapControllers();

app.Run();
