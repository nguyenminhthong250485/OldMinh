using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BET_BET
{
    class TelegramHelper
    {
        RestClient client;
        public TelegramHelper(string token)
        {
            client = new RestClient("https://api.telegram.org/bot" + token);
        }
        internal class Item
        {
            public int chat_id { get; set; }
            public string text { get; set; }
        }
        public getUpdates getMessagesWithOffset(int offsetValue)
        {
            var request = new RestRequest("getUpdates?offset=" + offsetValue, Method.GET);
            IRestResponse response = client.Execute(request);
            getUpdates o = JsonConvert.DeserializeObject<getUpdates>(response.Content);
            return o;
        }

        public getUpdates getMessages()
        {
            var request = new RestRequest("getUpdates", Method.GET);
            IRestResponse response = client.Execute(request);
            getUpdates o = JsonConvert.DeserializeObject<getUpdates>(response.Content);
            return o;
        }

        public void sendMessage(string receive, string message)
        {
            var request = new RestRequest("sendMessage  ", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new Item
            {
                chat_id = Int32.Parse(receive),
                text = message
            });

            client.Execute(request);
        }
    }
}
