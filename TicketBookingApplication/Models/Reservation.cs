using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingApplication.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int PlayId { get; set; }

        public string ReservationType { get; set; }
        public string ReservationDate { get; set; }
    }
}
