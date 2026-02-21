using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(opt =>
opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddHostedService<CleanupWorker>();


builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(InfrastructureProfile));
builder.Services.AddLogging(config => {config.AddConsole(); config.SetMinimumLevel(LogLevel.Information);});
builder.Services.AddSwaggerGen(options=>{
     options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    });

//mardakvori amijawa nafahmidm ay chat gpt prsidm neki xdm navistmw.
builder.Services.AddCors(options => {
    options.AddPolicy("AddFrontend", policy => {
        policy.WithOrigins("http://localhost:3000")
              .WithMethods("GET", "POST")
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AddFrontend"); //ijawam ki kor kna
app.MapControllers();
app.UseHttpsRedirection();
app.Run();