using System;
using Microsoft.Maui;

namespace TemoraColetaETT.UI
{
	class Program : MauiApplication
	{
		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
		static void Main(string[] args)
		{
			var app = new Program();
			app.Run(args);
		}
	}
}