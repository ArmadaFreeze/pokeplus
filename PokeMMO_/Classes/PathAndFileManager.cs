// Decompiled with JetBrains decompiler
// Type: PokeMMO_.Classes.PathAndFileManager
// Assembly: PokeMMO+, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F9DFFE97-DBAD-4EA1-90EA-586E112BF54C
// Assembly location: C:\Users\admin\Desktop\koi2-cleaned-cleaned_unpacked.exe

using PokeMMO_.Botting;
using PokeMMO_.Model;
using PokeMMO_.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

#nullable disable
namespace PokeMMO_.Classes;

public class PathAndFileManager
{
  public static void SelectDefaultPath()
  {
    try
    {
      using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
      {
        if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
          return;
        if (!folderBrowserDialog.SelectedPath.Contains("themes"))
        {
          MainViewModel.Instance.Settings.DefaultPath = folderBrowserDialog.SelectedPath;
        }
        else
        {
          int num = (int) System.Windows.MessageBox.Show("Make sure you have choosen the base directory of PokeMMO.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, System.Windows.MessageBoxOptions.DefaultDesktopOnly);
        }
      }
    }
    catch
    {
    }
  }

  public static void ReadPropertiesFile()
  {
    try
    {
      Bot.Instance.Settings.Data.Clear();
      foreach (string readAllLine in File.ReadAllLines(MainViewModel.Instance.Settings.DefaultPath + "\\config\\main.properties"))
        Bot.Instance.Settings.Data.Add(readAllLine.Split('=')[0], string.Join("=", ((IEnumerable<string>) readAllLine.Split('=')).Skip<string>(1).ToArray<string>()));
    }
    catch
    {
      int num = (int) System.Windows.MessageBox.Show("Make sure you have choosen the base directory of PokeMMO.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, System.Windows.MessageBoxOptions.DefaultDesktopOnly);
      Bot.Instance.Stop();
    }
  }

  public static void ReplacePropertiesAndGFXFile(bool messagebox)
  {
    try
    {
      if ((!File.Exists(MainViewModel.Instance.Settings.DefaultPath + "\\config\\main.properties") || !File.Exists(MainViewModel.Instance.Settings.DefaultPath + "\\data\\themes\\default\\gfx.xml") ? 0 : (File.Exists("gfx.xml") ? 1 : 0)) != 0)
      {
        Properties properties = new Properties(MainViewModel.Instance.Settings.DefaultPath + "\\config\\main.properties");
        if (Bot.Instance.Settings.ResolutionMode == ResolutionMode.HD)
        {
          properties.set("client.graphics.width", (object) 1920);
          properties.set("client.graphics.height", (object) 1080);
        }
        else
        {
          properties.set("client.graphics.width", (object) 1280 /*0x0500*/);
          properties.set("client.graphics.height", (object) 720);
        }
        properties.set("client.graphics.max_fpx", (object) 60);
        properties.set("client.gui.scale.guiscale", (object) 1.0);
        properties.set("client.graphics.gba.zoom_level", (object) -1);
        properties.set("client.graphics.battle.size", (object) 100);
        properties.set("client.graphics.graphics_api", (object) "OpenGL");
        properties.set("client.graphics.vsync", (object) false);
        properties.set("client.graphics.render.overworld_in_battle", (object) false);
        properties.set("client.graphics.render.battlebg", (object) false);
        properties.set("client.graphics.render_border", (object) false);
        properties.set("client.ui.theme", (object) "default");
        properties.set("client.ui.smallhud", (object) true);
        properties.set("client.gui.scale.hidpifont", (object) true);
        properties.set("client.ui.chat.transparency", (object) 100);
        properties.set("client.gui.hud.chatframe.x", (object) 0);
        properties.set("client.gui.hud.chatframe.y", (object) 0);
        properties.set("client.gui.hud.chatframe.width", (object) 411);
        properties.set("client.gui.hud.chatframe.height", (object) 176 /*0xB0*/);
        properties.set("client.gui.hud.chatframe.locked", (object) true);
        properties.set("client.gui.hud.hidden", (object) false);
        properties.Save();
        File.Delete(MainViewModel.Instance.Settings.DefaultPath + "\\data\\themes\\default\\gfx.xml");
        File.Copy("gfx.xml", MainViewModel.Instance.Settings.DefaultPath + "\\data\\themes\\default\\gfx.xml");
        Configuration.Save();
        if (!messagebox)
          return;
        int num = (int) System.Windows.MessageBox.Show("Successfully replaced Properties & GFX file.\nPlease restart PokeMMO for the applied changes to take effect.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, System.Windows.MessageBoxOptions.DefaultDesktopOnly);
      }
      else
      {
        if (!messagebox)
          return;
        int num = (int) System.Windows.MessageBox.Show("Could not replace Properties & GFX file.\nMake sure you have choosen the base directory of PokeMMO.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, System.Windows.MessageBoxOptions.DefaultDesktopOnly);
      }
    }
    catch
    {
      if (!messagebox)
        return;
      int num = (int) System.Windows.MessageBox.Show("Could not replace Properties & GFX file.\nMake sure you have choosen the base directory of PokeMMO.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK, System.Windows.MessageBoxOptions.DefaultDesktopOnly);
    }
  }
}
