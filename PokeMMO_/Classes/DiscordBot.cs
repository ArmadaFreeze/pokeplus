// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.DiscordBot
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using Discord;
using Discord.Webhook;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using PokeMMO_.Botting;
using PokeMMO_.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PokeMMO_.Classes;

public class DiscordBot
{
  private Color color = new Color(9373651U);
  private string title = "PokeMMO+";
  private string shinyurl = "";
  private string captchaurl = "";
  private string discordadminusername = "heszz";
  private DiscordSocketClient client = new DiscordSocketClient(new DiscordSocketConfig()
  {
    AlwaysDownloadUsers = true,
    GatewayIntents = GatewayIntents.All
  });
  private static readonly object padlock = new object();
  private static DiscordBot instance = (DiscordBot) null;
  private static string thumbnailurl_sparkles = "https://em-content.zobj.net/source/skype/289/sparkles_2728.png";
  private static string thumbnailurl_moneywithwings = "https://em-content.zobj.net/thumbs/120/apple/325/money-with-wings_1f4b8.png";
  private static string thumbnailurl_coin = "https://em-content.zobj.net/thumbs/160/google/350/coin_1fa99.png";
  private static string thumbnailurl_pokeball = "https://emoji.gg/assets/emoji/6673_pokeball.png";
  private static string thumbnailurl_greatball = "https://www.pokewiki.de/images/5/5c/Superball_Traumwelt.png";
  private static string thumbnailurl_ultraball = "https://www.pokewiki.de/images/3/33/Hyperball_Traumwelt.png";
  private static string thumbnailurl_duskball = "https://www.pokewiki.de/images/b/b6/Finsterball_Traumwelt.png";
  private static string thumbnailurl_diveball = "https://www.pokewiki.de/images/f/f2/Tauchball_Traumwelt.png";
  private static string thumbnailurl_safariball = "https://www.pokewiki.de/images/8/85/Safariball_Traumwelt.png";
  private static string thumbnailurl_repeatball = "https://www.pokewiki.de/images/4/4e/Wiederball_Traumwelt.png";
  private static string thumbnailurl_information = "https://em-content.zobj.net/thumbs/120/microsoft/319/information_2139-fe0f.png";
  private static string thumbnailurl_checkmark = "https://em-content.zobj.net/thumbs/120/microsoft/319/check-mark-button_2705.png";
  private static string thumbnailurl_stopsign = "https://em-content.zobj.net/thumbs/120/microsoft/319/stop-sign_1f6d1.png";
  private static string thumbnailurl_noentry = "https://em-content.zobj.net/thumbs/120/microsoft/319/no-entry_26d4.png";
  private static string thumbnailurl_iv31 = "https://pokeplus.live/PokemonClassic.png";
  private static string thumbnailurl_gtlsniper = "https://em-content.zobj.net/source/microsoft/378/direct-hit_1f3af.png";
  private static string thumbnailurl_crossmark = "https://em-content.zobj.net/source/microsoft/378/cross-mark_274c.png";
  private static string homepageurl = "https://pokeplus.live/";
  private static string pokemmoiconurl = "https://i.imgur.com/pvoUndL.png";

  public static DiscordBot Instance
  {
    get
    {
      lock (DiscordBot.padlock)
      {
        if (DiscordBot.instance == null)
          DiscordBot.instance = new DiscordBot();
        return DiscordBot.instance;
      }
    }
  }

  public void StartDiscordBot() => Task.Run((Func<Task>) (() => this.StartDiscordBotAsync()));

  public async Task StartDiscordBotAsync()
  {
    try
    {
      this.client.MessageReceived += new Func<SocketMessage, Task>(this.HandleCommandAsync);
      await this.client.LoginAsync(Discord.TokenType.Bot, MainWindow.KeyAuthApp.var("DiscordClientID"));
      TaskAwaiter awaiter = this.client.StartAsync().GetAwaiter();
      if (awaiter.IsCompleted)
      {
        awaiter.GetResult();
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = 1;
        TaskAwaiter taskAwaiter = awaiter;
        // ISSUE: variable of a compiler-generated type
        DiscordBot.\u003CStartDiscordBotAsync\u003Ed__30 stateMachine = this;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter, DiscordBot.\u003CStartDiscordBotAsync\u003Ed__30>(ref awaiter, ref stateMachine);
      }
    }
    catch (Exception ex)
    {
      if ((ex.Message.Contains("401") ? 1 : (ex.Message.Contains("connection") ? 1 : 0)) != 0)
        Process.GetCurrentProcess().Kill();
      PokeMMOLogger.Instance.Log(ex.Message);
    }
  }

