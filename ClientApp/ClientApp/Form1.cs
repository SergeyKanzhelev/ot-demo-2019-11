using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        private HttpClient _httpClient;

        public Form1()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async Task<string> GetWeatherForecast()
        {
            return await _httpClient.GetStringAsync(
                                         "http://localhost:5000/weatherforecastproxy");
        }


        private void RedButton_Click(object sender, EventArgs e)
        {
            var activity = new Activity("CallToBackend")
                .AddBaggage("FlightID", "red");
            
            activity.Start();

            try
            {
                _ = GetWeatherForecast();
            }
            finally
            {
                activity.Stop();
            }
        }

        private void GreenButton_Click(object sender, EventArgs e)
        {
            var activity = new Activity("CallToBackend")
                .AddBaggage("FlightID", "green");

            activity.Start();

            try
            {
                _ = GetWeatherForecast();
            }
            finally
            {
                activity.Stop();
            }
        }
    }
}
