using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorPro.WebUI.Utils;

public class AuthStateProvider : AuthenticationStateProvider
{
	private readonly ILocalStorageService _localStorage;
	private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());
	private bool _isInitialized = false;

	public AuthStateProvider(ILocalStorageService localStorage)
	{
		_localStorage = localStorage;
	}

	public override Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		return Task.FromResult(new AuthenticationState(_currentUser));
	}

	public async Task InitializeAsync()
	{
		if (_isInitialized) return;

		var token = await _localStorage.GetItemAsStringAsync("token");
		var username = await _localStorage.GetItemAsStringAsync("username");

		if (!string.IsNullOrWhiteSpace(token))
		{
			var identity = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.Name, username)
			}, "jwt");

			_currentUser = new ClaimsPrincipal(identity);
		}

		_isInitialized = true;
		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
	}

	public async Task LoginAsync(string username, string token)
	{
		await _localStorage.SetItemAsync("token", token);
		await _localStorage.SetItemAsync("username", username);

		var identity = new ClaimsIdentity(new[]
		{
			new Claim(ClaimTypes.Name, username)
		}, "jwt");

		_currentUser = new ClaimsPrincipal(identity);
		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
	}

	public async Task LogoutAsync()
	{
		await _localStorage.RemoveItemAsync("token");
		await _localStorage.RemoveItemAsync("username");

		_currentUser = new ClaimsPrincipal(new ClaimsIdentity());
		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
	}
}
