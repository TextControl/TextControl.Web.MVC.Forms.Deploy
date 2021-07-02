using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tx_mvc_forms_deploy.Helpers;
using tx_mvc_forms_deploy.Models;

namespace tx_mvc_forms_deploy.Controllers {
	public class HomeController : Controller {
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger) {
			_logger = logger;
		}

		public IActionResult Complete(string Username) {

			CompleteModel model = new CompleteModel() {
				Username = Username
			};

			return View(model);
		}

		public IActionResult Index() {

			return View(DeploymentHelpers.GetDeployments());

		}

		[HttpPost]
		public IActionResult FormValues(string Username) {

			// read stream to retrieve form field values
			Stream inputStream = Request.Body;
			FormField[] fields;

			StreamReader str = new StreamReader(inputStream);

			string sBuf = str.ReadToEndAsync().Result;
			fields = (FormField[])JsonConvert.DeserializeObject(sBuf, typeof(FormField[]));

			// write the values
			DeploymentHelpers.WriteValue(Username, fields);

			return Ok();
		}

		[HttpGet]
		public IActionResult NewDeployment() {

			DeploymentHelpers.NewDeployment();
			return Ok();

		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
