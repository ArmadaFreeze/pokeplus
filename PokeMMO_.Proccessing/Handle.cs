using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using PokeMMO_.Botting;
using PokeMMO_.Classes;

namespace PokeMMO_.Proccessing;

public class Handle
{
	public IntPtr GameHandle => GetGameHandle();

	public Process GameProcess => GetGameProcess();

	public bool IsCERunning => CERunningCheck();

	private bool CERunningCheck()
	{
		try
		{
			return CEDetection.FindCEExe(CEDetection.GetProcessesWithWindow());
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log("CERunningCheck error: " + ex.Message);
			return false;
		}
	}

	public Process GetGameProcess()
	{
		try
		{
			return Process.GetProcesses().FirstOrDefault((Process x) => x.ProcessName.StartsWith("java", StringComparison.OrdinalIgnoreCase));
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log("GetGameProcess error: " + ex.Message);
			return null;
		}
	}

	public IntPtr GetGameHandle()
	{
		try
		{
			if (Bot.Instance.RequestStop)
			{
				return IntPtr.Zero;
			}
			return GameProcess.MainWindowHandle;
		}
		catch (Exception ex)
		{
			PokeMMOLogger.Instance.Log("GetGameHandle error: " + ex.Message);
			Bot.Instance.Stop();
			TopMostMessageBox.Show("Please start PokeMMO first.", "Bot stopped", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.OK);
			return IntPtr.Zero;
		}
	}
}
