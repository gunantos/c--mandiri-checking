using System;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.SystemTextJson;
using System.Net.Http.Headers;
using mandiri_checking.Models;
using System.Collections.Generic;

namespace mandiri_checking.Controller
{
    public class Api
    {
        readonly RestClient client;
        
        public Api()
        {
            client = new RestClient(Properties.Resources.BaseURl);
            client.AddDefaultHeader("X-TTPG-KEY", Properties.Resources.ApiKey);
            client.UseSystemTextJson();
        }

        public TicketMdl getTiket(string idTiket)
        {
            var request = new RestRequest($"boarding?ticket_id={idTiket}",Method.GET, DataFormat.Json);
            IRestResponse<TicketMdl> response = client.Execute<TicketMdl>(request);
            return response.Data;
        }

        public TicketMdl_status saveTiket(List<TicketMdl_print> data)
        {
            var request = new RestRequest("boarding-print", Method.POST, DataFormat.Json);
            request.AddJsonBody(data);
            IRestResponse<TicketMdl_status> response = client.Execute<TicketMdl_status>(request);
            return response.Data;
        }
    }
}
