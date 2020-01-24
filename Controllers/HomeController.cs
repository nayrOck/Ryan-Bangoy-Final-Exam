using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using opg_201910_interview.Models;

namespace opg_201910_interview.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var c = new Client();
            List<Client> clients = new List<Client>();
            List<FileMode> jsonfiles = new List<FileMode>();
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\rb\Desktop\FlexisourceIT_DotnetCoreExam\opg-201910Base-master\opg-201910Base-master\UploadFiles\ClientA");
            int s = 1;
            
            if (d.Exists)
            {

                FileInfo[] files = d.GetFiles("*.xml");
                var obj = files.ToList();
                foreach (FileInfo file in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file.Name);
                    var x = c.ValidateFormat(d.Name, fileName);
                    if (x.FileDate != null)
                    {
                        var xj = (new {Client = d.Name, Id = x.Id, FileName = fileName, FileDirectoryPath = file.DirectoryName, FileDate = x.FileDate });
                        clients.Add(new Client { Sort = s, Name = d.Name, Id = x.Id, FileName = fileName, FileDirectoryPath = file.DirectoryName, FileDate = x.FileDate, JSONFile = c.ConvertToJSON(xj) });
                        s++;

                    }
                }               
                
            }

            return View(clients.OrderBy(c => c.FileDate));

        }


        public IActionResult Privacy()
        {
            var c = new Client();
            List<Client> clients = new List<Client>();
            List<FileMode> jsonfiles = new List<FileMode>();
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\rb\Desktop\FlexisourceIT_DotnetCoreExam\opg-201910Base-master\opg-201910Base-master\UploadFiles\ClientB");
            int s = 1;
            if (d.Exists)
            {

                FileInfo[] files = d.GetFiles("*.xml");
                var obj = files.ToList();
                foreach (FileInfo file in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file.Name);

                    var x = c.ValidateFormat(d.Name, fileName);
                    if (x.FileDate != null)
                    {
                        var xj = (new  { Sort = s, Client = d.Name, Id = x.Id, FileName = fileName, FileDirectoryPath = file.DirectoryName, FileDate = x.FileDate });
                        clients.Add(new Client { Sort = s, Name = d.Name, Id = x.Id, FileName = fileName, FileDirectoryPath = file.DirectoryName, FileDate = x.FileDate,JSONFile = c.ConvertToJSON(xj) });
                        s++;

                    }
                    
                }

            }

            return View(clients.OrderBy(c => c.FileName));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
