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

        // O construtor � injetado pelo MAUI com o ViewModel registrado
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
                    // Reinicia a flag para que a pr�xima chamada funcione
                    viewModel.NeedsClear = false;
                }
            };
        }

        // M�todo que lida com o in�cio do desenho (substitui GestureStatus.Started)
        private void OnPointerPressed(object sender, PointerEventArgs e)
        {
            // Obt�m a posi��o absoluta do toque/mouse DENTRO da SignatureArea
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

        // M�todo que lida com o desenho cont�nuo (substitui GestureStatus.Running)
        private void OnPointerMoved(object sender, PointerEventArgs e)
        {
            if (currentStroke != null)
            {
                // 1. Obt�m a posi��o absoluta do toque/mouse
                var location = e.GetPosition(SignatureArea).GetValueOrDefault();

                // 2. Adiciona o novo ponto � Polyline
                currentPoints.Add(location);

                currentStroke.IsVisible = false;
                currentStroke.IsVisible = true;
            }
        }

        // M�todo que lida com o fim do desenho (substitui GestureStatus.Completed)
        private void OnPointerReleased(object sender, PointerEventArgs e)
        {
            currentPoints = null;
            currentStroke = null;
        }
    }
}