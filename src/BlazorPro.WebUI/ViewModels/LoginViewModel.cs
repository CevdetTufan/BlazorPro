using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorPro.WebUI.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	private string _username;
	private string _password;
	private string _errorMessage;

	[Required]
	public string Username
	{
		get => _username;
		set
		{
			if (_username == value) return;

			_username = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Username)));
		}
	}

	[Required]
	[DataType(DataType.Password)]
	[MinLength(4)]
	public string Password
	{
		get => _password;
		set
		{
			if (_password == value) return;

			_password = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
		}
	}

	public string ErrorMessage
	{
		get => _errorMessage;
		set
		{
			if (_errorMessage == value) return;

			_errorMessage = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorMessage)));
		}
	}

	public void HandleLogin()
	{
		ErrorMessage = string.Empty;

		bool isValid = Validete(out List<ValidationResult> results);


		if (!isValid)
		{
			// Bir veya daha fazla kural ihlali var.
			// Tüm hata mesajlarını birleştirip gösterebilirsiniz:
			ErrorMessage = string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage));
			return;
		}
	}

	private bool Validete(out List<ValidationResult> results)
	{
		var context = new ValidationContext(this);
		results = [];

		bool isValid = Validator.TryValidateObject(
			this,
			context,
			results,
			validateAllProperties: true
		);

		return isValid;
	}
}