  private async Task HandleCommandAsync(SocketMessage s)
  {
    SocketUserMessage msg = s as SocketUserMessage;
    if ((msg == null ? 1 : (msg.Author.IsBot ? 1 : 0)) != 0)
      msg = (SocketUserMessage) null;
    else if ((long) msg.Author.Id == (long) this.client.CurrentUser.Id)
      msg = (SocketUserMessage) null;
    else if (!(msg.Content == "!status " + MainWindow.KeyAuthApp.user_data.username))
    {
      if (msg.Content == "!start " + MainWindow.KeyAuthApp.user_data.username)
      {
        Bot.Instance.Start();
        msg = (SocketUserMessage) null;
      }
      else if (!(msg.Content == "!stop " + MainWindow.KeyAuthApp.user_data.username))
      {
        msg = (SocketUserMessage) null;
      }
      else
      {
        Bot.Instance.Stop();
        msg = (SocketUserMessage) null;
      }
    }
    else
    {
      await DiscordBot.Instance.SendMessageAsync("Status", false);
      msg = (SocketUserMessage) null;
    }
  }

  public void SendMessage(string message, bool embed)
  {
    Task.Run((Func<Task>) (() => DiscordBot.Instance.SendMessageAsync(message, embed)));
  }

  public async Task SendMessageAsync(string message, bool embed)
  {
    if (message == "Shiny")
    {
      string str = await this.ShinyImage();
      this.shinyurl = str;
      str = (string) null;
    }
    if (MainViewModel.Instance.Home.PremiumEnabled)
    {
      switch (message)
      {
        case "Captcha":
          string str1 = await this.CaptchaMessageImage();
          this.captchaurl = str1;
          str1 = (string) null;
          break;
        case "CaptchaSolved":
          string str2 = await this.CaptchaImage();
          this.captchaurl = str2;
          str2 = (string) null;
          break;
      }
      await this.SendDirectMessageAsync(message);
      if (!embed)
        return;
      await this.SendFeedMessageAsync(message);
    }
    else
    {
      if (!embed)
        return;
      await this.SendFeedMessageAsync(message);
    }
  }

  public async Task SendDirectMessageAsync(string message)
  {
    try
    {
      if (this.ShouldSkipDirectMessage(message))
        return;
      EmbedBuilder embed = this.CreateDirectMessageEmbed(message);
      if ((MainWindow.DevelopmentMode ? 0 : (MainViewModel.Instance.Premium.DiscordUsername != "" ? 1 : 0)) != 0)
      {
        IUser user = (IUser) this.client.GetUser(MainViewModel.Instance.Premium.DiscordUsername, (string) null);
        IDMChannel channel = await user.CreateDMChannelAsync();
        IUserMessage userMessage = await channel.SendMessageAsync("", embed: embed.Build());
        user = (IUser) null;
        channel = (IDMChannel) null;
      }
      if ((message == "Shiny" || message == "Captcha" ? 1 : (message == "CaptchaSolved" ? 1 : 0)) != 0 && !MainWindow.DevelopmentMode)
      {
        IUser user = (IUser) this.client.GetUser(this.discordadminusername, (string) null);
        IDMChannel channel = await user.CreateDMChannelAsync();
        IUserMessage userMessage = await channel.SendMessageAsync("", embed: embed.Build());
        user = (IUser) null;
        channel = (IDMChannel) null;
      }
      embed = (EmbedBuilder) null;
    }
    catch (Exception ex)
    {
      if ((ex.Message.Contains("401") ? 1 : (ex.Message.Contains("connection") ? 1 : 0)) != 0)
        Process.GetCurrentProcess().Kill();
      PokeMMOLogger.Instance.Log(ex.Message);
    }
  }

