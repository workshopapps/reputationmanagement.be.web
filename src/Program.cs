using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using src;
using src.Data;
using src.Entities;
using src.Models;
using src.Services;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using src.Models.Dtos;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Hosting;
using Hellang.Middleware.ProblemDetails;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
        ValidAudience = jwtSettings.GetSection("validAudience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
    };
});

// only return json.
builder.Services.AddControllers(setupAction =>
{
    setupAction.ReturnHttpNotAcceptable = true;

})
.AddNewtonsoftJson(setupAction =>
{
    setupAction.SerializerSettings.ContractResolver =
       new CamelCasePropertyNamesContractResolver();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    policy =>
    {
	    policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddTransient<IBufferedFileUploadService, BufferedFileUploadLocalService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var AuthConnString = builder.Configuration.GetConnectionString("DefaultAuthConnection");
var connstring = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppIdentityDbContext>(options => {
    options.UseMySql(AuthConnString, ServerVersion.AutoDetect(AuthConnString));
});

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseMySql(connstring, ServerVersion.AutoDetect(connstring));
});

//Configuring Problem Details
builder.Services.AddProblemDetails();

// Configuring FluentValidation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters().AddValidatorsFromAssemblyContaining<ResetPasswordDto>();

// Use role base auth.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
opts =>
{
    opts.User.RequireUniqueEmail = true;
    opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMOPQRSTUVWXYZ1234567890!()_.-$@+";
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
})
.AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IReviewRepository, AzSqlReviewRepo>();
builder.Services.AddTransient<IQuoteRepository, QuoteRepo>();



// Allower Swagger to deal with JWT Auth fluently
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { }}
    });
    c.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();
builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();
builder.Services.Configure<MailKitEmailSenderOptions>(
builder.Configuration.GetSection(nameof(MailKitEmailSenderOptions)));
builder.Services.AddResponseCaching();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseStatusCodePages();
}
app.UseProblemDetails();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();

await SeedDb.Seed(app, app.Configuration);

app.Run();

public partial class Program { }