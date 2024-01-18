using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.Globalization;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            lblTime.Text = DateTime.Now.ToLongTimeString();
            lblDate.Text = DateTime.Now.ToLongDateString();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void panel1_MouseUp(object sender, MouseEventArgs e)
                {
                    Move = 0;         
                }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        { 
            if (Move == 1)
                {
                    this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
                }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text=DateTime.Now.ToLongTimeString();
            lblDate.Text = DateTime.Now.ToLongDateString();
        }

        string APIKey = "98715726e3b1e853c750c34298fa8bad";
        private void btnSearch_Click(object sender, EventArgs e)
        {
            getWeather();
        }

        private void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", textboxCity.text, APIKey);
                var json = web.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                pictureBox1.ImageLocation = "https://api.openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                lblCondition.Text = Info.weather[0].main;
                lblHumidity.Text = Info.main.humidity.ToString();
                lblPres.Text =Info.main.pressure.ToString();
                lblSpeed.Text =Info.wind.speed.ToString();
                lblTemp.Text =Info.main.temp.ToString();
                lblCity.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textboxCity.text);
                lblDetail.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Info.weather[0].description);       
            }
        }
    }
}
