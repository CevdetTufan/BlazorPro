using Blazored.LocalStorage;
using BlazorPro.WebUI.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorPro.WebUI.Components.Pages;


public partial class Login
{
	private string Username { get; set; }
	private string Password { get; set; }

	private string ErrorMessage { get; set; }

	[Inject]
	private AuthenticationStateProvider AuthenticationStateProvider { get; set; }



	[Inject]
	private NavigationManager Navigation { get; set; }


	// Butonun çağıracağı metod
	private async Task HandleLoginAsync()
	{
		if (AuthenticationStateProvider is AuthStateProvider auth)
		{
			await auth.LoginAsync(Username, "dummyToken123");
			Navigation.NavigateTo("/");
		}
	}
}
