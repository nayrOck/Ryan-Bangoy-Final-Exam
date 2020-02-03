using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace opg_201910_interview.Models
{
    public class ImportUtility:Client
    {
        public Client ValidateFilenameDate(string fileName )
        {
            Client obj = new Client();
            var fileRegex1 = new Regex(@"((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[13578])|(1[02]))-((0[1-9])|([12][0-9])|(3[01])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-((0[469])|11)-((0[1-9])|([12][0-9])|(30)))|(((000[48])|([0-9]0-9)|([0-9][1-9][02468][048])|([1-9][0-9][02468][048]))-02-((0[1-9])|([12][0-9])))|((([0-9][0-9][0-9][1-9])|([1-9][0-9][0-9][0-9])|([0-9][1-9][0-9][0-9])|([0-9][0-9][1-9][0-9]))-02-((0[1-9])|([1][0-9])|([2][0-8])))");
            var fileRegex2 = new Regex(@"\d{4}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])");

            var match1 = fileRegex1.Match(fileName);
            var match2 = fileRegex2.Match(fileName);
            DateTime fileNameDate;

            if (match1.Success && DateTime.TryParseExact(match1.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fileNameDate))
            {
                obj.FileDateFormat = "yyyy-MM-dd";
                obj.FileDate =  DateTime.ParseExact(match1.Value,"yyyy-MM-dd",CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }
            else if(match2.Success && DateTime.TryParseExact(match2.Value, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fileNameDate))
            {
                obj.FileDateFormat = "yyyyMMdd";
                obj.FileDate = DateTime.ParseExact(match2.Value, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }

            return obj;
        }

        public string ConvertToJSON(object list)
        {
            string sJSONResponse = JsonConvert.SerializeObject(list);

            return sJSONResponse;
        }
    }
}
