using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using opg_201910_interview.Models;

namespace opg_201910_interview.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment _host;

        public HomeController(IHostingEnvironment hosting)
        {
            _host = hosting;
        }

        public IActionResult Index()
        {
            List<Client> clients = new List<Client>();

            return View(clients);
        }

        [HttpPost]
        //[ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        public IActionResult Index(Client client)
        {
            List<Client> clients = new List<Client>();
            List<FileMode> jsonfiles = new List<FileMode>();
            DirectoryInfo d = new DirectoryInfo(_host.ContentRootPath + @"\UploadFiles\" + client.ClientList);
            int s = 1;

            if (d.Exists)
            {
                FileInfo[] files = d.GetFiles("*.xml");
                var obj = files.ToList();
                foreach (FileInfo file in files)
                {
                    var iFile = new ImportUtility();
                    string fileName = Path.GetFileNameWithoutExtension(file.Name);
                    var x = iFile.ValidateFilenameDate( fileName);
                    if (x.FileDate != null)
                    {
                        var xj = (new { Client = d.Name, Id = x.Id, FileName = fileName, FileDirectoryPath = file.DirectoryName, FileDate = x.FileDate });
                        clients.Add(new Client { Sort = s, Name = d.Name, Id = x.Id, FileName = fileName, FileDirectoryPath = file.DirectoryName, FileDate = x.FileDate, JSONFile = iFile.ConvertToJSON(xj), FileDateFormat = x.FileDateFormat });
                        s++;

                    }
                }
                
            }

            if (client.ClientList.ToString() == "ClientA")
            {
                var outputFiles = clients.FindAll(c => c.FileDateFormat == (from item in clients
                                                                            group item.FileDateFormat by item.FileDateFormat into g
                                                                            orderby g.Count() descending
                                                                            select g.Key).First().ToString()).OrderBy(n => Convert.ToDateTime(n.FileDate).ToString("yyyy")).ThenBy(c => Convert.ToDateTime(c.FileDate).ToString("MM"));

                return View(outputFiles);
            }
            else
            {
                var outputFiles = clients.FindAll(c => c.FileDateFormat == (from item in clients
                                                                            group item.FileDateFormat by item.FileDateFormat into g
                                                                            orderby g.Count() descending
                                                                            select g.Key).First().ToString()).OrderByDescending(c => c.FileName);

                return View(outputFiles);
            }

        }      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
