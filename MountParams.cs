using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASAMount
{
    class MountParams
    {
        public MountParams()
        {
            ra = "";
            dec = "";
            az = "";
            alt = "";
            longitude = "";
            latitude = "";
            connected = false;
            errMsg = "";
            isTracking = false;
            isSlewing = false;
            isHomed = false;
            isParked = false;
            st = "";
            ut = "";
            date = "";
        }
        public string ra;
        public string dec;
        public string az;
        public string alt;
        public string longitude;
        public string latitude;
        public string st;
        public string date;
        public string ut;
        public bool connected;
        public string errMsg;
        public bool isTracking;
        public bool isSlewing;
        public bool isHomed;
        public bool isParked;
    }
}
