using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SchoolDBCoreWebAPI.Models;
using SchoolDBCoreWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //For CamelCase(default for JSON in .net
        //options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        //For PascalCase (matches C# property names)
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAutheticationHandler>(
        "BasicAuthentication", options => { }
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasicAuth", Version = "v1" });
    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Basic",
        In = ParameterLocation.Header,
        Description = "Basic Autherization header using the Bearer Scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Basic"
            }
        },
        new string[] { }  
    }


});
   });
builder.Services.AddDbContext<SchoolDBContext>(options =>
                                    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolCon")));
builder.Services.AddScoped<SchoolDAL>();

//------------------------CORS------------------------

builder.Services.AddCors();


//----------------------------------------BUILDING------------------------------

var app = builder.Build(); //gives webapp object

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
