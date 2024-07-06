using Financial.Domain;
using Financial.Domain.Interfaces;
using Financial.Infra;
using Financial.Infra.Repos;
using Financial.Infra.Repos.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.RateLimiting;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string conn = builder.Configuration["ConnectionStrings:DatabaseConn"] ?? throw new NullReferenceException("ConnectionStrings:DatabaseConn");

builder.Services.AddMySql<FinancialDbContext>(conn, ServerVersion.AutoDetect(conn));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IInitializeDBRepo, InitializeDBRepo>();

builder.Services.AddScoped<IInitializeDbService, InitializeDbService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    op =>
    {
        op.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = $"1.2",
            Title = "Financial Server",
            Description = "Routes of apis for financial app",
        });
    }
    );

#region Auth configs

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"] ?? throw new NullReferenceException("JwtKey")))
    };
    options.SaveToken = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthorization();

#endregion

#region Rate Limit Confis

builder.Services.AddRateLimiter(options => options.AddFixedWindowLimiter(policyName: "fixed", options =>
{
    options.PermitLimit = 4;
    options.Window = TimeSpan.FromSeconds(12);
    options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    options.QueueLimit = 2;
}).OnRejected = async (context, token) =>
{
    context.HttpContext.Response.StatusCode = 429;

    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
    {
        await context.HttpContext.Response.WriteAsync(
            $"Too many requests. Please try again after {retryAfter.TotalMinutes} minute(s).", cancellationToken: token
            );
    }
    else
    {
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later");
    }
}
);

#endregion

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

//
app.UseRateLimiter();
app.UseAuthentication();
app.UseHttpsRedirection();
//

app.UseAuthorization();

app.MapControllers().RequireRateLimiting("fixed");

app.Run();
