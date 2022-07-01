using ConfigurationSample.Options;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

//builder.Services.Configure<LocationOptions>(c =>
//{
//    c.Province = "fars";
//    c.City= "shiraz";
//});

builder.Services.Configure<LocationOptions>(builder.Configuration.GetSection("Location"));

CourseOption courseOption = new CourseOption();
builder.Configuration.Bind("Course",courseOption);
//builder.Configuration.GetSection("Course").Bind(courseOption);
builder.Services.AddSingleton<CourseOption>(courseOption);

var app = builder.Build();

app.MapGet("/ioption1", async (HttpContext httpContext, IOptions<LocationOptions> options ) =>
{
    httpContext.Response.StatusCode = 200;
    httpContext.Response.ContentType = "text/html";
    await httpContext.Response.WriteAsJsonAsync($"<h1>{options.Value.Province} -- {options.Value.City} </h1>");
});
app.MapGet("/ioption2", async (HttpContext httpContext, IOptions<LocationOptions> options) =>
{
    httpContext.Response.StatusCode = 200;
    httpContext.Response.ContentType = "text/html";
    await httpContext.Response.WriteAsJsonAsync($"<h1>{courseOption.CourseName} -- {courseOption.TeacherName} </h1>");
});

//app.MapGet("/username", async (HttpContext httpContext,IConfiguration configuration) =>
//{
//    var username = configuration["CustomerName"];
//    httpContext.Response.StatusCode = 200;
//    httpContext.Response.ContentType = "text/html";
//    await httpContext.Response.WriteAsJsonAsync($"<h1>{username}</h1>");
//});

//app.MapGet("/city", async (HttpContext httpContext, IConfiguration configuration) =>
//{
//    var city = configuration["Location:Province"];
//    httpContext.Response.StatusCode = 200;
//    httpContext.Response.ContentType = "text/html";
//    await httpContext.Response.WriteAsJsonAsync($"<h1>{city}</h1>");
//});

//app.MapGet("/section", async (HttpContext httpContext, IConfiguration configuration) =>
//{
//    var locationSectionConfig = configuration.GetSection("Location");
//    var city = locationSectionConfig["City"];
//    httpContext.Response.StatusCode = 200;
//    httpContext.Response.ContentType = "text/html";
//    await httpContext.Response.WriteAsJsonAsync($"<h1>{city}</h1>");
//});

//app.MapGet("/sectionlog", async (HttpContext httpContext, IConfiguration configuration) =>
//{
//    var locationSectionConfig = configuration.GetSection("Logging:LogLevel");
//    var Default = locationSectionConfig["Default"];
//    httpContext.Response.StatusCode = 200;
//    httpContext.Response.ContentType = "text/html";
//    await httpContext.Response.WriteAsJsonAsync($"<h1>{Default}</h1>");
//});


app.MapGet("/", () => "Hello World!");

app.Run();

