using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using PokeMMO_.Botting;
using PokeMMO_.Classes;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;

namespace PokeMMO_;

public class DiscordWindow : Window, IComponentConnector
{
	internal DiscordWindow MyDiscordWindow;

	internal Label lbl_x;

	internal CheckBox chk_DiscordDMThief;

	internal CheckBox chk_DiscordDMPayDay;

	internal CheckBox chk_DiscordDMThrowBall;

	internal CheckBox chk_DiscordDMIV31;

	private bool _contentLoaded;

	public DiscordWindow()
	{
		InitializeComponent();
		MyDiscordWindow.Title = RandomTitle.Generate();
		MyDiscordWindow.DataContext = DiscordViewModel.Instance;
		base.Left = SystemParameters.PrimaryScreenWidth - this.method_0();
		base.Top = SystemParameters.PrimaryScreenHeight - this.method_1();
		if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
		{
			base.Top -= 75.0;
			base.Left -= 10.0;
		}
		else if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.SD)
		{
			base.Left -= 75.0;
			base.Top -= 10.0;
		}
	}

	private void lbl_x_Click(object sender, MouseButtonEventArgs e)
	{
		Hide();
	}

	private void Window_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (e.LeftButton == MouseButtonState.Pressed)
		{
			DragMove();
		}
	}

	private void chk_DiscordDM_Changed(object sender, RoutedEventArgs e)
	{
		Configuration.SaveDiscordDMSettings();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/PokeMMO+;component/views/discordwindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerNonUserCode]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		default:
			_contentLoaded = true;
			break;
		case 1:
			MyDiscordWindow = (DiscordWindow)target;
			MyDiscordWindow.MouseDown += Window_MouseDown;
			break;
		case 2:
			lbl_x = (Label)target;
			lbl_x.MouseLeftButtonDown += lbl_x_Click;
			break;
		case 3:
			chk_DiscordDMThief = (CheckBox)target;
			chk_DiscordDMThief.Checked += chk_DiscordDM_Changed;
			chk_DiscordDMThief.Unchecked += chk_DiscordDM_Changed;
			break;
		case 4:
			chk_DiscordDMPayDay = (CheckBox)target;
			chk_DiscordDMPayDay.Checked += chk_DiscordDM_Changed;
			chk_DiscordDMPayDay.Unchecked += chk_DiscordDM_Changed;
			break;
		case 5:
			chk_DiscordDMThrowBall = (CheckBox)target;
			chk_DiscordDMThrowBall.Checked += chk_DiscordDM_Changed;
			chk_DiscordDMThrowBall.Unchecked += chk_DiscordDM_Changed;
			break;
		case 6:
			chk_DiscordDMIV31 = (CheckBox)target;
			chk_DiscordDMIV31.Checked += chk_DiscordDM_Changed;
			chk_DiscordDMIV31.Unchecked += chk_DiscordDM_Changed;
			break;
		}
	}

	double method_0()
	{
		return base.Width;
	}

	double method_1()
	{
		return base.Height;
	}
}
