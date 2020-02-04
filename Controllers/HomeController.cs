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
            var iFile = new ImportUtility();            

            var clientFiles = iFile.ReadXMLFile(_host.ContentRootPath, client.ClientList.ToString());
            List<Client> outputFiles = new List<Client>();

            if (client.ClientList.ToString() == "ClientA")
            {
                var sortFiles = clientFiles.FindAll(c => c.FileDateFormat == (from item in clientFiles
                                                                                group item.FileDateFormat by item.FileDateFormat into g
                                                                            orderby g.Count() descending
                                                                            select g.Key).First().ToString()).OrderBy(n => Convert.ToDateTime(n.FileDate).ToString("yyyy")).ThenBy(c => Convert.ToDateTime(c.FileDate).ToString("MM"));
                outputFiles = sortFiles.ToList();

            }
            else
            {
                var sortFiles = clientFiles.FindAll(c => c.FileDateFormat == (from item in clientFiles
                                                                                group item.FileDateFormat by item.FileDateFormat into g
                                                                            orderby g.Count() descending
                                                                            select g.Key).First().ToString()).OrderByDescending(c => c.FileName);
                outputFiles = sortFiles.ToList();

            }

            return View(outputFiles);

        }      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
