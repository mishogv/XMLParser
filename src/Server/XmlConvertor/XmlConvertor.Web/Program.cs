using XmlConvertor.Web.Middlewares;
using XmlConvertor.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IXmlConvertorService, XmlConvertorService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler(errorApp => errorApp.Run(errorApp.ApplicationServices.GetRequiredService<ExceptionMiddleware>().Get));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();