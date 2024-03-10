using My_Tube.Core.IServices;
using My_Tube.Core.Modoles;
using My_Tube.Core.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<MyTubeKey>(builder.Configuration.GetSection("MyTube"));
builder.Services.AddScoped<IMyTubeClientService, MyTubeClientService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
