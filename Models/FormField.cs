using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tx_mvc_forms_deploy.Models {
	public class FormField {
		public string name { get; set; }
		public string type { get; set; }
		public dynamic value { get; set; }
	}
}
