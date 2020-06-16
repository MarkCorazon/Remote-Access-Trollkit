﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trollkit_Library.Models;
using Trollkit_Library.Modules;
using Trollkit_Library.ServerModules;

namespace Trollkit_Library.ViewModels.Commands
{
    public class VisualCommands
    {
		private Server _server;
		private string handler;
		public VisualCommands(Server server, string handler)
		{
			_server = server;
			this.handler = handler;
		}

		public ICommand DisplayImage { get { return new SendServerCommand(SendDisplayImage); } }
		public ICommand DisplayText { get { return new SendServerCommand(SendDisplayText); } }
		public ICommand TurnOffScreen { get { return new SendServerCommand(SendTurnOffScreen); } }
		public ICommand	OpenSite { get { return new SendServerCommand(SendOpenSite); } }
		public ICommand PickBackgroundImage { get { return new SendServerCommand(SendPickBackgroundImage); } }

		private void SendDisplayImage()
		{
			OpenFileDialog open = new OpenFileDialog();
			open.Filter = "Image Files(*.png; *.jpg; *.jpeg; *.gif;)|*.png; *.jpg; *.jpeg; *.gif;";
			open.Multiselect = false;
			open.Title = "Pick an image to send to the client";
			if (open.ShowDialog() == true)
			{
				byte[] bytes = File.ReadAllBytes(open.FileName);

				string base64 = Convert.ToBase64String(bytes);
				TransferCommandObject returnObject = new TransferCommandObject { Command = "ShowImage", Handler = handler, Value = base64 };

				//App.Server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
				_server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
			}
		}

		private void SendDisplayText()
		{
			TransferCommandObject returnObject = new TransferCommandObject { Command = "TextBox", Handler = handler, Value = "" }; //tbDisplayText.Text };
			//App.Server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
			_server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
		}

		private void SendTurnOffScreen()
		{
			TransferCommandObject returnObject = new TransferCommandObject { Command = "BlackScreen", Handler = handler, Value = "" };
			//App.Server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
			_server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
		}

		private void SendOpenSite()
		{
			TransferCommandObject returnObject = new TransferCommandObject { Command = "OpenSite", Handler = handler, Value = "https://google.com" }; //tbOpenSite.Text };
			//App.Server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
			_server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
		}

		private void SendPickBackgroundImage()
		{
			OpenFileDialog open = new OpenFileDialog();
			open.Filter = "Image Files(*.png; *.jpg; *.jpeg;)|*.png; *.jpg; *.jpeg;";
			open.Multiselect = false;
			open.Title = "Pick an image to set as background in the client";
			if (open.ShowDialog() == true)
			{
				byte[] bytes = File.ReadAllBytes(open.FileName);

				string base64 = Convert.ToBase64String(bytes);

				TransferCommandObject returnObject = new TransferCommandObject { Command = "SetBackground", Handler = handler, Value = base64 };
				//App.Server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
				_server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
			}
		}
	}
}
