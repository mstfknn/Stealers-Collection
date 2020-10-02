using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using LiveCharts.Maps;
using LiveCharts.Wpf;

namespace LiveCharts.WinForms
{
	// Token: 0x02000006 RID: 6
	[DesignerSerializer("System.ComponentModel.Design.Serialization.TypeCodeDomSerializer , System.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design")]
	[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design")]
	public class GeoMap : ElementHost
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public GeoMap()
		{
			try
			{
				this.WpfBase = new GeoMap();
				base.Child = this.WpfBase;
				base.HostContainer.MouseEnter += delegate(object sender, MouseEventArgs args)
				{
					base.Focus();
				};
			}
			catch (Exception ex)
			{
				File.AppendAllText("logs.txt", ex.ToString());
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000078 RID: 120 RVA: 0x00002653 File Offset: 0x00000853
		// (remove) Token: 0x06000079 RID: 121 RVA: 0x00002661 File Offset: 0x00000861
		public event EventHandler<MapData> OnLeftMouseLandClick
		{
			add
			{
				this.WpfBase.OnLeftMouseLandClick += value;
			}
			remove
			{
				this.WpfBase.OnLeftMouseLandClick -= value;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600007A RID: 122 RVA: 0x0000266F File Offset: 0x0000086F
		// (remove) Token: 0x0600007B RID: 123 RVA: 0x0000267D File Offset: 0x0000087D
		public event EventHandler<MapData> OnRightMouseLandClick
		{
			add
			{
				this.WpfBase.OnRightMouseLandClick += value;
			}
			remove
			{
				this.WpfBase.OnRightMouseLandClick -= value;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600007C RID: 124 RVA: 0x00003B18 File Offset: 0x00001D18
		// (remove) Token: 0x0600007D RID: 125 RVA: 0x00003B50 File Offset: 0x00001D50
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000268B File Offset: 0x0000088B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GeoMap Base
		{
			get
			{
				return this.WpfBase;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002693 File Offset: 0x00000893
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000026A0 File Offset: 0x000008A0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Dictionary<string, string> LanguagePack
		{
			get
			{
				return this.WpfBase.LanguagePack;
			}
			set
			{
				this.WpfBase.LanguagePack = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000026AE File Offset: 0x000008AE
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000026BB File Offset: 0x000008BB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Brush DefaultLandFill
		{
			get
			{
				return this.WpfBase.DefaultLandFill;
			}
			set
			{
				this.WpfBase.DefaultLandFill = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000026C9 File Offset: 0x000008C9
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000026D6 File Offset: 0x000008D6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public double LandStrokeThickness
		{
			get
			{
				return this.WpfBase.LandStrokeThickness;
			}
			set
			{
				this.WpfBase.LandStrokeThickness = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000026E4 File Offset: 0x000008E4
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000026F1 File Offset: 0x000008F1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Brush LandStroke
		{
			get
			{
				return this.WpfBase.LandStroke;
			}
			set
			{
				this.WpfBase.LandStroke = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000026FF File Offset: 0x000008FF
		// (set) Token: 0x06000088 RID: 136 RVA: 0x0000270C File Offset: 0x0000090C
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

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000271A File Offset: 0x0000091A
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002727 File Offset: 0x00000927
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002735 File Offset: 0x00000935
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002742 File Offset: 0x00000942
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Hoverable
		{
			get
			{
				return this.WpfBase.Hoverable;
			}
			set
			{
				this.WpfBase.Hoverable = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002750 File Offset: 0x00000950
		// (set) Token: 0x0600008E RID: 142 RVA: 0x0000275D File Offset: 0x0000095D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Dictionary<string, double> HeatMap
		{
			get
			{
				return this.WpfBase.HeatMap;
			}
			set
			{
				this.WpfBase.HeatMap = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000276B File Offset: 0x0000096B
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002778 File Offset: 0x00000978
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GradientStopCollection GradientStopCollection
		{
			get
			{
				return this.WpfBase.GradientStopCollection;
			}
			set
			{
				this.WpfBase.GradientStopCollection = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002786 File Offset: 0x00000986
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002793 File Offset: 0x00000993
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Source
		{
			get
			{
				return this.WpfBase.Source;
			}
			set
			{
				this.WpfBase.Source = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000027A1 File Offset: 0x000009A1
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000027AE File Offset: 0x000009AE
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool EnableZoomingAndPanning
		{
			get
			{
				return this.WpfBase.EnableZoomingAndPanning;
			}
			set
			{
				this.WpfBase.EnableZoomingAndPanning = value;
			}
		}

		// Token: 0x04000007 RID: 7
		public readonly GeoMap WpfBase;
	}
}
