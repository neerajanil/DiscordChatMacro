using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordWebHook
{
    class Program
    {
        static int Main(string[] args)
        {
            //for debugging
            //args = new string[] {
            //    "<<put your webhook url here>>",
            //    "?chatwheel crybaby"
            //};

            if (args.Length < 2)
                return 0;

            try
            {
                string hookUrl = args[0];
                string messageContent = args[1];

                try
                {
                    Uri hookUri = new Uri(hookUrl);
                }
                catch(System.UriFormatException)
                {
                    MessageBox.Show("Error: Discord webhook url not in correct format.");
                    return 0;
                }


                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 1, 0, 0);


                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var body = new StringContent(String.Format("{{ \"content\":\"{0}\" }}", messageContent));
                    body.Headers.ContentType.MediaType = "application/json";

                    var response = client.PostAsync(hookUrl, body).Result;
                    return response.IsSuccessStatusCode ? 1 : 0;
                }

            }
            catch
            {
                return 0;
            }
        }
    }
}