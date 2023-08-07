using Crawler.Infra.Components;
using Crawler.Application;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.InjectServices(builder.Configuration); 
builder.Services.AddInfraComponents(builder.Configuration);
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
app.ConfigureMiddlewares();

app.Run();
