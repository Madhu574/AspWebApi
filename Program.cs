//using EmployeeDemoProject.IRepositories;
//using EmployeeDemoProject.Repositories;
//using EmployeeMCrud.IRepositories;
//using EmployeeMCrud.Models;
//using EmployeeMCrud.Repositories;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.FileProviders;
//using Microsoft.OpenApi.Models;
//using Newtonsoft.Json.Serialization;
//using Swashbuckle.AspNetCore.SwaggerUI;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.


//    builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
//builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
//builder.Services.AddScoped<IDesignationRepository, DesignationRepository>();

//builder.Services.AddDbContext<APIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeAppCon")));
//builder.Services.AddSwaggerGen(options => {
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "WEB API",
//        Version = "v1"
//    });
//});
////Enable CORS
//builder.Services.AddCors(c => {
//    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//});
////JSON Serializer
//builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());


//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();

//}
//app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


//app.UseHttpsRedirection();

//app.UseRouting();

//app.UseAuthorization();
//app.UseEndpoints(endpoints => {
//    endpoints.MapControllers();
//});
////app.MapControllers();
//app.UseSwagger();
//app.UseSwaggerUI(c => {
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEB API");
//    c.DocumentTitle = "WEB API";
//    c.DocExpansion(DocExpansion.List);
//});

//app.Run();

using EmployeeDemoProject.IRepositories;
using EmployeeDemoProject.Repositories;
using EmployeeMCrud.IRepositories;
using EmployeeMCrud.Models;
using EmployeeMCrud.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDesignationRepository, DesignationRepository>();

builder.Services.AddDbContext<APIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeAppCon")));

builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WEB API",
        Version = "v1"
    });
});

// Enable CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowOrigin", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// JSON Serializer
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });

builder.Services.AddControllers();

// Enable Swagger and SwaggerUI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowOrigin");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEB API");
    c.DocumentTitle = "WEB API";
    c.DocExpansion(DocExpansion.List);
});

app.Run();
