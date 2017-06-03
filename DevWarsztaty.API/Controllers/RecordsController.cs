using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevWarsztaty.Messages.Commands;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DevWarsztaty.API.Controllers
{
	[Route("[controller]")]
	public class RecordsController : Controller
	{
		//[HttpPost("records")]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody]CreateRecord command)
		{
			return Accepted();
		}
	}
}
