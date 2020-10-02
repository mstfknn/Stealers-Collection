using System;
using System.Reflection;
using System.Windows.Forms;

namespace MysteryPanel.ViewModels
{
	// Token: 0x0200000B RID: 11
	public class FolderSelectDialog
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000029B5 File Offset: 0x00000BB5
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000029D0 File Offset: 0x00000BD0
		public string InitialDirectory
		{
			get
			{
				if (!string.IsNullOrEmpty(this._initialDirectory))
				{
					return this._initialDirectory;
				}
				return Environment.CurrentDirectory;
			}
			set
			{
				this._initialDirectory = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000029D9 File Offset: 0x00000BD9
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000029EA File Offset: 0x00000BEA
		public string Title
		{
			get
			{
				return this._title ?? "Select a folder";
			}
			set
			{
				this._title = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000029F3 File Offset: 0x00000BF3
		public string FileName
		{
			get
			{
				return this._fileName;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000029FB File Offset: 0x00000BFB
		public bool Show()
		{
			return this.Show(IntPtr.Zero);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000040D4 File Offset: 0x000022D4
		public bool Show(IntPtr hWndOwner)
		{
			FolderSelectDialog.ShowDialogResult showDialogResult = (Environment.OSVersion.Version.Major >= 6) ? FolderSelectDialog.VistaDialog.Show(hWndOwner, this.InitialDirectory, this.Title) : FolderSelectDialog.ShowXpDialog(hWndOwner, this.InitialDirectory, this.Title);
			this._fileName = showDialogResult.FileName;
			return showDialogResult.Result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004130 File Offset: 0x00002330
		private static FolderSelectDialog.ShowDialogResult ShowXpDialog(IntPtr ownerHandle, string initialDirectory, string title)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
			{
				Description = title,
				SelectedPath = initialDirectory,
				ShowNewFolderButton = false
			};
			FolderSelectDialog.ShowDialogResult result = default(FolderSelectDialog.ShowDialogResult);
			if (folderBrowserDialog.ShowDialog(new FolderSelectDialog.WindowWrapper(ownerHandle)) == DialogResult.OK)
			{
				result.Result = true;
				result.FileName = folderBrowserDialog.SelectedPath;
			}
			return result;
		}

		// Token: 0x0400001C RID: 28
		private string _initialDirectory;

		// Token: 0x0400001D RID: 29
		private string _title;

		// Token: 0x0400001E RID: 30
		private string _fileName = "";

		// Token: 0x0200000C RID: 12
		private struct ShowDialogResult
		{
			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002A1B File Offset: 0x00000C1B
			// (set) Token: 0x060000C3 RID: 195 RVA: 0x00002A23 File Offset: 0x00000C23
			public bool Result { get; set; }

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002A2C File Offset: 0x00000C2C
			// (set) Token: 0x060000C5 RID: 197 RVA: 0x00002A34 File Offset: 0x00000C34
			public string FileName { get; set; }
		}

		// Token: 0x0200000D RID: 13
		private static class VistaDialog
		{
			// Token: 0x060000C6 RID: 198 RVA: 0x00004188 File Offset: 0x00002388
			public static FolderSelectDialog.ShowDialogResult Show(IntPtr ownerHandle, string initialDirectory, string title)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog
				{
					AddExtension = false,
					CheckFileExists = false,
					DereferenceLinks = true,
					Filter = "Folders|\n",
					InitialDirectory = initialDirectory,
					Multiselect = false,
					Title = title
				};
				object obj = FolderSelectDialog.VistaDialog.s_createVistaDialogMethodInfo.Invoke(openFileDialog, new object[0]);
				FolderSelectDialog.VistaDialog.s_onBeforeVistaDialogMethodInfo.Invoke(openFileDialog, new object[]
				{
					obj
				});
				FolderSelectDialog.VistaDialog.s_setOptionsMethodInfo.Invoke(obj, new object[]
				{
					(uint)FolderSelectDialog.VistaDialog.s_getOptionsMethodInfo.Invoke(openFileDialog, new object[0]) | FolderSelectDialog.VistaDialog.s_fosPickFoldersBitFlag
				});
				object[] array = new object[]
				{
					FolderSelectDialog.VistaDialog.s_vistaDialogEventsConstructorInfo.Invoke(new object[]
					{
						openFileDialog
					}),
					0u
				};
				FolderSelectDialog.VistaDialog.s_adviseMethodInfo.Invoke(obj, array);
				FolderSelectDialog.ShowDialogResult showDialogResult;
				try
				{
					int num = (int)FolderSelectDialog.VistaDialog.s_showMethodInfo.Invoke(obj, new object[]
					{
						ownerHandle
					});
					showDialogResult = default(FolderSelectDialog.ShowDialogResult);
					showDialogResult.Result = (num == 0);
					showDialogResult.FileName = openFileDialog.FileName;
					showDialogResult = showDialogResult;
				}
				finally
				{
					FolderSelectDialog.VistaDialog.s_unAdviseMethodInfo.Invoke(obj, new object[]
					{
						array[1]
					});
				}
				return showDialogResult;
			}

			// Token: 0x04000021 RID: 33
			private const string c_foldersFilter = "Folders|\n";

			// Token: 0x04000022 RID: 34
			private const BindingFlags c_flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

			// Token: 0x04000023 RID: 35
			private static readonly Assembly s_windowsFormsAssembly = typeof(FileDialog).Assembly;

			// Token: 0x04000024 RID: 36
			private static readonly Type s_iFileDialogType = FolderSelectDialog.VistaDialog.s_windowsFormsAssembly.GetType("System.Windows.Forms.FileDialogNative+IFileDialog");

			// Token: 0x04000025 RID: 37
			private static readonly MethodInfo s_createVistaDialogMethodInfo = typeof(OpenFileDialog).GetMethod("CreateVistaDialog", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			// Token: 0x04000026 RID: 38
			private static readonly MethodInfo s_onBeforeVistaDialogMethodInfo = typeof(OpenFileDialog).GetMethod("OnBeforeVistaDialog", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			// Token: 0x04000027 RID: 39
			private static readonly MethodInfo s_getOptionsMethodInfo = typeof(FileDialog).GetMethod("GetOptions", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			// Token: 0x04000028 RID: 40
			private static readonly MethodInfo s_setOptionsMethodInfo = FolderSelectDialog.VistaDialog.s_iFileDialogType.GetMethod("SetOptions", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			// Token: 0x04000029 RID: 41
			private static readonly uint s_fosPickFoldersBitFlag = (uint)FolderSelectDialog.VistaDialog.s_windowsFormsAssembly.GetType("System.Windows.Forms.FileDialogNative+FOS").GetField("FOS_PICKFOLDERS").GetValue(null);

			// Token: 0x0400002A RID: 42
			private static readonly ConstructorInfo s_vistaDialogEventsConstructorInfo = FolderSelectDialog.VistaDialog.s_windowsFormsAssembly.GetType("System.Windows.Forms.FileDialog+VistaDialogEvents").GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
			{
				typeof(FileDialog)
			}, null);

			// Token: 0x0400002B RID: 43
			private static readonly MethodInfo s_adviseMethodInfo = FolderSelectDialog.VistaDialog.s_iFileDialogType.GetMethod("Advise");

			// Token: 0x0400002C RID: 44
			private static readonly MethodInfo s_unAdviseMethodInfo = FolderSelectDialog.VistaDialog.s_iFileDialogType.GetMethod("Unadvise");

			// Token: 0x0400002D RID: 45
			private static readonly MethodInfo s_showMethodInfo = FolderSelectDialog.VistaDialog.s_iFileDialogType.GetMethod("Show");
		}

		// Token: 0x0200000E RID: 14
		private class WindowWrapper : IWin32Window
		{
			// Token: 0x060000C8 RID: 200 RVA: 0x00002A3D File Offset: 0x00000C3D
			public WindowWrapper(IntPtr handle)
			{
				this._handle = handle;
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002A4C File Offset: 0x00000C4C
			public IntPtr Handle
			{
				get
				{
					return this._handle;
				}
			}

			// Token: 0x0400002E RID: 46
			private readonly IntPtr _handle;
		}
	}
}
