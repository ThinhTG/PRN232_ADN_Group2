using ADN_Group2.BusinessObject.Identity;
using ADN_Group2.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Net.payOS;
using Repository.DBContext;
using Repository.Repository;
using Service;
using Service.Auth;
using Service.Interface;
using System.Reflection;
using System.Text;

namespace ADN_Group2
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add DbContext
			builder.Services.AddDbContext<ADNDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			// Add Identity
			builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
				.AddEntityFrameworkStores<ADNDbContext>()
				.AddDefaultTokenProviders();

			// Add JWT Authentication
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
					ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
					ValidAudience = builder.Configuration["JwtSettings:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
				};
			});
            PayOS payOS = new PayOS(builder.Configuration["PayOS:ClientId"] ?? throw new Exception("Cannot find payment environment"),
                    builder.Configuration["PayOS:ApiKey"] ?? throw new Exception("Cannot find payment environment"),
                    builder.Configuration["PayOS:ChecksumKey"] ?? throw new Exception("Cannot find payment environment"));
            builder.Services.AddSingleton(payOS);

            // Swagger Configuration
            builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "ADN",
					Version = "v1",
					Description = "Services to ADN Website"
				});

				// Thm h? tr? XML Comments
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Please enter a valid token in the following format: {your token here} do not add the word 'Bearer' before it."
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
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header,
						},
						new List<string>()
					}
				});
			});


			// Add CORS services
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowReactApp", builder =>
				{
					builder.WithOrigins("http://localhost:5173") // Allow your frontend origin
						   .AllowAnyHeader()
						   .AllowAnyMethod()
						   .AllowCredentials(); // If you need to send cookies or auth headers
				});
			});

			// Register DI for Repository and Service
			builder.Services.AddScoped<UserRepository>();
			builder.Services.AddScoped<AuthService>();
			builder.Services.AddScoped<IAddressRepository, AddressRepository>();
			builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
			builder.Services.AddScoped<IAppointmentService, AppointmentService>();
			builder.Services.AddScoped<IBlogRepository, BlogRepository>();
			builder.Services.AddScoped<IBlogService, BlogService>();
			builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IKitRepository, KitRepository>();
			builder.Services.AddScoped<IKitService, KitService>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<ITestPersonRepository, TestPersonRepository>();
			builder.Services.AddScoped<ITestPersonService, TestPersonService>();
			builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
			builder.Services.AddScoped<IFeedbackService, FeedbackService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			// Use CORS before Authentication and Authorization
			app.UseCors("AllowReactApp");

			app.UseAuthentication();
			app.UseAuthorization();

			// Seed roles when the application starts
			using (var scope = app.Services.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
				string[] roles = new[] { "Guest", "Customer", "Staff", "Manager", "Admin" };
				foreach (var role in roles)
				{
					if (!await roleManager.RoleExistsAsync(role))
					{
						await roleManager.CreateAsync(new ApplicationRole { Name = role });
					}
				}
			}
            app.UseSimpleExceptionMiddleware();
            app.MapControllers();

			app.Run();
		}
	}
}