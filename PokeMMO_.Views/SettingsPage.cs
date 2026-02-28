using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace PokeMMO_.Views;

public class SettingsPage : UserControl, IComponentConnector
{
	internal TextBlock SupportedResolutionsLabel;

	internal RadioButton chk_fullhd;

	internal RadioButton chk_sd;

	private bool _contentLoaded;

	public SettingsPage()
	{
		InitializeComponent();
	}

	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/PokeMMO+;component/views/settingspage.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		default:
			_contentLoaded = true;
			break;
		case 1:
			SupportedResolutionsLabel = (TextBlock)target;
			break;
		case 2:
			chk_fullhd = (RadioButton)target;
			break;
		case 3:
			chk_sd = (RadioButton)target;
			break;
		}
	}
}