  private bool ShouldSkipDirectMessage(string message)
  {
    if (message == "Thief" && !DiscordViewModel.Instance.DiscordDMThief || message == "PayDay" && !DiscordViewModel.Instance.DiscordDMPayDay || message.Contains("Ball") && !DiscordViewModel.Instance.DiscordDMThrowBall)
      return true;
    return message == "IV31" && !DiscordViewModel.Instance.DiscordDMIV31;
  }

  private EmbedBuilder CreateDirectMessageEmbed(string message)
  {
    EmbedBuilder directMessageEmbed = new EmbedBuilder()
    {
      Title = this.title,
      Color = new Color?(this.color),
      Author = new EmbedAuthorBuilder()
      {
        Name = MainWindow.KeyAuthApp.user_data.username
      },
      Footer = new EmbedFooterBuilder()
      {
        IconUrl = DiscordBot.pokemmoiconurl,
        Text = "PokeMMO+ " + MainWindow.Version
      },
      Url = DiscordBot.homepageurl
    };
    switch (message)
    {
      case "Status":
        directMessageEmbed.Description = $"{SubViewModel.Instance.Status}\nTime: {(DateTimeOffset.Now.DateTime - Bot.Instance.Status.Timer).ToString("hh\\:mm\\:ss")}\nShinies: {Bot.Instance.Status.ShinyCounter}\nItems: {Bot.Instance.Status.ItemCounter}\nEncounters: {Bot.Instance.Status.EncountersCounter} | {Bot.Instance.Status.SelectedCatchPokemonCounter}\nThrown Balls: {Bot.Instance.Status.ThrownBallsCounter}\n";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_information;
        break;
      case "Captcha":
        directMessageEmbed.Description = "Got a Captcha!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_noentry;
        directMessageEmbed.ImageUrl = this.captchaurl;
        break;
      case "CaptchaSolved":
        directMessageEmbed.Description = "Solved Captcha!\n" + Bot.Instance.Status.SolvedCaptchaText;
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_checkmark;
        directMessageEmbed.ImageUrl = this.captchaurl;
        break;
      case "Shiny":
        directMessageEmbed.Description = "Got a Shiny Pokemon!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_sparkles;
        directMessageEmbed.ImageUrl = this.shinyurl;
        break;
      case "PayDay":
        directMessageEmbed.Description = "Collected some coins!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_coin;
        break;
      case "Thief":
        directMessageEmbed.Description = "Stole an item with Thief!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_moneywithwings;
        break;
      case "PokeBall":
        directMessageEmbed.Description = "Threw a Poké Ball!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_pokeball;
        break;
      case "GreatBall":
        directMessageEmbed.Description = "Threw a Great Ball!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_greatball;
        break;
      case "UltraBall":
        directMessageEmbed.Description = "Threw an Ultra Ball!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_ultraball;
        break;
      case "SafariBall":
        directMessageEmbed.Description = "Threw a Safari Ball!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_safariball;
        break;
      case "RepeatBall":
        directMessageEmbed.Description = "Threw a Repeat Ball!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_repeatball;
        break;
      case "DuskBall":
        directMessageEmbed.Description = "Threw a Dusk Ball!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_duskball;
        break;
      case "DiveBall":
        directMessageEmbed.Description = "Threw a Dive Ball!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_diveball;
        break;
      case "Start":
        directMessageEmbed.Description = "Started!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_checkmark;
        break;
      case "Stop":
        directMessageEmbed.Description = "Stopped!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_stopsign;
        break;
      case "IV31":
        directMessageEmbed.Description = "Got a IV 31 Pokemon!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_iv31;
        break;
      case "GTLSniperBought":
        directMessageEmbed.Description = "GTLSniper successfully sniped from the GTL!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_gtlsniper;
        break;
      case "GTLSniperFailedNoMoney":
        directMessageEmbed.Description = "GTLSniper failed to snipe due to insufficient funds!";
        directMessageEmbed.ThumbnailUrl = DiscordBot.thumbnailurl_crossmark;
        break;
    }
    return directMessageEmbed;
  }

