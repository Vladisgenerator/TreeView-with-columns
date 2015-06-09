using System;
using System.Windows.Forms;
using Aga.Controls;
using System.Collections.Generic;
using BusinessEntities;

namespace SampleApp
{
	static class Program
	{
        public static Device UserDevice { get; set; }




		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
            UserDevice = new Device("1 Устройство");
            UserDevice.Modules.Add(new Module("2-0-1 Первый модуль без свойств"));
            UserDevice.Modules.Add(new Module("2-0-2 Второй модуль 100 now", 100, DateTime.Now));
            UserDevice.Modules[0].Modules.Add(new Module("3-0-1 Вложенный модуль"));
            UserDevice.Modules[0].Components.Add(new Component("3-1-1 Вложенный компонент 255 now + 1h", 255, DateTime.Now.AddHours(1)));
            UserDevice.Modules.Add(new Module("2-0-3 Третий модуль 144 utc", 144, DateTime.UtcNow));
            UserDevice.Components.Add(new Component("2-1-1 Первый компонент 111 now", 111, DateTime.Now));




            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
			Console.WriteLine(PerformanceAnalyzer.GenerateReport("OnPaint"));
		}
	}
}