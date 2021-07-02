using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tx_mvc_forms_deploy.Models;

namespace tx_mvc_forms_deploy.Helpers {
	public static class DeploymentHelpers {

		public static List<Deployment> GetDeployments() {

			List<Deployment> deployments;

			string deploymentFileLocation = "App_Data/deployments.json";

			if (System.IO.File.Exists(deploymentFileLocation) == false) {
				deployments = new List<Deployment>();

				System.IO.File.WriteAllText("App_Data/deployments.json", JsonConvert.SerializeObject(deployments));
			}
			else {
				try {
					deployments = JsonConvert.DeserializeObject<List<Deployment>>(System.IO.File.ReadAllText(deploymentFileLocation));
				}
				catch {
					deployments = null;
				}

				if (deployments == null) {

					System.IO.File.Delete(deploymentFileLocation);
					return GetDeployments();
				}
			}

			return deployments;
		}

		public static bool WriteValue(string username, FormField[] formFields) {

			List<Deployment> deployments = GetDeployments();

			foreach (Deployment deployment in deployments) {
				if (deployment.Username == username) {
					deployment.FormFields = formFields;

					System.IO.File.WriteAllText("App_Data/deployments.json", JsonConvert.SerializeObject(deployments));

					return true;
				}
			}

			return false;
		}

		public static string NewDeployment() {

			string username = Guid.NewGuid().ToString();
			List<Deployment> deployments = GetDeployments();

			deployments.Add(new Deployment() {
				Username = username
			});

			System.IO.File.WriteAllText("App_Data/deployments.json", JsonConvert.SerializeObject(deployments));

			return username;
		}

	}
}
