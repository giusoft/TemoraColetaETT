namespace TemoraColetaETT.UI.Views.Components
{
    public partial class ComponentEntryInput : ContentView
    {
        // Propriedade para o texto do Label
        public static readonly BindableProperty LabelProperty =
            BindableProperty.Create(nameof(Label), typeof(string), typeof(ComponentEntryInput), string.Empty);

        // Propriedade para o valor do Entry (com TwoWay binding)
        public static readonly BindableProperty ValorProperty =
            BindableProperty.Create(nameof(Valor), typeof(string), typeof(ComponentEntryInput), string.Empty, BindingMode.TwoWay);

        // Propriedade para o placeholder do Entry
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ComponentEntryInput), string.Empty);

        // Propriedade para o tipo de teclado do Entry
        public static readonly BindableProperty TipoTecladoProperty =
            BindableProperty.Create(nameof(TipoTeclado), typeof(Keyboard), typeof(ComponentEntryInput), Keyboard.Default);

        // Propriedade para o MaxLength do Entry
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(ComponentEntryInput), int.MaxValue);

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public string Valor
        {
            get => (string)GetValue(ValorProperty);
            set => SetValue(ValorProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public Keyboard TipoTeclado
        {
            get => (Keyboard)GetValue(TipoTecladoProperty);
            set => SetValue(TipoTecladoProperty, value);
        }

        public ComponentEntryInput()
        {
            InitializeComponent();
        }
    }
}