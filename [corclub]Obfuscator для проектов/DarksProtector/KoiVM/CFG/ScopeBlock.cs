#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dnlib.DotNet.Emit;

#endregion

namespace KoiVM.CFG
{
    public class ScopeBlock
    {
        public ScopeBlock()
        {
            this.Type = ScopeType.None;
            this.ExceptionHandler = null;
            this.Children = new List<ScopeBlock>();
            this.Content = new List<IBasicBlock>();
        }

        public ScopeBlock(ScopeType type, ExceptionHandler eh)
        {
            if(type == ScopeType.None)
                throw new ArgumentException("type");
            this.Type = type;
            this.ExceptionHandler = eh;
            this.Children = new List<ScopeBlock>();
            this.Content = new List<IBasicBlock>();
        }

        public ScopeType Type
        {
            get;
        }

        public ExceptionHandler ExceptionHandler
        {
            get;
        }

        public IList<ScopeBlock> Children
        {
            get;
        }

        public IList<IBasicBlock> Content
        {
            get;
        }

        public IEnumerable<IBasicBlock> GetBasicBlocks()
        {
            this.Validate();
            return this.Content.Count > 0 ? this.Content : this.Children.SelectMany(child => child.GetBasicBlocks());
        }

        public Dictionary<BasicBlock<TOld>, BasicBlock<TNew>> UpdateBasicBlocks<TOld, TNew>(
            Func<BasicBlock<TOld>, TNew> updateFunc) => this.UpdateBasicBlocks(updateFunc, (id, content) => new BasicBlock<TNew>(id, content));

        public Dictionary<BasicBlock<TOld>, BasicBlock<TNew>> UpdateBasicBlocks<TOld, TNew>(
            Func<BasicBlock<TOld>, TNew> updateFunc,
            Func<int, TNew, BasicBlock<TNew>> factoryFunc)
        {
            var blockMap = new Dictionary<BasicBlock<TOld>, BasicBlock<TNew>>();
            this.UpdateBasicBlocksInternal(updateFunc, blockMap, factoryFunc);
            foreach(KeyValuePair<BasicBlock<TOld>, BasicBlock<TNew>> blockPair in blockMap)
            {
                foreach(BasicBlock<TOld> src in blockPair.Key.Sources)
                    blockPair.Value.Sources.Add(blockMap[src]);
                foreach(BasicBlock<TOld> dst in blockPair.Key.Targets)
                    blockPair.Value.Targets.Add(blockMap[dst]);
            }
            return blockMap;
        }

        private void UpdateBasicBlocksInternal<TOld, TNew>(Func<BasicBlock<TOld>, TNew> updateFunc,
            Dictionary<BasicBlock<TOld>, BasicBlock<TNew>> blockMap,
            Func<int, TNew, BasicBlock<TNew>> factoryFunc)
        {
            this.Validate();
            if(this.Content.Count > 0)
                for(int i = 0; i < this.Content.Count; i++)
                {
                    var oldBlock = (BasicBlock<TOld>)this.Content[i];
                    TNew newContent = updateFunc(oldBlock);
                    BasicBlock<TNew> newBlock = factoryFunc(oldBlock.Id, newContent);
                    newBlock.Flags = oldBlock.Flags;
                    this.Content[i] = newBlock;
                    blockMap[oldBlock] = newBlock;
                }
            else
                foreach(ScopeBlock child in this.Children)
                    child.UpdateBasicBlocksInternal(updateFunc, blockMap, factoryFunc);
        }

        public void ProcessBasicBlocks<T>(Action<BasicBlock<T>> processFunc)
        {
            this.Validate();
            if(this.Content.Count > 0)
                foreach(IBasicBlock child in this.Content)
                    processFunc((BasicBlock<T>) child);
            else
                foreach(ScopeBlock child in this.Children)
                    child.ProcessBasicBlocks(processFunc);
        }

        public void Validate()
        {
            if(this.Children.Count != 0 && this.Content.Count != 0)
                throw new InvalidOperationException("Children and Content cannot be set at the same time.");
        }

        public ScopeBlock[] SearchBlock(IBasicBlock target)
        {
            var scopeStack = new Stack<ScopeBlock>();
            SearchBlockInternal(this, target, scopeStack);
            return scopeStack.Reverse().ToArray();
        }

        private static bool SearchBlockInternal(ScopeBlock scope, IBasicBlock target, Stack<ScopeBlock> scopeStack)
        {
            if(scope.Content.Count > 0)
            {
                if(scope.Content.Contains(target))
                {
                    scopeStack.Push(scope);
                    return true;
                }
                return false;
            }
            scopeStack.Push(scope);
            foreach(ScopeBlock child in scope.Children)
                if(SearchBlockInternal(child, target, scopeStack))
                    return true;
            scopeStack.Pop();
            return false;
        }


        private static string ToString(ExceptionHandler eh) => $"{eh.GetHashCode():x8}:{eh.HandlerType}";

        public override string ToString()
        {
            var ret = new StringBuilder();
            switch (this.Type)
            {
                case ScopeType.Try:
                    ret.AppendLine($"try @ {ToString(this.ExceptionHandler)} {{");
                    break;
                case ScopeType.Handler:
                    ret.AppendLine($"handler @ {ToString(this.ExceptionHandler)} {{");
                    break;
                case ScopeType.Filter:
                    ret.AppendLine($"filter @ {ToString(this.ExceptionHandler)} {{");
                    break;
                default:
                    ret.AppendLine("{");
                    break;
            }
            if (this.Children.Count <= 0)
            {
                foreach (IBasicBlock child in this.Content)
                    ret.AppendLine(child.ToString());
            }
            else
                foreach (ScopeBlock child in this.Children)
                    ret.AppendLine(child.ToString());
            ret.AppendLine("}");
            return ret.ToString();
        }
    }
}