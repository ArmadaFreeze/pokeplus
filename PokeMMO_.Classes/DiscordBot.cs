using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using PokeMMO_.Botting;
using PokeMMO_.ViewModels;
using RestSharp;

namespace PokeMMO_.Classes;

public class DiscordBot
{
	private readonly Color color = new Color(9373651u);

	private readonly string title = "PokeMMO+";

	private string shinyurl = "";

	private string captchaurl = "";

	private readonly string discordadminusername = "heszz";

	private bool isReady = false;

	private readonly DiscordSocketClient client;

	private static readonly Lazy<DiscordBot> _instance = new Lazy<DiscordBot>(() => new DiscordBot());

	private static readonly string homepageurl = "https://pokeplus.live/";

	private static readonly string pokemmoiconurl = "https://i.imgur.com/pvoUndL.png";

	private static readonly Dictionary<string, EmbedData> MessageEmbedData = new Dictionary<string, EmbedData>
	{
		{
			"Shiny",
			new EmbedData
			{
				Description = "Got a Shiny Pokemon!",
				ThumbnailUrl = "https://em-content.zobj.net/source/skype/289/sparkles_2728.png"
			}
		},
		{
			"PayDay",
			new EmbedData
			{
				Description = "Collected some coins!",
				ThumbnailUrl = "https://em-content.zobj.net/thumbs/160/google/350/coin_1fa99.png"
			}
		},
		{
			"Thief",
			new EmbedData
			{
				Description = "Stole an item with Thief!",
				ThumbnailUrl = "https://em-content.zobj.net/thumbs/120/apple/325/money-with-wings_1f4b8.png"
			}
		},
		{
			"PokeBall",
			new EmbedData
			{
				Description = "Threw a Poké Ball!",
				ThumbnailUrl = "https://emoji.gg/assets/emoji/6673_pokeball.png"
			}
		},
		{
			"GreatBall",
			new EmbedData
			{
				Description = "Threw a Great Ball!",
				ThumbnailUrl = "https://www.pokewiki.de/images/5/5c/Superball_Traumwelt.png"
			}
		},
		{
			"UltraBall",
			new EmbedData
			{
				Description = "Threw an Ultra Ball!",
				ThumbnailUrl = "https://www.pokewiki.de/images/3/33/Hyperball_Traumwelt.png"
			}
		},
		{
			"SafariBall",
			new EmbedData
			{
				Description = "Threw a Safari Ball!",
				ThumbnailUrl = "https://www.pokewiki.de/images/8/85/Safariball_Traumwelt.png"
			}
		},
		{
			"RepeatBall",
			new EmbedData
			{
				Description = "Threw a Repeat Ball!",
				ThumbnailUrl = "https://www.pokewiki.de/images/4/4e/Wiederball_Traumwelt.png"
			}
		},
		{
			"DuskBall",
			new EmbedData
			{
				Description = "Threw a Dusk Ball!",
				ThumbnailUrl = "https://www.pokewiki.de/images/b/b6/Finsterball_Traumwelt.png"
			}
		},
		{
			"DiveBall",
			new EmbedData
			{
				Description = "Threw a Dive Ball!",
				ThumbnailUrl = "https://www.pokewiki.de/images/f/f2/Tauchball_Traumwelt.png"
			}
		},
		{
			"Start",
			new EmbedData
			{
				Description = "Started!",
				ThumbnailUrl = "https://em-content.zobj.net/thumbs/120/microsoft/319/check-mark-button_2705.png"
			}
		},
		{
			"Stop",
			new EmbedData
			{
				Description = "Stopped!",
				ThumbnailUrl = "https://em-content.zobj.net/thumbs/120/microsoft/319/stop-sign_1f6d1.png"
			}
		},
		{
			"IV31",
			new EmbedData
			{
				Description = "Got a IV 31 Pokemon!",
				ThumbnailUrl = "https://dl.pokeplus.live/PokemonClassic.png"
			}
		},
		{
			"GTLSniperBought",
			new EmbedData
			{
				Description = "GTLSniper successfully sniped from the GTL!",
				ThumbnailUrl = "https://em-content.zobj.net/source/microsoft/378/direct-hit_1f3af.png"
			}
		},
		{
			"GTLSniperFailedNoMoney",
			new EmbedData
			{
				Description = "GTLSniper failed to snipe due to insufficient funds!",
				ThumbnailUrl = "https://em-content.zobj.net/source/microsoft/378/cross-mark_274c.png"
			}
		},
		{
			"Captcha",
			new EmbedData
			{
				Description = "Got a Captcha!",
				ThumbnailUrl = "https://em-content.zobj.net/thumbs/120/microsoft/319/no-entry_26d4.png"
			}
		},
		{
			"CaptchaSolved",
			new EmbedData
			{
				Description = "Solved Captcha!",
				ThumbnailUrl = "https://em-content.zobj.net/thumbs/120/microsoft/319/check-mark-button_2705.png"
			}
		},
		{
			"Status",
			new EmbedData
			{
				Description = "",
				ThumbnailUrl = "https://em-content.zobj.net/thumbs/120/microsoft/319/information_2139-fe0f.png"
			}
		}
	};

