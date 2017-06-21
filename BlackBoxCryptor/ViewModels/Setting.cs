using System;
using System.Collections.Generic;
using System.Text;

namespace BlackBoxCryptor.ViewModels
{
    public class Setting
    {
        public string key { get; set; }
        public string value { get; set; }
    }
    public class AppSettings
    {
        public Setting[] appSettings { get; set; }
    }
}
