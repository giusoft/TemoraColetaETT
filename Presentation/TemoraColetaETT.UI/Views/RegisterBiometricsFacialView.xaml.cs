using TemoraColetaETT.UI.ViewModels;

namespace TemoraColetaETT.UI.Views;

public partial class RegisterBiometricsFacialView : ContentPage
{
	public RegisterBiometricsFacialView(RegisterBiometricsFacialViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is RegisterBiometricsFacialViewModel vm)
        {
            await vm.OnAppearingAsync();
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is RegisterBiometricsFacialViewModel vm)
        {
            vm.Cleanup();
        }
    }
}