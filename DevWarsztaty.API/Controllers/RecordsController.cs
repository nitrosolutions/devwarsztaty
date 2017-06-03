using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevWarsztaty.Messages.Commands;
using RawRabbit;
using DevWarsztaty.API.Storage;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DevWarsztaty.API.Controllers
{
	[Route("[controller]")]
	public class RecordsController : Controller
	{
		readonly IBusClient _busClient;
		readonly IStorage _storage;

		public RecordsController(IBusClient busClient, IStorage storage)
		{
			_busClient = busClient;
			_storage = storage;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Json(_storage.GetAll());
		}

		//[HttpPost("records")]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody]CreateRecord command)
		{
			await _busClient.PublishAsync(command);

			return Accepted();
		}
	}
}
