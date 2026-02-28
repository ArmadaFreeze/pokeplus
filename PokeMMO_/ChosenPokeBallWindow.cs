using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using PokeMMO_.Botting;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;

namespace PokeMMO_;

public class ChosenPokeBallWindow : Window, IComponentConnector
{
	internal ChosenPokeBallWindow MyChosenPokeBallWindow;

	internal Label lbl_x;

	private bool _contentLoaded;

	public ChosenPokeBallWindow()
	{
		InitializeComponent();
		MyChosenPokeBallWindow.Title = RandomTitle.Generate();
		MyChosenPokeBallWindow.DataContext = ChosenPokeBallViewModel.Instance;
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

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/PokeMMO+;component/views/chosenpokeballwindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			MyChosenPokeBallWindow = (ChosenPokeBallWindow)target;
			MyChosenPokeBallWindow.MouseDown += Window_MouseDown;
			break;
		default:
			_contentLoaded = true;
			break;
		case 2:
			lbl_x = (Label)target;
			lbl_x.MouseLeftButtonDown += lbl_x_Click;
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
