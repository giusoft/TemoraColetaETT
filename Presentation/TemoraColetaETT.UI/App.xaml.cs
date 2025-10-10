using MauiApp = Microsoft.Maui.Controls.Application;

namespace TemoraColetaETT.UI
{
    public partial class App : MauiApp
    {
        public App(AppShell appShell)
        {
            InitializeComponent();
            MainPage = appShell;
        }
    }
}