#region

using System;
using System.Collections.Generic;

#endregion

namespace KoiVM.CFG
{
    public class BasicBlock<TContent> : IBasicBlock
    {
        public BasicBlock(int id, TContent content)
        {
            this.Id = id;
            this.Content = content;
            this.Sources = new List<BasicBlock<TContent>>();
            this.Targets = new List<BasicBlock<TContent>>();
        }

        public TContent Content
        {
            get;
            set;
        }

        public IList<BasicBlock<TContent>> Sources
        {
            get;
        }

        public IList<BasicBlock<TContent>> Targets
        {
            get;
        }

        public int Id
        {
            get;
            set;
        }

        public BlockFlags Flags
        {
            get;
            set;
        }

        object IBasicBlock.Content => this.Content;

        IEnumerable<IBasicBlock> IBasicBlock.Sources => this.Sources;

        IEnumerable<IBasicBlock> IBasicBlock.Targets => this.Targets;

        public void LinkTo(BasicBlock<TContent> target)
        {
            this.Targets.Add(target);
            target.Sources.Add(this);
        }

        public override string ToString() => $"Block_{this.Id:x2}:{Environment.NewLine}{this.Content}";
    }
}