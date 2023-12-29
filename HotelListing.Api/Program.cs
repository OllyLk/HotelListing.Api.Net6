using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS policy.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

// Serilog logging.
builder.Host.UseSerilog((ctx, logconfig) => logconfig.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
