using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tx_mvc_forms_deploy.Models {
	public class Deployment {
		public string Username { get; set; }
		public FormField[] FormFields { get; set; }
	}
}
