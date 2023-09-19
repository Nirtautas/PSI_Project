using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Data;

RelationContainer relationContainer = new RelationContainer();
CourseContainer courseContainer = new CourseContainer(relationContainer);
UserContainer userContainer = new UserContainer(relationContainer);
CommentContainer commentContainer = new CommentContainer();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ICourseContainer, CourseContainer>();
builder.Services.AddSingleton<IUserContainer, UserContainer>();
builder.Services.AddSingleton<IRelationContainer, RelationContainer>();
builder.Services.AddSingleton<ICommentContainer, CommentContainer>();
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

app.Run();

