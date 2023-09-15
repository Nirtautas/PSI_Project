using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Data;

var builder = WebApplication.CreateBuilder(args);

//Established connection with PostgreSQL database
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IUserContainer, UserContainer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

RelationContainer relationContainer = RelationContainer.Instance;
CourseContainer courseContainer = CourseContainer.Instance;
UserContainer userContainer = UserContainer.Instance;

ContainerHelper.FetchLocalData(relationContainer, courseContainer, userContainer);
ContainerHelper.PrintRelationalList(courseContainer, userContainer);
ContainerHelper.WriteLocalData(relationContainer, courseContainer, userContainer);

app.Run();
