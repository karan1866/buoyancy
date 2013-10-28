using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Reflection;
using System.IO;
//using System.Windows.Forms;

namespace BatteryAgent
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        wpfMain forms = new wpfMain();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Enabled =true , Interval = 1};

        public void writefile( System.IO.Stream src, System.IO.Stream dest)
        {
            int readCount;
            var buffer = new byte[30000];
            while ((readCount = src.Read(buffer, 0, buffer.Length)) != 0)
                dest.Write(buffer, 0, readCount);
        }


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                //if (!System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\battery.mp3"))
                //{
                    Assembly assembly = Assembly.GetExecutingAssembly();

                    Stream reader = assembly.GetManifestResourceStream("BatteryAgent.battery.mp3");
                    
                    string saveTo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\battery.mp3";
                    FileStream writeStream = new FileStream(saveTo, FileMode.Create, FileAccess.Write);
                    writefile(reader, writeStream);
                //}
            }
            catch (Exception ex) { }

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\battery.mp3";

            //MessageBox.Show((new System.Uri(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\battery.mp3", true)).ToString());
            //MessageBox.Show(new System.Uri("battery.mp3", UriKind.Relative).ToString());
            forms.media.Source  = new System.Uri(path,UriKind.Absolute);

            timer.Tick += new EventHandler(timer_tick);

            forms.Show();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            System.Windows.Forms.PowerStatus power = System.Windows.Forms.SystemInformation.PowerStatus;

            

            int powerPercent = (int)(power.BatteryLifePercent * 100);

                switch (power.PowerLineStatus)
                {
                    case System.Windows.Forms.PowerLineStatus.Online:
                        if (forms.critical == true)
                            forms.HideSign();
                        break;

                    case System.Windows.Forms.PowerLineStatus.Offline:
                        if (powerPercent <= 10 && forms.critical==false)
                            forms.ShowSign();
                        break;
                }

        }
    }
}
