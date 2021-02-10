using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REST_WetterClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var url = $"https://www.metaweather.com/api/location/{textBox1.Text}";

            var http = new HttpClient();
            var json = await http.GetStringAsync(url);

            textBox2.Text = json;
            WetterDaten wd = JsonConvert.DeserializeObject<WetterDaten>(json);

            label2.Text = wd.title;
            dataGridView1.DataSource = wd.consolidated_weather;
        }
    }
}
