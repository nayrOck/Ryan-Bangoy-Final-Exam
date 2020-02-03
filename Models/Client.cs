using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace opg_201910_interview.Models
{
    public class Client
    {
        public int Sort { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FileDirectoryPath { get; set; }
        public string FileDate { get; set; }
        public string FileDateFormat { get; set; }
        public string JSONFile { get; set; }
        public ClientList ClientList { get; set; }
        
    }

    public enum ClientList
    {
        ClientA,
        ClientB
    }
}
