using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Xceed.Wpf.Toolkit;

namespace PokeMMO_.Views;

public class SecurityPage : UserControl, IComponentConnector
{
	internal CheckBox chk_stoppm;

	internal CheckBox chk_alertpm;

	internal CheckBox chk_stopwalkcycle;

	internal CheckBox chk_alertwalkcycle;

	internal CheckBox chk_alertsweetcent;

	internal IntegerUpDown walkcycles;

	internal TextBlock lbl_text_walkcycles;

	internal TextBlock lbl_walkspeed_in_ms;

	internal IntegerUpDown walkfromrnd;

	internal TextBlock lbl_text_to;

	internal IntegerUpDown walktornd;

	internal CheckBox chk_turnofftimer;

	internal IntegerUpDown turnoff;

	internal TextBlock lbl_AutomaticCatpchaSolver;

	private bool _contentLoaded;

	public SecurityPage()
	{
		InitializeComponent();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/PokeMMO+;component/views/securitypage.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		switch (connectionId)
		{
		default:
			_contentLoaded = true;
			break;
		case 1:
			chk_stoppm = (CheckBox)target;
			break;
		case 2:
			chk_alertpm = (CheckBox)target;
			break;
		case 3:
			chk_stopwalkcycle = (CheckBox)target;
			break;
		case 4:
			chk_alertwalkcycle = (CheckBox)target;
			break;
		case 5:
			chk_alertsweetcent = (CheckBox)target;
			break;
		case 6:
			walkcycles = (IntegerUpDown)target;
			break;
		case 7:
			lbl_text_walkcycles = (TextBlock)target;
			break;
		case 8:
			lbl_walkspeed_in_ms = (TextBlock)target;
			break;
		case 9:
			walkfromrnd = (IntegerUpDown)target;
			break;
		case 10:
			lbl_text_to = (TextBlock)target;
			break;
		case 11:
			walktornd = (IntegerUpDown)target;
			break;
		case 12:
			chk_turnofftimer = (CheckBox)target;
			break;
		case 13:
			turnoff = (IntegerUpDown)target;
			break;
		case 14:
			lbl_AutomaticCatpchaSolver = (TextBlock)target;
			break;
		}
	}
}
