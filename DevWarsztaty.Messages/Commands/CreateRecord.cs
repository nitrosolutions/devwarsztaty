﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DevWarsztaty.Messages.Commands
{
    public class CreateRecord : ICommand
    {
		public string Key { get; set; }

		public object Value { get; set; }
	}
}
