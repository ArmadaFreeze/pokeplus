using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Classes;

public class Unlocker : Window
{
	public void ShowCopyableInstruction(string message)
	{
		Window dialog = new Window
		{
			Title = "Verification Required",
			Width = 400.0,
			Height = 250.0,
			ResizeMode = ResizeMode.NoResize,
			WindowStartupLocation = WindowStartupLocation.CenterScreen,
			Topmost = true
		};
		TextBox element = new TextBox
		{
			Text = message,
			IsReadOnly = true,
			TextWrapping = TextWrapping.Wrap,
			VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
			Margin = new Thickness(10.0)
		};
		Button button = new Button
		{
			Content = "OK",
			Width = 70.0,
			Margin = new Thickness(10.0),
			HorizontalAlignment = HorizontalAlignment.Right
		};
		button.Click += delegate
		{
			dialog.Close();
		};
		StackPanel stackPanel = new StackPanel();
		stackPanel.Children.Add(element);
		stackPanel.Children.Add(button);
		dialog.Content = stackPanel;
		dialog.ShowDialog();
	}

	public async void NewUnlock()
	{
		try
		{
			string videoId = MainWindow.KeyAuthApp.var("YOUTUBE_LATEST_VIDEO_ID");
			string userIdentifier = WindowsIdentity.GetCurrent().User.Value;
			string videoUrl = "https://www.youtube.com/watch?v=" + videoId;
			Process.Start(new ProcessStartInfo
			{
				FileName = videoUrl,
				UseShellExecute = true
			});
			string message = "To unlock this bot for free, please:\n\n1\ufe0f\u20e3 Like the video\n2\ufe0f\u20e3 Subscribe to the channel\n3\ufe0f\u20e3 Comment your code below:\n\n\"" + userIdentifier + "\"\n\nOnce done, click OK to verify.";
			Application.Current.Dispatcher.Invoke(delegate
			{
				ShowCopyableInstruction(message);
			});
			Application.Current.Dispatcher.Invoke(delegate
			{
				TopMostMessageBox.Show("Verifying your comment... This may take up to 30 seconds.\nPress OK to start verification.", "Verifying", MessageBoxButton.OK, MessageBoxImage.Asterisk);
			});
			if (await HasUserCommented(videoId, userIdentifier))
			{
				UnlockStartButton();
				return;
			}
			TopMostMessageBox.Show("Comment not found. Please make sure you commented your code exactly as shown and try again.", "Not Verified", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			MainViewModel.Instance.Home.FreeEnabled = false;
		}
		catch (Exception ex2)
		{
			Exception ex = ex2;
			PokeMMOLogger.Instance.Log("NewUnlock error: " + ex.Message);
			TopMostMessageBox.Show("Verification failed. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
			MainViewModel.Instance.Home.FreeEnabled = false;
		}
	}

	private async Task<bool> HasUserCommented(string videoId, string uniqueCode)
	{
		string apiKey = MainWindow.KeyAuthApp.var("YOUTUBE_API_KEY");
		Initializer val = new Initializer();
		val.set_ApiKey(apiKey);
		val.set_ApplicationName("PokeMMO_Bot_Verifier");
		YouTubeService youtubeService = new YouTubeService(val);
		try
		{
			int[] delaysMs = new int[5] { 0, 5000, 5000, 5000, 5000 };
			for (int attempt = 0; attempt < delaysMs.Length; attempt++)
			{
				if (delaysMs[attempt] > 0)
				{
					await Task.Delay(delaysMs[attempt]);
				}
				ListRequest commentRequest = youtubeService.get_CommentThreads().List(Repeatable<string>.op_Implicit("snippet"));
				commentRequest.set_VideoId(videoId);
				commentRequest.set_SearchTerms(uniqueCode);
				commentRequest.set_TextFormat((TextFormatEnum?)(TextFormatEnum)2);
				commentRequest.set_Order((OrderEnum?)(OrderEnum)1);
				commentRequest.set_MaxResults((long?)100L);
				CommentThreadListResponse response = await ((ClientServiceRequest<CommentThreadListResponse>)(object)commentRequest).ExecuteAsync();
				if (((response != null) ? response.get_Items() : null) == null)
				{
					continue;
				}
				foreach (CommentThread item in response.get_Items())
				{
					object obj;
					if (item == null)
					{
						obj = null;
					}
					else
					{
						CommentThreadSnippet snippet = item.get_Snippet();
						if (snippet == null)
						{
							obj = null;
						}
						else
						{
							Comment topLevelComment = snippet.get_TopLevelComment();
							if (topLevelComment == null)
							{
								obj = null;
							}
							else
							{
								CommentSnippet snippet2 = topLevelComment.get_Snippet();
								obj = ((snippet2 != null) ? snippet2.get_TextDisplay() : null);
							}
						}
					}
					string comment = (string)obj;
					if (comment != null && comment.IndexOf(uniqueCode, StringComparison.OrdinalIgnoreCase) >= 0)
					{
						return true;
					}
				}
			}
			return false;
		}
		finally
		{
			((IDisposable)youtubeService)?.Dispose();
		}
	}

	public void UnlockStartButton()
	{
		Thread.Sleep(1000);
		MainViewModel.Instance.Home.FreeEnabled = true;
		MainViewModel.Instance.Home.StartEnabled = true;
		TopMostMessageBox.Show("Successfully Unlocked.", "Unlocked", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
		MainWindow.KeyAuthApp.log(Environment.UserName + " FREE Unlocked");
	}
}
