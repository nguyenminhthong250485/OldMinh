using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BET_BET
{
    public class LoginVerifyInfo
    {
        private string host;
        private string ticketID;
        private string custID;
        private string countryName;
        private string lan;
        private string ssl;

        public string Host
        {
            get
            {
                return host;
            }

            set
            {
                host = value;
            }
        }

        public string TicketID
        {
            get
            {
                return ticketID;
            }

            set
            {
                ticketID = value;
            }
        }

        public string CustID
        {
            get
            {
                return custID;
            }

            set
            {
                custID = value;
            }
        }

        public string CountryName
        {
            get
            {
                return countryName;
            }

            set
            {
                countryName = value;
            }
        }

        public string Lan
        {
            get
            {
                return lan;
            }

            set
            {
                lan = value;
            }
        }

        public string Ssl
        {
            get
            {
                return ssl;
            }

            set
            {
                ssl = value;
            }
        }
    }
}
