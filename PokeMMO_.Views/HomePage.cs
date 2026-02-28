using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using PokeMMO_.Botting;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Views;

public class HomePage : UserControl, IComponentConnector
{
	internal HomePage MyHomePage;

	internal CheckBox chk_walk;

	internal CheckBox chk_fish;

	internal CheckBox chk_auto_walk;

	internal CheckBox chk_auto_sweet_cent;

	internal CheckBox chk_shiny;

	internal CheckBox chk_shinystop;

	internal CheckBox chk_skipdialog;

	internal CheckBox chk_skipevolve;

	internal CheckBox chk_learn_move_skip;

	internal CheckBox chk_login;

	internal CheckBox chk_lure;

	internal CheckBox chk_ether;

	internal CheckBox chk_sweet_cent;

	internal Button btn_start;

	internal Button btn_stop;

	private bool _contentLoaded;

	private Home H => MainViewModel.Instance.Home;

	public HomePage()
	{
		InitializeComponent();
	}

	private void ClearMovementModesExcept(string keep)
	{
		if (keep != "Walk")
		{
			H.Walk = false;
		}
		if (keep != "Fish")
		{
			H.Fish = false;
		}
		if (keep != "SweetScent")
		{
			H.SweetScent = false;
		}
		if (keep != "AutoWalkFish")
		{
			H.AutoWalkFish = false;
		}
		if (keep != "AutoSweetScent")
		{
			H.AutoSweetScent = false;
		}
		if (keep != "SafariAutoWalk")
		{
			H.SafariAutoWalk = false;
		}
		if (keep != "SafariAutoFish")
		{
			H.SafariAutoFish = false;
		}
	}

	private void chk_randomwalkpattern_Checked(object sender, RoutedEventArgs e)
	{
		H.SquaresWalkPattern = false;
	}

	private void chk_squareswalkpattern_Checked(object sender, RoutedEventArgs e)
	{
		H.RandomWalkPattern = false;
	}

	private void chk_walk_Checked(object sender, RoutedEventArgs e)
	{
		ClearMovementModesExcept("Walk");
	}

	private void chk_fish_Checked(object sender, RoutedEventArgs e)
	{
		ClearMovementModesExcept("Fish");
	}

	private void chk_sweet_cent_Checked(object sender, RoutedEventArgs e)
	{
		ClearMovementModesExcept("SweetScent");
	}

	private void chk_auto_walk_Checked(object sender, RoutedEventArgs e)
	{
		ClearMovementModesExcept("AutoWalkFish");
	}

	private void chk_auto_sweet_cent_Checked(object sender, RoutedEventArgs e)
	{
		ClearMovementModesExcept("AutoSweetScent");
	}

	private void chk_safari_auto_walk_Checked(object sender, RoutedEventArgs e)
	{
		ClearMovementModesExcept("SafariAutoWalk");
	}

	private void chk_safari_auto_fish_Checked(object sender, RoutedEventArgs e)
	{
		ClearMovementModesExcept("SafariAutoFish");
	}

	private void chk_rock_Checked(object sender, RoutedEventArgs e)
	{
		H.Bait = false;
	}

	private void chk_bait_Checked(object sender, RoutedEventArgs e)
	{
		H.Rock = false;
	}

	private void chk_shiny_Checked(object sender, RoutedEventArgs e)
	{
		H.StopOnShiny = false;
	}

	private void chk_shiny_stop_Checked(object sender, RoutedEventArgs e)
	{
		H.CatchShiny = false;
	}

	private void btn_test_Click(object sender, RoutedEventArgs e)
	{
		Bot.Instance.Actions.MailClaim();
	}

	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[DebuggerNonUserCode]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/PokeMMO+;component/views/homepage.xaml", UriKind.Relative);
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
			MyHomePage = (HomePage)target;
			break;
		case 2:
			((CheckBox)target).Checked += chk_rock_Checked;
			break;
		case 3:
			((CheckBox)target).Checked += chk_bait_Checked;
			break;
		case 4:
			chk_walk = (CheckBox)target;
			chk_walk.Checked += chk_walk_Checked;
			break;
		case 5:
			chk_fish = (CheckBox)target;
			chk_fish.Checked += chk_fish_Checked;
			break;
		case 6:
			chk_auto_walk = (CheckBox)target;
			chk_auto_walk.Checked += chk_auto_walk_Checked;
			break;
		case 7:
			chk_auto_sweet_cent = (CheckBox)target;
			chk_auto_sweet_cent.Checked += chk_auto_sweet_cent_Checked;
			break;
		case 8:
			((CheckBox)target).Checked += chk_safari_auto_walk_Checked;
			break;
		case 9:
			((CheckBox)target).Checked += chk_safari_auto_fish_Checked;
			break;
		case 10:
			((CheckBox)target).Checked += chk_squareswalkpattern_Checked;
			break;
		case 11:
			((CheckBox)target).Checked += chk_randomwalkpattern_Checked;
			break;
		case 12:
			chk_shiny = (CheckBox)target;
			chk_shiny.Checked += chk_shiny_Checked;
			break;
		case 13:
			chk_shinystop = (CheckBox)target;
			chk_shinystop.Checked += chk_shiny_stop_Checked;
			break;
		case 14:
			chk_skipdialog = (CheckBox)target;
			break;
		case 15:
			chk_skipevolve = (CheckBox)target;
			break;
		case 16:
			chk_learn_move_skip = (CheckBox)target;
			break;
		case 17:
			chk_login = (CheckBox)target;
			break;
		case 18:
			chk_lure = (CheckBox)target;
			break;
		case 19:
			chk_ether = (CheckBox)target;
			break;
		case 20:
			chk_sweet_cent = (CheckBox)target;
			chk_sweet_cent.Checked += chk_sweet_cent_Checked;
			break;
		case 21:
			btn_start = (Button)target;
			break;
		case 22:
			btn_stop = (Button)target;
			break;
		}
	}
}
