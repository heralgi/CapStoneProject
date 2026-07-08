using InsurancePolicyApi.Data;
using InsurancePolicyApi.Entities;
using InsurancePolicyApi.Repositories;
using InsurancePolicyApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using System.Text;

namespace InsurancePolicyApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<IPlanService, PlanService>();

            builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();
            builder.Services.AddScoped<IPolicyService, PolicyService>();
            builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
            builder.Services.AddScoped<IClaimService, ClaimService>();

            builder.Services.AddScoped<IClaimDocumentRepository, ClaimDocumentRepository>();
            builder.Services.AddScoped<IClaimDocumentService, ClaimDocumentService>();
            builder.Services.AddScoped<IClaimStatusHistoryRepository, ClaimStatusHistoryRepository>();
            builder.Services.AddScoped<IClaimStatusHistoryService, ClaimStatusHistoryService>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //get key from appsettings
            var mysecretkey = builder.Configuration["JWT:Key"] ?? throw new InvalidOperationException("JWT Secret key is missing");

            //add the Authentication Service
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                //Create Token validation Parameters - and check those when token is sent by user in request
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mysecretkey))
                };
            });

            //Configure the Service for JWT params to be available from appsettings.json anywhere\
            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));

            // Add Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //SwaggerGenServiceCollectionExtensions.AddSwaggerGen(builder.Services);

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Movie Rental App"
                }
                );

                //Define Security Scheme for Jwt Bearer Tokens
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter Jwt Bearer Token Only",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    //define Security Scheme                     
                };

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token Like : Bearer {your Token}"
                });

                //Apply The Security
                options.AddSecurityRequirement(new OpenApiSecurityRequirement     {
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
                // options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);//Add Security Requirement
                options.AddSecurityRequirement(new OpenApiSecurityRequirement     {
                     { securityScheme , new string[]{ }
                }
            });
            });

            builder.Services.AddAuthorization();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
