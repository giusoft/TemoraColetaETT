using TemoraColetaETT.UI.ViewModels;

namespace TemoraColetaETT.UI.Views;

public partial class RegisterPersonView : ContentPage
{
	public RegisterPersonView(RegisterPersonViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}