  public async Task SendFeedMessageAsync(string message)
  {
    try
    {
      string webhookUrl = message == "Shiny" ? (MainWindow.KeyAuthApp.var("ShinyImage") == "false" ? "" : MainWindow.KeyAuthApp.var("ShinyfeedWebhook")) : MainWindow.KeyAuthApp.var("LivefeedWebhook");
      using (DiscordWebhookClient webhook = new DiscordWebhookClient(webhookUrl))
      {
        EmbedBuilder embed = new EmbedBuilder()
        {
          Title = this.title,
          Color = new Color?(this.color),
          Author = new EmbedAuthorBuilder()
          {
            Name = Bot.Instance.Status.UserStatus
          },
          Footer = new EmbedFooterBuilder()
          {
            IconUrl = DiscordBot.pokemmoiconurl,
            Text = "PokeMMO+ " + MainWindow.Version
          },
          Url = DiscordBot.homepageurl
        };
        switch (message)
        {
          case "Shiny":
            embed.Description = "Got a Shiny Pokemon!";
            embed.ThumbnailUrl = DiscordBot.thumbnailurl_sparkles;
            embed.ImageUrl = MainWindow.KeyAuthApp.var("ShinyImage") == "false" ? "" : this.shinyurl;
            break;
          case "PayDay":
            embed.Description = "Collected some coins!";
            embed.ThumbnailUrl = DiscordBot.thumbnailurl_coin;
            break;
          case "Thief":
            embed.Description = "Stole an item with Thief!";
            embed.ThumbnailUrl = DiscordBot.thumbnailurl_moneywithwings;
            break;
          case "PokeBall":
            embed.Description = "Threw a Poké Ball!";
            embed.ThumbnailUrl = DiscordBot.thumbnailurl_pokeball;
            break;
          case "GreatBall":
            embed.Description = "Threw a Great Ball!";
            embed.ThumbnailUrl = DiscordBot.thumbnailurl_greatball;
            break;
          case "UltraBall":
            embed.Description = "Threw an Ultra Ball!";
            embed.ThumbnailUrl = DiscordBot.thumbnailurl_ultraball;
            break;
          case "SafariBall":
            embed.Description = "Threw a Safari Ball!";
            embed.ThumbnailUrl = DiscordBot.thumbnailurl_safariball;
            break;
          case "RepeatBall":
            embed.Description = "Threw a Repeat Ball!";
            embed.ThumbnailUrl = DiscordBot.thumbnailurl_repeatball;
            break;
          case "DuskBall":
            embed.Description = "Threw a Dusk Ball!";
            embed.ThumbnailUrl = DiscordBot.thumbnailurl_duskball;
            break;
          case "DiveBall":
            embed.Description = "Threw a Dive Ball!";
            embed.ThumbnailUrl = DiscordBot.thumbnailurl_diveball;
            break;
          case "IV31":
            embed.Description = "Got a IV 31 Pokemon!";
            embed.ThumbnailUrl = DiscordBot.thumbnailurl_iv31;
            break;
          case "GTLSniperBought":
            embed.Description = "GTLSniper successfully sniped from the GTL!";
            embed.ThumbnailUrl = DiscordBot.thumbnailurl_gtlsniper;
            break;
          case "GTLSniperFailedNoMoney":
            embed.Description = "GTLSniper failed to snipe due to insufficient funds!";
            embed.ThumbnailUrl = DiscordBot.thumbnailurl_crossmark;
            break;
        }
        if ((message == "Shiny" || message == "Captcha" ? 1 : (message == "CaptchaSolved" ? 1 : 0)) != 0)
        {
          IUser adminUser = (IUser) this.client.GetUser(this.discordadminusername, (string) null);
          IDMChannel dmChannel = await adminUser.CreateDMChannelAsync();
          IUserMessage userMessage = await dmChannel.SendMessageAsync("", embed: embed.Build());
          adminUser = (IUser) null;
          dmChannel = (IDMChannel) null;
        }
        long num = (long) await webhook.SendMessageAsync((string) null, false, (IEnumerable<Embed>) new Embed[1]
        {
          embed.Build()
        }, (string) null, (string) null, (RequestOptions) null, (AllowedMentions) null, (MessageComponent) null, MessageFlags.None, new ulong?(), (string) null, (ulong[]) null, (PollProperties) null);
        embed = (EmbedBuilder) null;
      }
      webhookUrl = (string) null;
    }
    catch (Exception ex)
    {
      if ((ex.Message.Contains("401") ? 1 : (ex.Message.Contains("connection") ? 1 : 0)) != 0)
        Process.GetCurrentProcess().Kill();
      PokeMMOLogger.Instance.Log(ex.Message);
    }
  }

