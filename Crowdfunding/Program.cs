
using Crowdfunding.Services;
using Datastore.implementation;
using Datastore.implementation.Repository;
using DataStore.Abstraction.IRepository;
using DataStore.Abstraction.IRepositry;
using DataStore.Abstraction.Repositories;
using DataStore.Abstraction.Repository;
using DataStore.Implementation.Repositories;
using DataStore.Implementation.Repository;
using FeatureObject.Abstraction.Manager;
using FeatureObjects.Abstraction.Managers;
using FeatureObjects.Implementation.Manager;
using FeatureObjects.Implementation.Managers;
using FeauterObject.abstraction.Manager;
using FeauterObject.abstraction.Services;
using FeauterObject.Implemetation.Manager;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Datastore.Implementation.Repository;
using Crowdfunding.Middleware;


//using Crowdfunding.Exceptions;

namespace Crowdfunding
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          ;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])), 

                        ValidateIssuer = true, 
                        ValidIssuer = builder.Configuration["Jwt:Issuer"], 

                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"], 

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero 
                    };
                });

            builder.Services.AddAuthorization();

            

            builder.Services.AddControllers();
            builder.Services.AddScoped<DapperContext>();
            builder.Services.AddScoped<ILoginManager, LoginManager>();
            builder.Services.AddScoped<ILogin,Login>();
            builder.Services.AddScoped<ISignUpManager, SignUpManager>();
            builder.Services.AddScoped<ISignUpRepo, SignUpRepo>();
            builder.Services.AddScoped<IHomePageRepo, HomePageRepo>();
            builder.Services.AddScoped<IHomePageManager, HomePageManager>();
            builder.Services.AddScoped<ICampaignDetailsManager, CampaignDetailManager>();
            builder.Services.AddScoped<ICampaignDetailsRepo, CampaignDetailsRepo>();
            builder.Services.AddScoped<IPostCampaignManager, PostCampaignManager>();
            builder.Services.AddScoped<IPostCampaign,PostCampaignRepo>();
            builder.Services.AddScoped<IInvestmentRepository, InvestmentRepository>();
            builder.Services.AddScoped<IInvestmentCalcManager, InvestmentCalcManager>();
            builder.Services.AddScoped<IPasswordHasher,PasswordHasher>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IInvestmentDetailsManager, InvestmentDetailsManager>();
            builder.Services.AddScoped<IInvestmentDetailsRepo,InvestmentDetailsRepo>();
            builder.Services.AddScoped<IInvestmentsListsManager, InvestmentsListsManager>();
            builder.Services.AddScoped<IInvestmentsListsRepo, InvestmentsListsRepo>();
            builder.Services.AddScoped<IMyCampaignManager,MyCampaignManager>();
            builder.Services.AddScoped<IMyCampaignRepository,MyCampaignRepository>();
            builder.Services.AddScoped<IDeleteCampaignRepo, DeleteCampaignRepo>();
            builder.Services.AddScoped<IDeleteCampaignManager, DeleteCampaignManager>();

            


            //builder.Services.AddScoped<ICampaignDashboardManager, CampaignDashboardManager>();
            //builder.Services.AddScoped<ICampaignDashboardRepo,CampaignDashboardRepo>();

            //builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin()  // Allows all origins
                              .AllowAnyMethod()   // Allows all HTTP methods (GET, POST, PUT, DELETE, etc.)
                              .AllowAnyHeader();  // Allows all headers
                    });
            });


            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseExceptionHandler(options => { });
            app.UseHttpsRedirection();

            


            app.MapControllers();
            

            app.Run();
        }
    }
}
