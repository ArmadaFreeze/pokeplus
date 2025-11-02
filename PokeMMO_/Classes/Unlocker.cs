// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.Unlocker
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using PokeMMO_.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PokeMMO_.Classes
{
    public class Unlocker : Window
    {
      public void ShowCopyableInstruction(string message)
      {
        Window window = new Window();
        window.Title = "Verification Required";
        window.Width = 400.0;
        window.Height = 250.0;
        window.ResizeMode = ResizeMode.NoResize;
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.Topmost = true;
        Window dialog = window;
        TextBox textBox = new TextBox();
        textBox.Text = message;
        textBox.IsReadOnly = true;
        textBox.TextWrapping = TextWrapping.Wrap;
        textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        textBox.Margin = new Thickness(10.0);
        TextBox element1 = textBox;
        Button button = new Button();
        button.Content = (object) "OK";
        button.Width = 70.0;
        button.Margin = new Thickness(10.0);
        button.HorizontalAlignment = HorizontalAlignment.Right;
        Button element2 = button;
        element2.Click += (RoutedEventHandler) ((sender, e) => dialog.Close());
        StackPanel stackPanel = new StackPanel();
        stackPanel.Children.Add((UIElement) element1);
        stackPanel.Children.Add((UIElement) element2);
        dialog.Content = (object) stackPanel;
        dialog.ShowDialog();
      }

      public async void NewUnlock()
      {
        string videoId = MainWindow.KeyAuthApp.var("YOUTUBE_LATEST_VIDEO_ID");
        string userIdentifier = WindowsIdentity.GetCurrent().User.Value;
        string videoUrl = "https://www.youtube.com/watch?v=" + videoId;
        Process.Start(new ProcessStartInfo()
        {
          FileName = videoUrl,
          UseShellExecute = true
        });
        string message = $"To unlock this bot for free, please:\n\n1️⃣ Like the video\n2️⃣ Subscribe to the channel\n3️⃣ Comment your code below:\n\n\"{userIdentifier}\"\n\nOnce done, click OK to verify.";
        Application.Current.Dispatcher.Invoke((Action) (() => this.ShowCopyableInstruction(message)));
        if (await this.HasUserCommented(videoId, userIdentifier))
        {
          this.UnlockStartButton();
          videoId = (string) null;
          userIdentifier = (string) null;
          videoUrl = (string) null;
        }
        else
        {
          int num = (int) MessageBox.Show("❌ Comment not found. Please make sure to comment your code exactly as shown.", "Not Verified", MessageBoxButton.OK, MessageBoxImage.Exclamation);
          MainViewModel.Instance.Home.FreeEnabled = false;
          videoId = (string) null;
          userIdentifier = (string) null;
          videoUrl = (string) null;
        }
      }

      private async Task<bool> HasUserCommented(string videoId, string uniqueCode)
      {
        string apiKey = MainWindow.KeyAuthApp.var("YOUTUBE_API_KEY");
        YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
        {
          ApiKey = apiKey,
          ApplicationName = "PokeMMO_Bot_Verifier"
        });
        CommentThreadsResource.ListRequest commentRequest = youtubeService.CommentThreads.List(Repeatable<string>.op_Implicit("snippet"));
        commentRequest.VideoId = videoId;
        commentRequest.TextFormat = new CommentThreadsResource.ListRequest.TextFormatEnum?(CommentThreadsResource.ListRequest.TextFormatEnum.PlainText);
        commentRequest.MaxResults = new long?(100L);
        CommentThreadListResponse response = await commentRequest.ExecuteAsync();
        foreach (CommentThread item in (IEnumerable<CommentThread>) response.Items)
        {
          string comment = item.Snippet.TopLevelComment.Snippet.TextDisplay;
          if (comment.IndexOf(uniqueCode, StringComparison.OrdinalIgnoreCase) >= 0)
            return true;
          comment = (string) null;
        }
        return false;
      }

      public void UnlockStartButton()
      {
        Thread.Sleep(1000);
        MainViewModel.Instance.Home.FreeEnabled = true;
        MainViewModel.Instance.Home.StartEnabled = true;
        int num = (int) MessageBox.Show("Successfully Unlocked.", "Unlocked", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        MainWindow.KeyAuthApp.log(Environment.UserName + " FREE Unlocked");
      }
    }
}
