using System.Windows.Input;

namespace TemoraColetaETT.UI.Views.Components
{
    public partial class NextStepButton : ContentView
    {
        // BindableProperty para o texto do botão (ex: "Próximo", "Finalizar")
        public static readonly BindableProperty ButtonTextProperty =
            BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(NextStepButton), "Próximo");

        // BindableProperty para o comando a ser executado no clique
        public static readonly BindableProperty ButtonCommandProperty =
            BindableProperty.Create(nameof(ButtonCommand), typeof(ICommand), typeof(NextStepButton), null);

        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }

        public NextStepButton()
        {
            InitializeComponent();
            // A linha 'this.BindingContext = this;' foi removida para permitir a herança do BindingContext.
        }
    }
}
