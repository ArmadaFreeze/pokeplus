using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using PokeMMO_.Classes;
using PokeMMO_.ViewModels;

namespace PokeMMO_;

public class SubWindow : Window, IComponentConnector
{
	internal SubWindow MySubWindow;

	internal TextBlock lbl_shinycounter;

	internal TextBlock lbl_timer;

	internal TextBlock lbl_ballsthrowncounter;

	internal TextBlock lbl_encounterscount;

	internal TextBlock lbl_status;

	internal RichTextBox txt_richtextbox;

	internal Button btn_show;

	internal Button btn_hide;

	private bool _contentLoaded;

	public SubWindow()
	{
		InitializeComponent();
		MySubWindow.Title = RandomTitle.Generate();
		MySubWindow.DataContext = SubViewModel.Instance;
		base.Left = SystemParameters.PrimaryScreenWidth - this.method_0();
		base.Top = SystemParameters.PrimaryScreenHeight - this.method_1();
		btn_show.Click += btn_show_Click;
		btn_hide.Click += btn_hide_Click;
	}

	private void btn_show_Click(object sender, RoutedEventArgs e)
	{
		((Window)(object)Application.Current.Windows.OfType<MainWindow>().SingleOrDefault())?.Show();
	}

	private void btn_hide_Click(object sender, RoutedEventArgs e)
	{
		((Window)(object)Application.Current.Windows.OfType<MainWindow>().SingleOrDefault())?.Hide();
		Includes.WindowHelper.BringProcessToFront();
	}

	private void Window_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (e.LeftButton == MouseButtonState.Pressed)
		{
			DragMove();
		}
	}

	private void txt_richtextbox_TextChanged(object sender, TextChangedEventArgs e)
	{
		txt_richtextbox.ScrollToEnd();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/PokeMMO+;component/views/subwindow.xaml", UriKind.Relative);
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
			MySubWindow = (SubWindow)target;
			MySubWindow.MouseDown += Window_MouseDown;
			break;
		case 2:
			lbl_shinycounter = (TextBlock)target;
			break;
		case 3:
			lbl_timer = (TextBlock)target;
			break;
		case 4:
			lbl_ballsthrowncounter = (TextBlock)target;
			break;
		case 5:
			lbl_encounterscount = (TextBlock)target;
			break;
		case 6:
			lbl_status = (TextBlock)target;
			break;
		case 7:
			txt_richtextbox = (RichTextBox)target;
			txt_richtextbox.TextChanged += txt_richtextbox_TextChanged;
			break;
		case 8:
			btn_show = (Button)target;
			break;
		case 9:
			btn_hide = (Button)target;
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
