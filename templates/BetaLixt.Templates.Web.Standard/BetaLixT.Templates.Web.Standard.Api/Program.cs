using BetaLixT.Templates.Web.Standard.Utility.Helpers;
using BetaLixT.Templates.Web.Standard.Domain;
using BetaLixT.Templates.Web.Standard.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterDomainServices();
builder.Services.RegisterDataServices();
{
    
}
builder.Services.AddSingleton<InitializerHelper>();
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// - Running all registered initializers
var initHelper = app.Services.GetRequiredService<InitializerHelper>();
initHelper.RunInitializers();

app.Run();

