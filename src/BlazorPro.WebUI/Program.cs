using BlazorPro.WebUI.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddAuthentication("Cookies")               // �emaya bir isim veriyoruz
	.AddCookie("Cookies", options =>
	{
		// �ste�e g�re login/logout path tan�mlayabilirsiniz
		//options.LoginPath = "/login";
		//options.LogoutPath = "/logout";
	});

// 2) Authorization servislerini ekleyin
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
