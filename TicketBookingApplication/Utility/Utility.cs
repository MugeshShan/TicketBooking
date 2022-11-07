using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingApplication.Models;

namespace TicketBookingApplication.Utility
{
    public static class Utility
    {
        public static List<Location> Locations = new List<Location>();
        public static Customer Customer = new Customer();
        public static Crew Crew = new Crew();
        public static Play Play = new Play();
        public static string ClickedButton = "";
        public static string Section = "";
        public static int Amount = 0;
        public static int No_of_Seats = 0;
    }
}
