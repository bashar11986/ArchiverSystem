using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace IdentityCoreAPI.Extentions
{
    // i want authontication work on swagger , i want jwt not cookies
    public static class CustomJwt
    {
        public static void AddCustomJwt(this IServiceCollection service, ConfigurationManager conf)
        {
            service.AddAuthentication(o =>
            {
                // for search token in the right place:
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  // for write bearer then token in post man for example
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;   // for go to login if unauthoriz
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;   // other schemes in services have to work on jwtbearerschema
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false; // if true, it runs on just https
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters()  // validate parameters in configuration file : Issuer, Audience ..
                {
                    ValidateIssuer = true,   // true: must validate issuer
                    ValidIssuer = conf["JWT:Issuer"],  // must have value if ValidateIssuer = true
                    ValidateAudience = false,
                    ValidAudience = conf["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:SecretKey"]))
                };
            });
        }
        public static void AddSwaggerGenJwtAuth(this IServiceCollection service)
        {
            service.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Identity Server Project",
                    Description = "one project contains identity APIs",
                    Contact = new OpenApiContact()
                    {
                        Name = "bashar ibrahim",
                        Email = "bashar11986@gmail.com",
                        Url = new Uri("https://github.com/bashar11986?tab=repositories")

                    }
                });
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "inter jwt key like this: Bearer [space] yourtokentext"

                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>() }
                }); 
            });
        }// end function
    } // end class
} // end namespace
