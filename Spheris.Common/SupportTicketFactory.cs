using System;

namespace Spheris.Common
{
    public static class SupportTicketFactory
    {
        public static ISupportTicket CreateSupportTicket()
        {
            return(new RemedyTicket());
        }
    }
}
