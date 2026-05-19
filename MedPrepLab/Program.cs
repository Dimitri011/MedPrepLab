using MedPrepLab.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSession();

builder.Services.AddSingleton<JsonDataService>();
builder.Services.AddSingleton<LessonService>();
builder.Services.AddSingleton<QuestionService>();
builder.Services.AddSingleton<QuizService>();
builder.Services.AddSingleton<ProgressService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();