	public static DiscordBot Instance => _instance.Value;

	public void StartDiscordBot()
	{
		Task.Run(() => StartDiscordBotAsync());
	}

	public async Task StartDiscordBotAsync()
	{
		try
		{
			((BaseSocketClient)client).add_MessageReceived((Func<SocketMessage, Task>)HandleCommandAsync);
			client.add_Ready((Func<Task>)delegate
			{
				isReady = true;
				PokeMMOLogger.Instance.Log($"Discord bot connected as {((SocketUser)((BaseSocketClient)client).get_CurrentUser()).get_Username()}#{((SocketUser)((BaseSocketClient)client).get_CurrentUser()).get_Discriminator()} in {((BaseSocketClient)client).get_Guilds().Count} guild(s).");
				return Task.CompletedTask;
			});
			client.add_Disconnected((Func<Exception, Task>)delegate(Exception ex)
			{
				PokeMMOLogger.Instance.Log("Discord bot disconnected: " + ex?.Message);
				return Task.CompletedTask;
			});
			await ((BaseDiscordClient)client).LoginAsync((TokenType)1, MainWindow.KeyAuthApp.var("DiscordClientID"), true);
			await ((BaseSocketClient)client).StartAsync();
			PokeMMOLogger.Instance.Log("Discord bot login and start called successfully.");
		}
		catch (Exception ex3)
		{
			Exception ex2 = ex3;
			PokeMMOLogger.Instance.Log("StartDiscordBot error: " + ex2.Message);
			if (ex2.Message.Contains("401") || ex2.Message.Contains("connection"))
			{
				Process.GetCurrentProcess().Kill();
			}
		}
	}

	private async Task HandleCommandAsync(SocketMessage s)
	{
		SocketUserMessage msg = (SocketUserMessage)(object)((s is SocketUserMessage) ? s : null);
		if (msg == null || ((SocketMessage)msg).get_Author().get_IsBot() || ((SocketEntity<ulong>)(object)((SocketMessage)msg).get_Author()).get_Id() == ((SocketEntity<ulong>)(object)((BaseSocketClient)client).get_CurrentUser()).get_Id())
		{
			return;
		}
		string username = MainWindow.KeyAuthApp.user_data.username;
		if (!(((SocketMessage)msg).get_Content() == "!status " + username))
		{
			if (!(((SocketMessage)msg).get_Content() == "!start " + username))
			{
				if (((SocketMessage)msg).get_Content() == "!stop " + username)
				{
					Bot.Instance.Stop();
				}
			}
			else
			{
				Bot.Instance.Start();
			}
		}
		else
		{
			await Instance.SendMessageAsync("Status", embed: false);
		}
	}

	public void SendMessage(string message, bool embed)
	{
		Task.Run(async delegate
		{
			try
			{
				await Instance.SendMessageAsync(message, embed);
			}
			catch (Exception ex2)
			{
				Exception ex = ex2;
				PokeMMOLogger.Instance.Log("SendMessage error (fire-and-forget): " + ex.Message);
			}
		});
	}

	public async Task SendMessageAsync(string message, bool embed)
	{
		if (isReady)
		{
			if (message == "Shiny")
			{
				shinyurl = await UploadImage(delegate
				{
					ScreenCapture.ShinyPokemonImage();
				}, "Shiny.jpg");
			}
			if (MainViewModel.Instance.Home.PremiumEnabled)
			{
				if (!(message == "Captcha"))
				{
					if (message == "CaptchaSolved")
					{
						captchaurl = await UploadImage(delegate
						{
							ScreenCapture.CaptchaMessageImage();
						}, "Captcha.jpg");
					}
				}
				else
				{
					captchaurl = await UploadImage(delegate
					{
						ScreenCapture.CaptchaMessageImage();
					}, "CaptchaMessage.png");
				}
				await SendDirectMessageAsync(message);
			}
			if (embed)
			{
				await SendFeedMessageAsync(message);
			}
		}
		else
		{
			PokeMMOLogger.Instance.Log("SendMessageAsync: Bot not ready yet, skipping '" + message + "'.");
		}
	}

