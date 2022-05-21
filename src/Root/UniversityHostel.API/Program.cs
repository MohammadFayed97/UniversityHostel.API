using CommonLibrary.AssemplyScanning;
using CommonLibrary.Server;
using CommonLibrary.Server.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddInstallerFromAssembly<Program>(builder.Configuration);
builder.Services.AddInstallerFromReferancedAssemblies(builder.Configuration, typeof(Program).Assembly, "*.Server.dll");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    ApplicationContext context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    if (builder.Configuration.GetValue<bool>("AppSettings:Migrate"))
        context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "University Hostel v1.0");
});

app.UseHttpsRedirection();

app.UseApiExceptionHandler(options =>
{
    options.Product = "UniversityHostel";
    options.Layer = "API";
});

app.UseCors("_myAllowSpecificOrigins");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