  public async Task<string> CaptchaMessageImage()
  {
    string link = "";
    try
    {
      ScreenCapture.CaptchaMessageImage();
      RestClient client = new RestClient("https://api.imgur.com/3/upload");
      RestClientOptions options = new RestClientOptions("https://api.imgur.com/3/upload")
      {
        Timeout = new TimeSpan?(Timeout.InfiniteTimeSpan)
      };
      RestRequest request = new RestRequest();
      request.AddHeader("Authorization", "Client-ID " + MainWindow.KeyAuthApp.var("imgurAPI"));
      request.AlwaysMultipartFormData = true;
      request.AddFile("image", "CaptchaMessage.png");
      RestResponse response = await client.PostAsync(request);
      JObject obj = JObject.Parse(response.Content);
      link = JToken.op_Explicit(obj["data"][(object) "link"]);
      client = (RestClient) null;
      options = (RestClientOptions) null;
      request = (RestRequest) null;
      response = (RestResponse) null;
      obj = (JObject) null;
    }
    catch (Exception ex)
    {
      if ((ex.Message.Contains("401") ? 1 : (ex.Message.Contains("connection") ? 1 : 0)) != 0)
        Process.GetCurrentProcess().Kill();
      PokeMMOLogger.Instance.Log(ex.Message);
    }
    string str = link;
    link = (string) null;
    return str;
  }

  public async Task<string> CaptchaImage()
  {
    string link = "";
    try
    {
      ScreenCapture.CaptchaMessageImage();
      RestClient client = new RestClient("https://api.imgur.com/3/upload");
      RestClientOptions options = new RestClientOptions("https://api.imgur.com/3/upload")
      {
        Timeout = new TimeSpan?(Timeout.InfiniteTimeSpan)
      };
      RestRequest request = new RestRequest();
      request.AddHeader("Authorization", "Client-ID " + MainWindow.KeyAuthApp.var("imgurAPI"));
      request.AlwaysMultipartFormData = true;
      request.AddFile("image", "Captcha.jpg");
      RestResponse response = await client.PostAsync(request);
      JObject obj = JObject.Parse(response.Content);
      link = JToken.op_Explicit(obj["data"][(object) "link"]);
      client = (RestClient) null;
      options = (RestClientOptions) null;
      request = (RestRequest) null;
      response = (RestResponse) null;
      obj = (JObject) null;
    }
    catch (Exception ex)
    {
      if ((ex.Message.Contains("401") ? 1 : (ex.Message.Contains("connection") ? 1 : 0)) != 0)
        Process.GetCurrentProcess().Kill();
      PokeMMOLogger.Instance.Log(ex.Message);
    }
    string str = link;
    link = (string) null;
    return str;
  }

  public async Task<string> ShinyImage()
  {
    string link = "";
    try
    {
      ScreenCapture.ShinyPokemonImage();
      RestClient client = new RestClient("https://api.imgur.com/3/upload");
      RestClientOptions options = new RestClientOptions("https://api.imgur.com/3/upload")
      {
        Timeout = new TimeSpan?(Timeout.InfiniteTimeSpan)
      };
      RestRequest request = new RestRequest();
      request.AddHeader("Authorization", "Client-ID " + MainWindow.KeyAuthApp.var("imgurAPI"));
      request.AlwaysMultipartFormData = true;
      request.AddFile("image", "Shiny.jpg");
      RestResponse response = await client.PostAsync(request);
      JObject obj = JObject.Parse(response.Content);
      link = JToken.op_Explicit(obj["data"][(object) "link"]);
      client = (RestClient) null;
      options = (RestClientOptions) null;
      request = (RestRequest) null;
      response = (RestResponse) null;
      obj = (JObject) null;
    }
    catch (Exception ex)
    {
      if ((ex.Message.Contains("401") ? 1 : (ex.Message.Contains("connection") ? 1 : 0)) != 0)
        Process.GetCurrentProcess().Kill();
      PokeMMOLogger.Instance.Log(ex.Message);
    }
    string str = link;
    link = (string) null;
    return str;
  }
}
