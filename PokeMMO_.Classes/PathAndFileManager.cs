using System;
using System.IO;
using System.Linq;
using System.Windows;
using PokeMMO_.Botting;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;

namespace PokeMMO_.Classes;

public class PathAndFileManager
{
	public static void SelectDefaultPath()
	{
		try
		{
			string defaultPath = MainViewModel.Instance.Settings.DefaultPath;
			string text = FolderPicker.ShowDialog(Application.Current.MainWindow, "Select PokeMMO Installation Folder", defaultPath);
			if (text != null)
			{
				if (text.Contains("themes"))
				{
					TopMostMessageBox.Show("Make sure you have choosen the base directory of PokeMMO.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
					return;
				}
				MainViewModel.Instance.Settings.DefaultPath = text;
				Configuration.Save();
				ReplacePropertiesAndGFXFile(messagebox: true);
			}
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log("SelectDefaultPath error: " + ex.Message);
		}
	}

	public static void ReadPropertiesFile()
	{
		try
		{
			Bot.Instance.Settings.Data.Clear();
			string[] array = File.ReadAllLines(MainViewModel.Instance.Settings.DefaultPath + "\\config\\main.properties");
			foreach (string text in array)
			{
				Bot.Instance.Settings.Data.Add(text.Split('=')[0], string.Join("=", text.Split('=').Skip(1).ToArray()));
			}
		}
		catch
		{
			TopMostMessageBox.Show("Make sure you have choosen the base directory of PokeMMO.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
			Bot.Instance.Stop();
		}
	}

	public static void ReplacePropertiesAndGFXFile(bool messagebox)
	{
		try
		{
			string defaultPath = MainViewModel.Instance.Settings.DefaultPath;
			string text = defaultPath + "\\config\\main.properties";
			string text2 = defaultPath + "\\data\\themes\\default\\gfx.xml";
			if (File.Exists(text) && File.Exists(text2) && File.Exists("gfx.xml"))
			{
				Properties properties = new Properties(text);
				if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
				{
					properties.set("client.graphics.width", 1920);
					properties.set("client.graphics.height", 1080);
				}
				else
				{
					properties.set("client.graphics.width", 1280);
					properties.set("client.graphics.height", 720);
				}
				properties.set("client.graphics.max_fpx", 60);
				properties.set("client.gui.scale.guiscale", 1.0);
				properties.set("client.graphics.gba.zoom_level", -1);
				properties.set("client.graphics.battle.size", 100);
				properties.set("client.graphics.graphics_api", "OpenGL");
				properties.set("client.graphics.vsync", false);
				properties.set("client.graphics.render.overworld_in_battle", false);
				properties.set("client.graphics.render.battlebg", false);
				properties.set("client.graphics.render_border", false);
				properties.set("client.ui.theme", "default");
				properties.set("client.ui.smallhud", true);
				properties.set("client.gui.scale.hidpifont", true);
				properties.set("client.ui.chat.transparency", 100);
				properties.set("client.gui.hud.chatframe.x", 0);
				properties.set("client.gui.hud.chatframe.y", 0);
				properties.set("client.gui.hud.chatframe.width", 411);
				properties.set("client.gui.hud.chatframe.height", 176);
				properties.set("client.gui.hud.chatframe.locked", true);
				properties.set("client.gui.hud.hidden", false);
				properties.Save();
				File.Delete(text2);
				File.Copy("gfx.xml", text2);
				Configuration.Save();
				if (messagebox)
				{
					TopMostMessageBox.Show("Successfully replaced Properties & GFX file.\nPlease restart PokeMMO for the applied changes to take effect.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
				}
			}
			else if (messagebox)
			{
				TopMostMessageBox.Show("Could not replace Properties & GFX file.\nMake sure you have choosen the base directory of PokeMMO.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
			}
		}
		catch
		{
			if (messagebox)
			{
				TopMostMessageBox.Show("Could not replace Properties & GFX file.\nMake sure you have choosen the base directory of PokeMMO.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
			}
		}
	}
}
