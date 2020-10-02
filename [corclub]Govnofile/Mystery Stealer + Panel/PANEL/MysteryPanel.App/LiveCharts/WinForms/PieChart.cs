using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using LiveCharts.Events;
using LiveCharts.Wpf;

namespace LiveCharts.WinForms
{
	// Token: 0x02000005 RID: 5
	[DesignerSerializer("System.ComponentModel.Design.Serialization.TypeCodeDomSerializer , System.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design")]
	[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design")]
	public class PieChart : ElementHost
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00003A50 File Offset: 0x00001C50
		public PieChart()
		{
			base.Child = this.WpfBase;
			base.HostContainer.MouseEnter += delegate(object sender, MouseEventArgs args)
			{
				base.Focus();
			};
			if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
			{
				this.WpfBase.Series = this.WpfBase.GetDesignerModeCollection();
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000055 RID: 85 RVA: 0x000024BB File Offset: 0x000006BB
		// (remove) Token: 0x06000056 RID: 86 RVA: 0x000024BB File Offset: 0x000006BB
		public event DataClickHandler DataClick
		{
			add
			{
				this.WpfBase.DataClick += value;
			}
			remove
			{
				this.WpfBase.DataClick += value;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000057 RID: 87 RVA: 0x000024C9 File Offset: 0x000006C9
		// (remove) Token: 0x06000058 RID: 88 RVA: 0x000024C9 File Offset: 0x000006C9
		public event DataHoverHandler DataHover
		{
			add
			{
				this.WpfBase.DataHover += value;
			}
			remove
			{
				this.WpfBase.DataHover += value;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000059 RID: 89 RVA: 0x000024D7 File Offset: 0x000006D7
		// (remove) Token: 0x0600005A RID: 90 RVA: 0x000024D7 File Offset: 0x000006D7
		public event UpdaterTickHandler UpdaterTick
		{
			add
			{
				this.WpfBase.UpdaterTick += value;
			}
			remove
			{
				this.WpfBase.UpdaterTick += value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000024E5 File Offset: 0x000006E5
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000024F2 File Offset: 0x000006F2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AxesCollection AxisY
		{
			get
			{
				return this.WpfBase.AxisY;
			}
			set
			{
				this.WpfBase.AxisY = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002500 File Offset: 0x00000700
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000250D File Offset: 0x0000070D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AxesCollection AxisX
		{
			get
			{
				return this.WpfBase.AxisX;
			}
			set
			{
				this.WpfBase.AxisX = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000251B File Offset: 0x0000071B
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002528 File Offset: 0x00000728
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public UserControl DefaultLegend
		{
			get
			{
				return this.WpfBase.ChartLegend;
			}
			set
			{
				this.WpfBase.ChartLegend = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002536 File Offset: 0x00000736
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002543 File Offset: 0x00000743
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ZoomingOptions Zoom
		{
			get
			{
				return this.WpfBase.Zoom;
			}
			set
			{
				this.WpfBase.Zoom = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002551 File Offset: 0x00000751
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000255E File Offset: 0x0000075E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LegendLocation LegendLocation
		{
			get
			{
				return this.WpfBase.LegendLocation;
			}
			set
			{
				this.WpfBase.LegendLocation = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000256C File Offset: 0x0000076C
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002579 File Offset: 0x00000779
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SeriesCollection Series
		{
			get
			{
				return this.WpfBase.Series;
			}
			set
			{
				this.WpfBase.Series = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002587 File Offset: 0x00000787
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002594 File Offset: 0x00000794
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

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000025A2 File Offset: 0x000007A2
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000025AF File Offset: 0x000007AF
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000025BD File Offset: 0x000007BD
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000025CA File Offset: 0x000007CA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public UserControl DataTooltip
		{
			get
			{
				return this.WpfBase.DataTooltip;
			}
			set
			{
				this.WpfBase.DataTooltip = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000025D8 File Offset: 0x000007D8
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000025E5 File Offset: 0x000007E5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double InnerRadius
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000025F3 File Offset: 0x000007F3
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002600 File Offset: 0x00000800
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double StartingRotationAngle
		{
			get
			{
				return this.WpfBase.StartingRotationAngle;
			}
			set
			{
				this.WpfBase.StartingRotationAngle = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000260E File Offset: 0x0000080E
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000261B File Offset: 0x0000081B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public UpdaterState UpdaterState
		{
			get
			{
				return this.WpfBase.UpdaterState;
			}
			set
			{
				this.WpfBase.UpdaterState = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002629 File Offset: 0x00000829
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002636 File Offset: 0x00000836
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double HoverPushOut
		{
			get
			{
				return this.WpfBase.HoverPushOut;
			}
			set
			{
				this.WpfBase.HoverPushOut = value;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002644 File Offset: 0x00000844
		public void Update(bool restartView, bool force)
		{
			this.WpfBase.Update(restartView, force);
		}

		// Token: 0x04000006 RID: 6
		protected readonly PieChart WpfBase = new PieChart();
	}
}
