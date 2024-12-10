namespace CCVProyecto2v2.ViewLogin;

using CCVProyecto2v2.ViewsGeneral;
using CCVProyecto2v2.ViewsModels;

public partial class LoginViewIdentity : ContentPage
{
    private readonly LoginViewModel _viewModel;
    public LoginViewIdentity(LoginViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    private async void Ingresar_Clicked(object sender, EventArgs e)
    {
        var username = UsuarioEntry.Text;
        var password = ContraseniaEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Por favor completa todos los campos", "Aceptar");
            return;
        }

        var result = await _viewModel.LoginAsync(username, password);

        if (result)
        {
            await DisplayAlert("Éxito", "Inicio de sesión exitoso", "Aceptar");
            await Navigation.PushAsync(new AdministradorView());
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "Aceptar");
        }
    }
}