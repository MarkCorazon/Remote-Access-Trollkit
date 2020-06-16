﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trollkit_Library.Models;
using Trollkit_Library.Modules;
using Trollkit_Library.ServerModules;

namespace Trollkit_Library.ViewModels.Commands
{
    public class WindowsCommands
    {
		private Server _server;
		private string handler;
		public WindowsCommands(Server server, string handler)
		{
			_server = server;
			this.handler = handler;
		}

		public ICommand MousePosition { get { return new SendServerCommand(SendMousePosition); } }
		public ICommand Go { get { return new SendServerCommand(SendGo); } }
		public ICommand LockWindows { get { return new SendServerCommand(SendLockWindowsk); } }
		public ICommand AltTab { get { return new SendServerCommand(SendAltTab); } }


        private void SendMousePosition()
        {
            //string X = TbXcoordinate.Text;
            //string Y = TbYcoordinate.Text;
            TransferCommandObject returnObject = new TransferCommandObject { Command = "MousePosition", Handler = handler, Value = ""};//$"{X},{Y}" };
            _server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
        }

        private void SendGo()
        {
            TransferCommandObject returnObject = new TransferCommandObject { Command = "Command", Handler = handler, Value = ""}; //$"{TbComammand.Text},hidden" };
             _server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
        }

        private void SendLockWindowsk()
        {
            TransferCommandObject returnObject = new TransferCommandObject { Command = "LockWindows", Handler = handler };
             _server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
        }

        private void SendAltTab()
        {
            TransferCommandObject returnObject = new TransferCommandObject { Command = "AltTab", Handler = handler };
            _server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
    }
    }
}