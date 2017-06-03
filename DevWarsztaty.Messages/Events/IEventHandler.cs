using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevWarsztaty.Messages.Events
{
	public interface IEventHandler<in T> where T : IEvent
	{
		Task HandlerAsync(T @event);
	}
}
