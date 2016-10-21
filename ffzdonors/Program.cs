using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ffzdonors
{
    class Program
    {
        static void Main(string[] args)
        {
            string chattyFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.chatty\\addressbookImport.txt";
            string chattyFileContents = "";
            string json = GetJsonFromUrl("http://api.frankerfacez.com/v1/badge/supporter");

            JObject o = JObject.Parse(json);
            JArray a = (JArray)o["users"]["3"];

            List<string> users = a.Select(c => (string)c).ToList();

            foreach (string user in users)
            {
                chattyFileContents += $"add {user} ffzdonor" + Environment.NewLine;
            }

            File.WriteAllText(chattyFile, chattyFileContents);

            //Console.ReadKey();
        }

        private static string GetJsonFromUrl(string url)
        {
            string json = "No data found.";

            using (WebClient client = new WebClient())
            {
                json = client.DownloadString(url);
            }

            return json;
        }
    }
}
