using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppDesserializar.Frame.Domain;

namespace AppDesserializar.Frame
{
    public partial class Weather : Form
    {
        public Weather()
        {
            InitializeComponent();
        }

        #region Events
        private void Weather_Load(object sender, EventArgs e)
        {
            label1.Text = $"\u00a9 Copyleft - { DateTime.Now.Year }";
        }

        private async void btnDeserialize_Click(object sender, EventArgs e)
        {
            txbDesserialization.Text = await GetWoeid(txbWoeid.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txbDesserialization.Clear();
        }

        #endregion

        #region Deserialize

        public async Task<string> GetWoeid(string woeid)
        {
            string url = $"https://api.hgbrasil.com/weather?woeid={woeid}";

            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(url);

                var content = await result.Content.ReadAsStringAsync();

                string hgresult = Convert.ToString(JsonConvert.DeserializeObject<dynamic>(content));

                return txbDesserialization.Text = hgresult.ToString();
            }
        }

        #endregion

        #region Output

        private void Output(string woeid)
        {
            try
            {
                Debug.Write(woeid + Environment.NewLine);
                txbDesserialization.Text = txbDesserialization.Text + woeid + Environment.NewLine;
                txbDesserialization.SelectionStart = txbDesserialization.TextLength;
                txbDesserialization.ScrollToCaret();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message.ToString() + Environment.NewLine);
            }
        }

        #endregion
    }
}
