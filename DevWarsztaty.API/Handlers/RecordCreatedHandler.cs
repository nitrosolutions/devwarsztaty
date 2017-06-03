using DevWarsztaty.API.Storage;
using DevWarsztaty.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevWarsztaty.API.Handlers
{
	public class RecordCreatedHandler : IEventHandler<RecordCreated>
	{
		private readonly IStorage _storage;

		public RecordCreatedHandler(IStorage storage)
		{
			_storage = storage;
		}

		public async Task HandleAsync(RecordCreated @event)
		{
			Console.WriteLine($"Record: '{@event.Key}' was created.");
			_storage.Add(@event.Key);
			await Task.CompletedTask;
		}
	}
}
