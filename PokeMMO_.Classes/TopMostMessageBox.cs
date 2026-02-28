using System.Windows;

namespace PokeMMO_.Classes;

public static class TopMostMessageBox
{
	public static MessageBoxResult Show(string messageBoxText)
	{
		return Show(messageBoxText, "", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None);
	}

	public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
	{
		return Show(messageBoxText, caption, button, icon, MessageBoxResult.None);
	}

	public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
	{
		_003C_003Ec__DisplayClass2_0 CS_0024_003C_003E8__locals0 = new _003C_003Ec__DisplayClass2_0();
		CS_0024_003C_003E8__locals0.messageBoxText = messageBoxText;
		CS_0024_003C_003E8__locals0.caption = caption;
		CS_0024_003C_003E8__locals0.button = button;
		CS_0024_003C_003E8__locals0.icon = icon;
		CS_0024_003C_003E8__locals0.defaultResult = defaultResult;
		CS_0024_003C_003E8__locals0.result = MessageBoxResult.None;
		Application current = Application.Current;
		if (current != null && (current.Dispatcher?.CheckAccess()).GetValueOrDefault())
		{
			CS_0024_003C_003E8__locals0.method_0();
		}
		else if (Application.Current?.Dispatcher != null)
		{
			Application.Current.Dispatcher.Invoke(delegate
			{
				Window window = new Window
				{
					Width = 0.0,
					Height = 0.0,
					WindowStyle = WindowStyle.None,
					ShowInTaskbar = false,
					Topmost = true,
					WindowStartupLocation = WindowStartupLocation.CenterScreen
				};
				window.Show();
				CS_0024_003C_003E8__locals0.result = MessageBox.Show(window, CS_0024_003C_003E8__locals0.messageBoxText, CS_0024_003C_003E8__locals0.caption, CS_0024_003C_003E8__locals0.button, CS_0024_003C_003E8__locals0.icon, CS_0024_003C_003E8__locals0.defaultResult);
				window.Close();
			});
		}
		else
		{
			CS_0024_003C_003E8__locals0.result = MessageBox.Show(CS_0024_003C_003E8__locals0.messageBoxText, CS_0024_003C_003E8__locals0.caption, CS_0024_003C_003E8__locals0.button, CS_0024_003C_003E8__locals0.icon, CS_0024_003C_003E8__locals0.defaultResult);
		}
		return CS_0024_003C_003E8__locals0.result;
	}
}
