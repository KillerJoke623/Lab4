using Lab4.Data;
using Lab4.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add our db
builder.Services.AddDbContext<EducationContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();



builder.Services.AddTransient<SellerService>();
builder.Services.AddTransient<CarPartService>();
builder.Services.AddTransient<CustomerService>();

builder.Services.AddControllers().AddNewtonsoftJson(options=>
    options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.Run();