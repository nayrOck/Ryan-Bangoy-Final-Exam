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
        public string JSONFile { get; set; }

        public Client ValidateFormat(string clientName, string fileName)
        {

            Client obj = new Client();
            string _date;
            DateTime dt;
            string format = null;
            if (clientName == "ClientA")
            {
                _date = fileName.Substring(fileName.Length - 10, 10);
                format = "yyyy-MM-dd";

            }
            else
            {
                _date = fileName.Substring(fileName.Length - 8, 8);
                format = "yyyyMMdd";

            }

            if (DateTime.TryParseExact(_date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                if (clientName == "ClientA")
                {
                    _date = fileName.Substring(fileName.Length - 10, 10);
                    format = "yyyy-MM-dd";
                    obj.Id = "1001";
                    obj.FileDate = _date;
                    obj.Name = clientName;
                    obj.FileName = fileName;
                    return obj;
                }
                else
                {
                    _date = fileName.Substring(fileName.Length - 8, 8);
                    format = "yyyyMMdd";
                    obj.Id = "2001";
                    obj.FileDate = DateTime.ParseExact(_date, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    obj.Name = clientName;
                    obj.FileName = fileName;
                    return obj;
                }
            }
            else
                return obj;
        }

        public string ConvertToJSON(object list)
        {
            string sJSONResponse = JsonConvert.SerializeObject(list);

            return sJSONResponse;
        }
    }
}
