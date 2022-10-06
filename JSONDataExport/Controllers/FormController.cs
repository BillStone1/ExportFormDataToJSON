using JSONDataExport.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JSONDataExport.Controllers
{
    public class FormController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FormController(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult ExportToJSON()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExportToJSON(FormDataModel formData)
        {
            JObject obj = (JObject)JToken.FromObject(formData);
            CreateFileJSON(obj);
            return View();
        }

        private void CreateFileJSON(JObject obj)
        {
            string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath,"JSON"); //wwwroot/JSON/
            var fileName = "Exported_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".json"; //Guid.NewGuid().ToString() + ".json";
            string filePath = Path.Combine(uploadDir,fileName);
            System.IO.File.WriteAllText(filePath, obj.ToString());
            ViewBag.SuccessMessage = fileName + " created successfully.";
            ModelState.Clear(); //Clears the form.
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
