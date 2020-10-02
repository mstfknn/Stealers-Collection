using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using LiveCharts.Wpf;

namespace LiveCharts.WinForms
{
	// Token: 0x02000003 RID: 3
	[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design")]
	[DesignerSerializer("System.ComponentModel.Design.Serialization.TypeCodeDomSerializer , System.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design")]
	public class AngularGauge : ElementHost
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002085 File Offset: 0x00000285
		public AngularGauge()
		{
			base.Child = this.WpfBase;
			base.HostContainer.MouseEnter += delegate(object sender, MouseEventArgs args)
			{
				base.Focus();
			};
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020BB File Offset: 0x000002BB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AngularGauge Base
		{
			get
			{
				return this.WpfBase;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020C3 File Offset: 0x000002C3
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020D0 File Offset: 0x000002D0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double Wedge
		{
			get
			{
				return this.WpfBase.Wedge;
			}
			set
			{
				this.WpfBase.Wedge = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020DE File Offset: 0x000002DE
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020EB File Offset: 0x000002EB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double TickStep
		{
			get
			{
				return this.WpfBase.TicksStep;
			}
			set
			{
				this.WpfBase.TicksStep = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020F9 File Offset: 0x000002F9
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002106 File Offset: 0x00000306
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double LabelsStep
		{
			get
			{
				return this.WpfBase.LabelsStep;
			}
			set
			{
				this.WpfBase.LabelsStep = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002114 File Offset: 0x00000314
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002121 File Offset: 0x00000321
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double FromValue
		{
			get
			{
				return this.WpfBase.FromValue;
			}
			set
			{
				this.WpfBase.FromValue = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000212F File Offset: 0x0000032F
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000213C File Offset: 0x0000033C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double ToValue
		{
			get
			{
				return this.WpfBase.ToValue;
			}
			set
			{
				this.WpfBase.ToValue = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000214A File Offset: 0x0000034A
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002157 File Offset: 0x00000357
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<AngularSection> Sections
		{
			get
			{
				return this.WpfBase.Sections;
			}
			set
			{
				this.WpfBase.Sections = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002165 File Offset: 0x00000365
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002172 File Offset: 0x00000372
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double Value
		{
			get
			{
				return this.WpfBase.Value;
			}
			set
			{
				this.WpfBase.Value = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002180 File Offset: 0x00000380
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000218D File Offset: 0x0000038D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Func<double, string> LabelFormatter
		{
			get
			{
				return this.WpfBase.LabelFormatter;
			}
			set
			{
				this.WpfBase.LabelFormatter = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000219B File Offset: 0x0000039B
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000021A8 File Offset: 0x000003A8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool DisableAnimations
		{
			get
			{
				return this.WpfBase.DisableaAnimations;
			}
			set
			{
				this.WpfBase.DisableaAnimations = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021B6 File Offset: 0x000003B6
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000021C3 File Offset: 0x000003C3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TimeSpan AnimationsSpeed
		{
			get
			{
				return this.WpfBase.AnimationsSpeed;
			}
			set
			{
				this.WpfBase.AnimationsSpeed = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021D1 File Offset: 0x000003D1
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000021DE File Offset: 0x000003DE
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Brush TicksForeground
		{
			get
			{
				return this.WpfBase.TicksForeground;
			}
			set
			{
				this.WpfBase.TicksForeground = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021EC File Offset: 0x000003EC
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000021F9 File Offset: 0x000003F9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double SectionsInnerRadius
		{
			get
			{
				return this.WpfBase.SectionsInnerRadius;
			}
			set
			{
				this.WpfBase.SectionsInnerRadius = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002207 File Offset: 0x00000407
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002214 File Offset: 0x00000414
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Brush NeedleFill
		{
			get
			{
				return this.WpfBase.NeedleFill;
			}
			set
			{
				this.WpfBase.NeedleFill = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002222 File Offset: 0x00000422
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000222F File Offset: 0x0000042F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Effect LabelsEffect
		{
			get
			{
				return this.WpfBase.LabelsEffect;
			}
			set
			{
				this.WpfBase.LabelsEffect = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000223D File Offset: 0x0000043D
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000224A File Offset: 0x0000044A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double TicksStrokeThickness
		{
			get
			{
				return this.WpfBase.TicksStrokeThickness;
			}
			set
			{
				this.WpfBase.TicksStrokeThickness = value;
			}
		}

		// Token: 0x04000004 RID: 4
		protected readonly AngularGauge WpfBase = new AngularGauge();
	}
}
