﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trollkit_Library.Models;
using Trollkit_Library.Modules;

namespace Trollkit
{
    /// <summary>
    /// Interaction logic for AudioTrolls.xaml
    /// </summary>
    public partial class AudioTrolls : UserControl
    {
		private MainWindow ParentFrame { get { return (MainWindow)Application.Current.MainWindow; } }
		private const string Handler = "Audio";

		public AudioTrolls()
        {
            InitializeComponent();
        }

		private void BtnBeep_Click(object sender, RoutedEventArgs e)
		{
			TransferCommandObject returnObject = new TransferCommandObject { Command = "PlayBeep", Handler = Handler, Value = "800,800" };
			ParentFrame.server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
		}

		private void BtnMyNameIsJeff_Click(object sender, RoutedEventArgs e)
		{
			TransferCommandObject returnObject = new TransferCommandObject { Command = "Jeff", Handler = Handler};
			ParentFrame.server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
		}

		private void BtnVolumeUp_Click(object sender, RoutedEventArgs e)
		{
			TransferCommandObject returnObject = new TransferCommandObject { Command = "VolumeUp", Handler = Handler};
			ParentFrame.server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
		}

		private void BtnVolumeDown_Click_Click(object sender, RoutedEventArgs e)
		{
			TransferCommandObject returnObject = new TransferCommandObject { Command = "VolumeDown", Handler = Handler};
			ParentFrame.server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
		}

		private void BtnWesselMove_Click(object sender, RoutedEventArgs e)
		{
			TransferCommandObject returnObject = new TransferCommandObject { Command = "WesselMove", Handler = Handler};
			ParentFrame.server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
		}

		private void BtnPlayPauze_Click(object sender, RoutedEventArgs e)
		{
			TransferCommandObject returnObject = new TransferCommandObject { Command = "PlayPause", Handler = Handler };
			ParentFrame.server.SendDataObjectToAll(ClientServerPipeline.BufferSerialize(returnObject));
		}

		//TODO: Add following actions:
		//Mute, NextTrack, PrevTrack
	}
}