	private EmbedBuilder BuildBaseEmbed(string authorName)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		EmbedBuilder val = new EmbedBuilder();
		val.set_Title(title);
		val.set_Color((Color?)color);
		EmbedAuthorBuilder val2 = new EmbedAuthorBuilder();
		val2.set_Name(authorName);
		val.set_Author(val2);
		EmbedFooterBuilder val3 = new EmbedFooterBuilder();
		val3.set_IconUrl(pokemmoiconurl);
		val3.set_Text("PokeMMO+ " + MainWindow.Version);
		val.set_Footer(val3);
		val.set_Url(homepageurl);
		return val;
	}

	private void ApplyEmbedData(EmbedBuilder embed, string message)
	{
		if (!MessageEmbedData.TryGetValue(message, out var value))
		{
			return;
		}
		embed.set_ThumbnailUrl(value.ThumbnailUrl);
		if (message == "Status")
		{
			embed.set_Description($"{SubViewModel.Instance.Status}\nTime: {DateTimeOffset.Now.DateTime - Bot.Instance.Status.Timer:hh\\:mm\\:ss}\nShinies: {Bot.Instance.Status.ShinyCounter}\nItems: {Bot.Instance.Status.ItemCounter}\nEncounters: {Bot.Instance.Status.EncountersCounter} | {Bot.Instance.Status.SelectedCatchPokemonCounter}\nThrown Balls: {Bot.Instance.Status.ThrownBallsCounter}\n");
		}
		else if (!(message == "CaptchaSolved"))
		{
			embed.set_Description(value.Description);
		}
		else
		{
			embed.set_Description("Solved Captcha!\n" + Bot.Instance.Status.SolvedCaptchaText);
		}
		if (!(message == "Shiny"))
		{
			if (message == "Captcha" || message == "CaptchaSolved")
			{
				embed.set_ImageUrl(captchaurl);
			}
		}
		else
		{
			embed.set_ImageUrl(shinyurl);
		}
	}

	public async Task SendDirectMessageAsync(string message)
	{
		try
		{
			if (ShouldSkipDirectMessage(message))
			{
				return;
			}
			EmbedBuilder embed = BuildBaseEmbed(MainWindow.KeyAuthApp.user_data.username);
			ApplyEmbedData(embed, message);
			if (!MainWindow.DevelopmentMode && MainViewModel.Instance.Premium.DiscordUsername != "")
			{
				IUser user = (IUser)(object)((BaseSocketClient)client).GetUser(MainViewModel.Instance.Premium.DiscordUsername, (string)null);
				if (user != null)
				{
					await ((IMessageChannel)(await user.CreateDMChannelAsync((RequestOptions)null))).SendMessageAsync("", false, embed.Build(), (RequestOptions)null, (AllowedMentions)null, (MessageReference)null, (MessageComponent)null, (ISticker[])null, (Embed[])null, (MessageFlags)0, (PollProperties)null);
				}
			}
			if ((message == "Shiny" || message == "Captcha" || message == "CaptchaSolved") && !MainWindow.DevelopmentMode)
			{
				IUser user2 = (IUser)(object)((BaseSocketClient)client).GetUser(discordadminusername, (string)null);
				if (user2 != null)
				{
					await ((IMessageChannel)(await user2.CreateDMChannelAsync((RequestOptions)null))).SendMessageAsync("", false, embed.Build(), (RequestOptions)null, (AllowedMentions)null, (MessageReference)null, (MessageComponent)null, (ISticker[])null, (Embed[])null, (MessageFlags)0, (PollProperties)null);
				}
			}
		}
		catch (Exception ex2)
		{
			Exception ex = ex2;
			PokeMMOLogger.Instance.Log("SendDirectMessage error: " + ex.Message);
			if (ex.Message.Contains("401") || ex.Message.Contains("connection"))
			{
				Process.GetCurrentProcess().Kill();
			}
		}
	}

	private bool ShouldSkipDirectMessage(string message)
	{
		return (message == "Thief" && !DiscordViewModel.Instance.DiscordDMThief) || (message == "PayDay" && !DiscordViewModel.Instance.DiscordDMPayDay) || (message.Contains("Ball") && !DiscordViewModel.Instance.DiscordDMThrowBall) || (message == "IV31" && !DiscordViewModel.Instance.DiscordDMIV31);
	}

	public async Task SendFeedMessageAsync(string message)
	{
		try
		{
			string channelIdStr = ((message == "Shiny") ? MainWindow.KeyAuthApp.var("ShinyfeedChannelId") : MainWindow.KeyAuthApp.var("LivefeedChannelId"));
			if (string.IsNullOrEmpty(channelIdStr) || !ulong.TryParse(channelIdStr, out var channelId))
			{
				PokeMMOLogger.Instance.Log("SendFeedMessage: Invalid or empty channel ID for '" + message + "'. Raw value: '" + channelIdStr + "'");
				return;
			}
			SocketChannel channel2 = ((BaseSocketClient)client).GetChannel(channelId);
			IMessageChannel channel = (IMessageChannel)(object)((channel2 is IMessageChannel) ? channel2 : null);
			if (channel != null)
			{
				EmbedBuilder embed = BuildBaseEmbed(Bot.Instance.Status.UserStatus);
				ApplyEmbedData(embed, message);
				if (message == "Shiny" && MainWindow.KeyAuthApp.var("ShinyImage") == "false")
				{
					embed.set_ImageUrl("");
				}
				if (message == "Shiny" || message == "Captcha" || message == "CaptchaSolved")
				{
					IUser adminUser = (IUser)(object)((BaseSocketClient)client).GetUser(discordadminusername, (string)null);
					if (adminUser != null)
					{
						await ((IMessageChannel)(await adminUser.CreateDMChannelAsync((RequestOptions)null))).SendMessageAsync("", false, embed.Build(), (RequestOptions)null, (AllowedMentions)null, (MessageReference)null, (MessageComponent)null, (ISticker[])null, (Embed[])null, (MessageFlags)0, (PollProperties)null);
					}
				}
				await channel.SendMessageAsync("", false, embed.Build(), (RequestOptions)null, (AllowedMentions)null, (MessageReference)null, (MessageComponent)null, (ISticker[])null, (Embed[])null, (MessageFlags)0, (PollProperties)null);
			}
			else
			{
				PokeMMOLogger.Instance.Log($"SendFeedMessage: Could not find channel {channelId} for '{message}'. Bot may not have access or is not connected.");
			}
		}
		catch (Exception ex2)
		{
			Exception ex = ex2;
			PokeMMOLogger.Instance.Log("SendFeedMessage error: " + ex.Message);
			if (ex.Message.Contains("401") || ex.Message.Contains("connection"))
			{
				Process.GetCurrentProcess().Kill();
			}
		}
	}

	private async Task<string> UploadImage(Action captureAction, string filename)
	{
		string link = "";
		try
		{
			captureAction();
			RestClient client = new RestClient("https://api.imgur.com/3/upload", (ConfigureRestClient)null, (ConfigureHeaders)null, (ConfigureSerialization)null);
			RestRequest request = new RestRequest();
			RestRequestExtensions.AddHeader(request, "Authorization", "Client-ID " + MainWindow.KeyAuthApp.var("imgurAPI"));
			request.set_AlwaysMultipartFormData(true);
			RestRequestExtensions.AddFile(request, "image", filename, (ContentType)null, (FileParameterOptions)null);
			JObject obj = JObject.Parse(((RestResponseBase)(await RestClientExtensions.PostAsync((IRestClient)(object)client, request, default(CancellationToken)))).get_Content());
			link = (string)obj.get_Item("data").get_Item((object)"link");
			return link;
		}
		catch (Exception ex2)
		{
			Exception ex = ex2;
			PokeMMOLogger.Instance.Log("UploadImage error: " + ex.Message);
			if (ex.Message.Contains("401") || ex.Message.Contains("connection"))
			{
				Process.GetCurrentProcess().Kill();
				return link;
			}
			return link;
		}
	}

	public DiscordBot()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		DiscordSocketConfig val = new DiscordSocketConfig();
		val.set_AlwaysDownloadUsers(true);
		val.set_GatewayIntents((GatewayIntents)53608447);
		client = new DiscordSocketClient(val);
		base._002Ector();
	}
}
