using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using LiveCharts.Wpf;

namespace LiveCharts.WinForms
{
	// Token: 0x02000004 RID: 4
	[DesignerSerializer("System.ComponentModel.Design.Serialization.TypeCodeDomSerializer , System.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design")]
	[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design")]
	public class SolidGauge : ElementHost
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002261 File Offset: 0x00000461
		public SolidGauge()
		{
			base.Child = this.WpfBase;
			base.HostContainer.MouseEnter += delegate(object sender, MouseEventArgs args)
			{
				base.Focus();
			};
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002297 File Offset: 0x00000497
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Gauge Base
		{
			get
			{
				return this.WpfBase;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000229F File Offset: 0x0000049F
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000022AC File Offset: 0x000004AC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Uses360Mode
		{
			get
			{
				return this.WpfBase.Uses360Mode;
			}
			set
			{
				this.WpfBase.Uses360Mode = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000022BA File Offset: 0x000004BA
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000022C7 File Offset: 0x000004C7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool DisableAnimations
		{
			get
			{
				return this.WpfBase.DisableAnimations;
			}
			set
			{
				this.WpfBase.DisableAnimations = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000022D5 File Offset: 0x000004D5
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000022E2 File Offset: 0x000004E2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double From
		{
			get
			{
				return this.WpfBase.From;
			}
			set
			{
				this.WpfBase.From = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000022F0 File Offset: 0x000004F0
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000022FD File Offset: 0x000004FD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double To
		{
			get
			{
				return this.WpfBase.To;
			}
			set
			{
				this.WpfBase.To = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000230B File Offset: 0x0000050B
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002318 File Offset: 0x00000518
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002326 File Offset: 0x00000526
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002333 File Offset: 0x00000533
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double? InnerRadius
		{
			get
			{
				return this.WpfBase.InnerRadius;
			}
			set
			{
				this.WpfBase.InnerRadius = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002341 File Offset: 0x00000541
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000234E File Offset: 0x0000054E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Brush Stroke
		{
			get
			{
				return this.WpfBase.Stroke;
			}
			set
			{
				this.WpfBase.Stroke = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000235C File Offset: 0x0000055C
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002369 File Offset: 0x00000569
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double StrokeThickness
		{
			get
			{
				return this.WpfBase.StrokeThickness;
			}
			set
			{
				this.WpfBase.StrokeThickness = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002377 File Offset: 0x00000577
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002384 File Offset: 0x00000584
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color ToColor
		{
			get
			{
				return this.WpfBase.ToColor;
			}
			set
			{
				this.WpfBase.ToColor = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002392 File Offset: 0x00000592
		// (set) Token: 0x0600003E RID: 62 RVA: 0x0000239F File Offset: 0x0000059F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color FromColor
		{
			get
			{
				return this.WpfBase.FromColor;
			}
			set
			{
				this.WpfBase.FromColor = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000023AD File Offset: 0x000005AD
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000023BA File Offset: 0x000005BA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Brush GaugeBackground
		{
			get
			{
				return this.WpfBase.GaugeBackground;
			}
			set
			{
				this.WpfBase.GaugeBackground = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000023C8 File Offset: 0x000005C8
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000023D5 File Offset: 0x000005D5
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000023E3 File Offset: 0x000005E3
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000023F0 File Offset: 0x000005F0
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000023FE File Offset: 0x000005FE
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000240B File Offset: 0x0000060B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double? HighFontSize
		{
			get
			{
				return this.WpfBase.HighFontSize;
			}
			set
			{
				this.WpfBase.HighFontSize = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002419 File Offset: 0x00000619
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002426 File Offset: 0x00000626
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double FontSize
		{
			get
			{
				return this.WpfBase.FontSize;
			}
			set
			{
				this.WpfBase.FontSize = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002434 File Offset: 0x00000634
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002441 File Offset: 0x00000641
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public FontFamily FontFamily
		{
			get
			{
				return this.WpfBase.FontFamily;
			}
			set
			{
				this.WpfBase.FontFamily = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000244F File Offset: 0x0000064F
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000245C File Offset: 0x0000065C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public FontWeight FontWeight
		{
			get
			{
				return this.WpfBase.FontWeight;
			}
			set
			{
				this.WpfBase.FontWeight = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000246A File Offset: 0x0000066A
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002477 File Offset: 0x00000677
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public FontStyle FontStyle
		{
			get
			{
				return this.WpfBase.FontStyle;
			}
			set
			{
				this.WpfBase.FontStyle = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002485 File Offset: 0x00000685
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002492 File Offset: 0x00000692
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public FontStretch FontStretch
		{
			get
			{
				return this.WpfBase.FontStretch;
			}
			set
			{
				this.WpfBase.FontStretch = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000024A0 File Offset: 0x000006A0
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000024AD File Offset: 0x000006AD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Brush ForeGround
		{
			get
			{
				return this.WpfBase.Foreground;
			}
			set
			{
				this.WpfBase.Foreground = value;
			}
		}

		// Token: 0x04000005 RID: 5
		protected readonly Gauge WpfBase = new Gauge();
	}
}
