using System;
using System.Collections.Generic;

namespace mandiri_checking.Models
{
   public class TicketMdl
   {
        public bool status { get; set; }
        public List<TicketMdl_data> data { get; set; }
        public List<TicketMdl_detail> data_detail { get; set; }
        public List<TicketMdl_print> data_print { get; set; }

   }
    
    public class TicketMdl_status
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class TicketMdl_print
    {
        public string boarding_id { get; set; }
        public string ticket_id { get; set; }
        public string date_of_departure { get; set; }
        public string time_of_departure { get; set; }
        public string po_id { get; set; }
        public string destination_id { get; set; }
        public string destination_name { get; set; }
        public string po_name { get; set; }
        public string owner_id { get; set; }
        public string nik { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string sex { get; set; }
        public string telp { get; set; }
        public string email { get; set; }
        public string status_pnp { get; set; }
    }

    public class TicketMdl_data
    {
        public string boarding_id { get; set; }
        public string ticket_id { get; set; }
        public string date_of_departure { get; set; }
        public string time_of_departure { get; set; }
        public string po_id { get; set; }
        public string destination_id { get; set; }
        public string destination_name { get; set; }
        public string po_name { get; set; }
        public string status { get; set; }
        public string owner_id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }

    }

    public class TicketMdl_detail
    {
        public string passanger_id { get; set; }
        public string nik { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string sex { get; set; }
        public string telp { get; set; }
        public string email { get; set; }
        public string status_pnp { get; set; }
        public bool print { get; set; }

    }
}
