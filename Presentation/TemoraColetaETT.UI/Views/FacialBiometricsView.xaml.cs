using TemoraColetaETT.UI.ViewModels;

namespace TemoraColetaETT.UI.Views
{
    public partial class FacialBiometricsView : ContentPage
    {
        public FacialBiometricsView(FacialBiometricsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}