using System.Windows;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Classes;

public static class UIHelper
{
	public static void SetStatus(string message)
	{
		Application.Current.Dispatcher.Invoke(() => SubViewModel.Instance.Status = message);
	}
}
