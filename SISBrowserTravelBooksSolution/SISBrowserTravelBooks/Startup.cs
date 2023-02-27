using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NUnit.Framework;
using SISBrowserTravelBooks.DataAccess.Dacs;
using SISBrowserTravelBooks.DataAccess.Interfaces;
using SISBrowserTravelBooks.Services.Interfaces;
using SISBrowserTravelBooks.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SISBrowserTravelBooks.Entities.ModelsConfiguration;
using System.Text;

namespace SISBrowserTravelBooks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IServiceCollection ServiceCollection { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region INYECTION DAC PROJECT
            services.AddScoped<IConnectionDac, ConnectionDac>();
            services.AddScoped<IAuthorsDac, AuthorsDac>();
            services.AddScoped<IBooksDac, BooksDac>();
            services.AddScoped<IEditorialsDac, EditorialsDac>();
            #endregion

            #region INYECTION SERVICE PROJECT
            services.AddScoped<IAuthorsService, AuthorsService>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IEditorialsService, EditorialsService>();
            services.AddScoped<IUsersService, UsersService>();
            #endregion

            #region SECURITY IN JWT
            var settings = this.Configuration.GetSection("Jwt");
            JwtModel jwtSecurity = settings.Get<JwtModel>();
            var llave = Encoding.UTF8.GetBytes(jwtSecurity.Secreto);

            services
                .AddHttpContextAccessor()
                .AddAuthorization()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(llave)
                    };
                });

            services.AddCors();

            #endregion

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SISBrowserTravelBooks", Version = "v1" });
            });

            this.ServiceCollection = services;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SISBrowserTravelBooks v1"));
            }
            else
            {
                app.UseCors(options =>
                {
                    options.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHttpsRedirection();       

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDefaultFiles();

            app.UseStaticFiles();
        }
    }
}
