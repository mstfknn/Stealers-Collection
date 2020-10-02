using System;
using System.ComponentModel;
using System.Reactive.Concurrency;

namespace MysteryPanel.ViewModels
{
	// Token: 0x0200002F RID: 47
	public abstract class SchedulerResolver : INotifyPropertyChanged
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000341A File Offset: 0x0000161A
		public IScheduler Scheduler { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00003422 File Offset: 0x00001622
		public ControlScheduler OwnerControl
		{
			get
			{
				return (ControlScheduler)this.Scheduler;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600019E RID: 414 RVA: 0x00007E2C File Offset: 0x0000602C
		// (remove) Token: 0x0600019F RID: 415 RVA: 0x00007E64 File Offset: 0x00006064
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060001A0 RID: 416 RVA: 0x0000342F File Offset: 0x0000162F
		protected SchedulerResolver(IScheduler scheduler)
		{
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			this.Scheduler = scheduler;
		}

		// Token: 0x060001A1 RID: 417
		public abstract void Load();

		// Token: 0x060001A2 RID: 418 RVA: 0x00007E9C File Offset: 0x0000609C
		protected void <>OnPropertyChanged(PropertyChangedEventArgs eventArgs)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, eventArgs);
			}
		}
	}
}
