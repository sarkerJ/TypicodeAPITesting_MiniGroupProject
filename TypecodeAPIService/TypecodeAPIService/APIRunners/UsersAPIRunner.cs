﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypecodeAPIService.Interfaces;

namespace TypecodeAPIService.APIRunners
{
    public class UsersAPIRunner<T> : IAPIRunner<T>
    {
        public RestClient Client { get; }

        public T ResponseDTO { get; set; }

        public List<KeyValuePair<string, object>> Args { get; }

        public UsersAPIRunner(RestClient client, string resource)
        {
            Client = client;
            Args = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("resource", resource) };
        }

        public void Execute()
        {
            var request = new RestRequest(Args.Where(x => x.Key == "resource").First().Value.ToString());
            var res = Client.Execute(request).Content;
            if (res != "{}")
            {
                ResponseDTO = JsonConvert.DeserializeObject<T>(res);
            }
        }
    }
}
