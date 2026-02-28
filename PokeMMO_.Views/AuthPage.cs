using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace PokeMMO_.Views;

public class AuthPage : UserControl, IComponentConnector
{
	internal TextBlock lbl_auth_status;

	internal TextBlock lbl_auth_username;

	internal TextBlock lbl_auth_hwid;

	internal TextBlock lbl_auth_ip;

	internal TextBlock lbl_auth_expiry;

	internal TextBlock lbl_auth_last_login;

	internal TextBlock lbl_auth_register_date;

	private bool _contentLoaded;

	public AuthPage()
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
			Uri resourceLocator = new Uri("/PokeMMO+;component/views/authpage.xaml", UriKind.Relative);
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
			lbl_auth_status = (TextBlock)target;
			break;
		case 2:
			lbl_auth_username = (TextBlock)target;
			break;
		case 3:
			lbl_auth_hwid = (TextBlock)target;
			break;
		case 4:
			lbl_auth_ip = (TextBlock)target;
			break;
		case 5:
			lbl_auth_expiry = (TextBlock)target;
			break;
		case 6:
			lbl_auth_last_login = (TextBlock)target;
			break;
		case 7:
			lbl_auth_register_date = (TextBlock)target;
			break;
		}
	}
}
