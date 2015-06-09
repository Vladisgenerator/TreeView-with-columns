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
            UserDevice = new Device("����������");
            UserDevice.Modules.Add(new Module("������ ������ ��� �������"));
            UserDevice.Modules.Add(new Module("������ ������ 100 now", 100, DateTime.Now));
            UserDevice.Modules[0].Modules.Add(new Module("��������� ������"));
            UserDevice.Modules[0].Components.Add(new Component("��������� ��������� 255 now + 1h", 255, DateTime.Now.AddHours(1)));
            UserDevice.Modules.Add(new Module("������ ������ 144 utc", 144, DateTime.UtcNow));
            UserDevice.Components.Add(new Component("������ ��������� 111 now", 111, DateTime.Now));




            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
			Console.WriteLine(PerformanceAnalyzer.GenerateReport("OnPaint"));
		}
	}
}