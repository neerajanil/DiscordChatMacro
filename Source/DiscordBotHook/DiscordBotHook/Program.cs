using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordBotHook
{
    class Program
    {
        static int Main(string[] args)
        {
            //for debugging
            //args = new string[] {
            //    "http://536f5bf9-e225-42cf-9186-0027b6d8cac6.pub.cloud.scaleway.com/api/macro",
            //    "<<token guid>>",
            //    "?chatwheel crybaby"
            //};

            if (args.Length < 2)
                return 0;

            try
            {
                string hookUrl = args[0];
                string token = args[1];
                string messageContent = args[2];
                

                try
                {
                    Uri hookUri = new Uri(hookUrl);
                }
                catch (System.UriFormatException)
                {
                    MessageBox.Show("Error: Discord webhook url not in correct format.");
                    return 0;
                }


                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 1, 0, 0);


                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var body = new StringContent(String.Format("{{ \"message\":\"{0}\",\"tokenid\":\"{1}\" }}", messageContent, token));
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