using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReservationBrute.Models;
using System.Net.Mail;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using System.IO;

namespace ReservationBrute.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult About()
        {
            // step 1: grab the storage account and container information first
            CloudBlobContainer container = getBlobStorageInformation();

            //Step 2: create the empty list to store for the blobs
            List<string> blobs = new List<string>();

            //step 3: get the listing record from the blob storage
            BlobResultSegment result = container.ListBlobsSegmentedAsync(null).Result;

            //step 4: to read blob listing from the storage
            foreach (IListBlobItem item in result.Results)
            {
                //step 4.1: check the type of the blob: block blob or directory or page blob
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobs.Add(blob.Name + "#" + blob.Uri.ToString());
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory blob = (CloudBlobDirectory)item;
                    blobs.Add(blob.Uri.ToString());
                }
            }

            return View(blobs);
        }

        private CloudBlobContainer getBlobStorageInformation()
        {
            //read json
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");
            IConfigurationRoot configure = builder.Build();

            // to get the access key
            // once link, time to read the content to get the connectionstring 
            CloudStorageAccount objectaccount =
               CloudStorageAccount.Parse(configure["ConnectionStrings:blobStorageConnection"]);

            CloudBlobClient blobclientagent = objectaccount.CreateCloudBlobClient();

            //step 2: how to create a new container in the blob storage account.
            CloudBlobContainer container = blobclientagent.GetContainerReference("home");

            return container;
        }

        public IActionResult Booking()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(vm.Email);
                    msz.To.Add("Chiranjeevnaidu.cn@gmail.com");
                    msz.Subject = vm.Subject;
                    msz.Body = vm.Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("ibbmsapu@gmail.com", "ibbmsapu2020!");

                    smtp.EnableSsl = true;

                    smtp.Send(msz);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting us ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                }
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
