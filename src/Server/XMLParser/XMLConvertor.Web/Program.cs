using XMLConvertor.Web.Middlewares;
using XMLConvertor.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IXMLConvertorService, XMLConvertorService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<ExceptionMiddleWare>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler(errorApp => errorApp.Run(errorApp.ApplicationServices.GetRequiredService<ExceptionMiddleWare>().Get));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();