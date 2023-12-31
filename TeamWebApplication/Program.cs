using TeamWebApplicationAPI.Data.ExceptionLogger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDataLogger, DataLogger>();
builder.Services.AddAutoMapper(typeof(Program), typeof(TeamWebApplicationAPI.Data.Mapping.MappingProfile));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseMiddleware<SessionCredentialExceptionHandlerMiddleware>();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.UseEndpoints(endpoints =>
{
    //Course Environment
    endpoints.MapControllerRoute(
        name: "CourseEnvironmentIndex",
        pattern: "CourseEnvironment/Index/{courseId}",
        defaults: new { controller = "CourseEnvironment", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "CourseEnvironmentCreateFilePost",
        pattern: "CourseEnvironment/CreateFilePost/{courseId}",
        defaults: new { controller = "CourseEnvironment", action = "CreateFilePost" }
    );

    endpoints.MapControllerRoute(
        name: "CourseEnvironmentEditFilePost",
        pattern: "CourseEnvironment/EditFilePost/{postId}",
        defaults: new { controller = "CourseEnvironment", action = "EditFilePost" }
    );

    endpoints.MapControllerRoute(
        name: "CourseEnvironmentCreateTextPost",
        pattern: "CourseEnvironment/CreateTextPost/{courseId}",
        defaults: new { controller = "CourseEnvironment", action = "CreateTextPost" }
    );

    endpoints.MapControllerRoute(
        name: "CourseEnvironmentEditTextPost",
        pattern: "CourseEnvironment/EditTextPost/{postId}",
        defaults: new { controller = "CourseEnvironment", action = "EditTextPost" }
    );

	endpoints.MapControllerRoute(
    	name: "CourseEnvironmentDeleteTextPost",
	    pattern: "CourseEnvironment/DeleteTextPost/{postId}",
	    defaults: new { controller = "CourseEnvironment", action = "DeleteTextPost" }
    );

	endpoints.MapControllerRoute(
        name: "CourseEnvironmentAddComment",
        pattern: "CourseEnvironment/AddComment/{courseId}",
        defaults: new { controller = "CourseEnvironment", action = "AddComment" }
    );

    endpoints.MapControllerRoute(
        name: "CourseEnvironment",
        pattern: "CourseEnvironment/DeleteComment/{courseId}",
        defaults: new { controller = "CourseEnvironment", action = "Delete" }
    );

    endpoints.MapControllerRoute(
       name: "CourseEnvironment",
       pattern: "CourseEnvironment/EditComment/{courseId}",
       defaults: new { controller = "CourseEnvironment", action = "EditComment" }
    );

    //Course
    endpoints.MapControllerRoute(
        name: "CourseEdit",
        pattern: "CourseEdit/{courseId}",
        defaults: new { controller = "Course", action = "Edit" }
    );

    endpoints.MapControllerRoute(
        name: "CourseDelete",
        pattern: "CourseDelete/{courseId}",
        defaults: new { controller = "Course", action = "Delete" }
    );

    endpoints.MapControllerRoute(
        name: "AddUser",
        pattern: "AddUser/{courseId}",
        defaults: new { controller = "Course", action = "AddUser" }
    );

    endpoints.MapControllerRoute(
        name: "RemoveUser",
        pattern: "RemoveUser/{courseId}",
        defaults: new { controller = "Course", action = "RemoveUser" }
    );
    
    //Default
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

});
app.Run();