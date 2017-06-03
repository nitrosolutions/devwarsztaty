using DevWarsztaty.Messages.Commands;
using DevWarsztaty.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevWarsztaty.API.Handlers
{
	public class CreateRecordFailedHandler : IEventHandler<CreateRecordFailed>
	{
		public async Task HandleAsync(CreateRecordFailed @event)
		{
			Console.WriteLine($"Record: '{@event.Key}' was not created. " + $"Reason: {@event.Reason}");

			await Task.CompletedTask;
		}
	}
}
