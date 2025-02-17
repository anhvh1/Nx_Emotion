﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Gosol.VHTT.API.Config
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string ClientID { get; set; }
        public double NumberDateExpire { get; set; }
        public string AudienceSecret { get; set; }
        public string ApiUrl { get; set; }
        public string ConfigKey { get; set; }
    }
}
