using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using TemoraColetaETT.UI.ViewModels;

namespace TemoraColetaETT.UI.Views
{
    public partial class SignatureView : ContentPage
    {
        private PointCollection currentPoints;
        private Polyline currentStroke;

        public SignatureView(SignatureViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            // 1. Assinar o evento de Limpeza do ViewModel (Esta l�gica permanece)
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(viewModel.NeedsClear) && viewModel.NeedsClear)
                {
                    SignatureArea.Children.Clear();
                    viewModel.NeedsClear = false;
                }
            };
        }

        // Iniciar a assinatura
        private void OnPointerPressed(object sender, PointerEventArgs e)
        {
            var location = e.GetPosition(SignatureArea).GetValueOrDefault();

            currentPoints = new PointCollection { location };

            currentStroke = new Polyline
            {
                Stroke = Colors.Black,
                StrokeThickness = 3,
                StrokeLineJoin = (PenLineJoin)LineJoin.Round,
                StrokeLineCap = PenLineCap.Round,
                Points = currentPoints
            };

            SignatureArea.Children.Add(currentStroke);
        }

        // Lida com o desenho continuo da assinatura
        private void OnPointerMoved(object sender, PointerEventArgs e)
        {
            if (currentStroke != null)
            {
                var location = e.GetPosition(SignatureArea).GetValueOrDefault();
                currentPoints.Add(location);
                currentStroke.IsVisible = false;
                currentStroke.IsVisible = true;
            }
        }

        // Fim da assinatura
        private void OnPointerReleased(object sender, PointerEventArgs e)
        {
            currentPoints = null;
            currentStroke = null;
        }
    }
}