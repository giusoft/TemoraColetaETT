using TemoraColetaETT.UI.ViewModels;

namespace TemoraColetaETT.UI.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}