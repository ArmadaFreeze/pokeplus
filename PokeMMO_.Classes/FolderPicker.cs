using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace PokeMMO_.Classes;

internal static class FolderPicker
{
	[ComImport]
	[Guid("DC1C5A9C-E88A-4DDE-A5A1-60F82A20AEF7")]
	private class FileOpenDialog
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern FileOpenDialog();
	}

	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("42F85136-DB7E-439C-85F1-E4075D135FC8")]
	private interface IFileOpenDialog
	{
		[PreserveSig]
		int Show(IntPtr hwndOwner);

		void SetFileTypes(uint cFileTypes, IntPtr rgFilterSpec);

		void SetFileTypeIndex(uint iFileType);

		void GetFileTypeIndex(out uint piFileType);

		void Advise(IntPtr pfde, out uint pdwCookie);

		void Unadvise(uint dwCookie);

		void SetOptions(uint fos);

		void GetOptions(out uint pfos);

		void SetDefaultFolder(IShellItem psi);

		void SetFolder(IShellItem psi);

		void GetFolder(out IShellItem ppsi);

		void GetCurrentSelection(out IShellItem ppsi);

		void SetFileName([MarshalAs(UnmanagedType.LPWStr)] string pszName);

		void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

		void SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pszTitle);

		void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string pszText);

		void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

		void GetResult(out IShellItem ppsi);

		void AddPlace(IShellItem psi, int fdap);

		void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

		void Close(int hr);

		void SetClientGuid(ref Guid guid);

		void ClearClientData();

		void SetFilter(IntPtr pFilter);

		void GetResults(out IntPtr ppenum);

		void GetSelectedItems(out IntPtr ppsai);
	}

	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
	private interface IShellItem
	{
		void BindToHandler(IntPtr pbc, ref Guid bhid, ref Guid riid, out IntPtr ppv);

		void GetParent(out IShellItem ppsi);

		void GetDisplayName(uint sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

		void GetAttributes(uint sfgaoMask, out uint psfgaoAttribs);

		void Compare(IShellItem psi, uint hint, out int piOrder);
	}

	private const uint FOS_PICKFOLDERS = 32u;

	private const uint FOS_FORCEFILESYSTEM = 64u;

	private const uint SIGDN_FILESYSPATH = 2147844096u;

	public static string ShowDialog(Window owner = null, string title = null, string initialPath = null)
	{
		IFileOpenDialog fileOpenDialog = (IFileOpenDialog)new FileOpenDialog();
		try
		{
			fileOpenDialog.SetOptions(96u);
			if (title != null)
			{
				fileOpenDialog.SetTitle(title);
			}
			if (!string.IsNullOrEmpty(initialPath) && Directory.Exists(initialPath))
			{
				SHCreateItemFromParsingName(initialPath, IntPtr.Zero, typeof(IShellItem).GUID, out var ppv);
				if (ppv != null)
				{
					fileOpenDialog.SetFolder(ppv);
				}
			}
			IntPtr hwndOwner = ((owner != null) ? new WindowInteropHelper(owner).Handle : IntPtr.Zero);
			if (fileOpenDialog.Show(hwndOwner) != 0)
			{
				return null;
			}
			fileOpenDialog.GetResult(out var ppsi);
			ppsi.GetDisplayName(2147844096u, out var ppszName);
			return ppszName;
		}
		finally
		{
			Marshal.ReleaseComObject(fileOpenDialog);
		}
	}

	[DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
	private static extern void SHCreateItemFromParsingName(string pszPath, IntPtr pbc, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IShellItem ppv);
}
