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

        // O construtor é injetado pelo MAUI com o ViewModel registrado
        public SignatureView(SignatureViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            // 1. Assinar o evento de Limpeza do ViewModel (Esta lógica permanece)
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(viewModel.NeedsClear) && viewModel.NeedsClear)
                {
                    SignatureArea.Children.Clear();
                    // Reinicia a flag para que a próxima chamada funcione
                    viewModel.NeedsClear = false;
                }
            };
        }

        // Método que lida com o início do desenho (substitui GestureStatus.Started)
        private void OnPointerPressed(object sender, PointerEventArgs e)
        {
            // Obtém a posição absoluta do toque/mouse DENTRO da SignatureArea
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

        // Método que lida com o desenho contínuo (substitui GestureStatus.Running)
        private void OnPointerMoved(object sender, PointerEventArgs e)
        {
            if (currentStroke != null)
            {
                // 1. Obtém a posição absoluta do toque/mouse
                var location = e.GetPosition(SignatureArea).GetValueOrDefault();

                // 2. Adiciona o novo ponto à Polyline
                currentPoints.Add(location);

                currentStroke.IsVisible = false;
                currentStroke.IsVisible = true;
            }
        }

        // Método que lida com o fim do desenho (substitui GestureStatus.Completed)
        private void OnPointerReleased(object sender, PointerEventArgs e)
        {
            currentPoints = null;
            currentStroke = null;
        }
    }
}