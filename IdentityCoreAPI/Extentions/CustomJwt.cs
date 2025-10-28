using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityCoreAPI.Extentions
{
    public static class CustomJwt
    {
        public static void AddCustomJwt(this IServiceCollection servise, ConfigurationManager conf)
        {
            servise.AddAuthentication(o =>
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
                    ValidateIssuer = true,
                    ValidIssuer = conf["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = conf["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:SecretKey"]))
                };
            });
        }
    }
}
