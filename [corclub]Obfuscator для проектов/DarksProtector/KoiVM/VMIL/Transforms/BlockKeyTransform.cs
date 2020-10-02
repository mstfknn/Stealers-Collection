#region

using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet.Emit;
using KoiVM.AST.IL;
using KoiVM.CFG;
using KoiVM.RT;
using KoiVM.VM;

#endregion

namespace KoiVM.VMIL.Transforms
{
    public class BlockKeyTransform : IPostTransform
    {
        private Dictionary<ILBlock, BlockKey> Keys;
        private DarksVMMethodInfo methodInfo;

        private DarksVMRuntime runtime;

        public void Initialize(ILPostTransformer tr)
        {
            this.runtime = tr.Runtime;
            this.methodInfo = tr.Runtime.Descriptor.Data.LookupInfo(tr.Method);
            this.ComputeBlockKeys(tr.RootScope);
        }

        public void Transform(ILPostTransformer tr)
        {
            BlockKey key = this.Keys[tr.Block];
            this.methodInfo.BlockKeys[tr.Block] = new VMBlockKey
            {
                EntryKey = (byte) key.Entry,
                ExitKey = (byte) key.Exit
            };
        }

        private void ComputeBlockKeys(ScopeBlock rootScope)
        {
            var blocks = rootScope.GetBasicBlocks().OfType<ILBlock>().ToList();
            uint id = 1;
            this.Keys = blocks.ToDictionary(
                block => block,
                block => new BlockKey {Entry = id++, Exit = id++});
            EHMap ehMap = this.MapEHs(rootScope);

            bool updated;
            do
            {
                updated = false;

                BlockKey key;

                key = this.Keys[blocks[0]];
                key.Entry = 0xfffffffe;
                this.Keys[blocks[0]] = key;

                key = this.Keys[blocks[blocks.Count - 1]];
                key.Exit = 0xfffffffd;
                this.Keys[blocks[blocks.Count - 1]] = key;

                // Update the state ids with the maximum id
                foreach(ILBlock block in blocks)
                {
                    key = this.Keys[block];
                    if(block.Sources.Count > 0)
                    {
                        uint newEntry = block.Sources.Select(b => this.Keys[(ILBlock) b].Exit).Max();
                        if(key.Entry != newEntry)
                        {
                            key.Entry = newEntry;
                            updated = true;
                        }
                    }
                    if(block.Targets.Count > 0)
                    {
                        uint newExit = block.Targets.Select(b => this.Keys[(ILBlock) b].Entry).Max();
                        if(key.Exit != newExit)
                        {
                            key.Exit = newExit;
                            updated = true;
                        }
                    }
                    this.Keys[block] = key;
                }

                // Match finally enter = finally exit = try end exit
                // Match filter start = 0xffffffff
                this.MatchHandlers(ehMap, ref updated);
            } while(updated);

            // Replace id with actual values
            var idMap = new Dictionary<uint, uint>();
            idMap[0xffffffff] = 0;
            idMap[0xfffffffe] = this.methodInfo.EntryKey;
            idMap[0xfffffffd] = this.methodInfo.ExitKey;
            foreach(ILBlock block in blocks)
            {
                BlockKey key = this.Keys[block];

                uint entryId = key.Entry;
                if(!idMap.TryGetValue(entryId, out key.Entry))
                    key.Entry = idMap[entryId] = (byte)this.runtime.Descriptor.Random.Next();

                uint exitId = key.Exit;
                if(!idMap.TryGetValue(exitId, out key.Exit))
                    key.Exit = idMap[exitId] = (byte)this.runtime.Descriptor.Random.Next();

                this.Keys[block] = key;
            }
        }

        private EHMap MapEHs(ScopeBlock rootScope)
        {
            var map = new EHMap();
            this.MapEHsInternal(rootScope, map);
            return map;
        }

        private void MapEHsInternal(ScopeBlock scope, EHMap map)
        {
            if(scope.Type == ScopeType.Filter)
            {
                map.Starts.Add((ILBlock) scope.GetBasicBlocks().First());
            }
            else if(scope.Type != ScopeType.None)
            {
                if(scope.ExceptionHandler.HandlerType == ExceptionHandlerType.Finally)
                {
                    if (!map.Finally.TryGetValue(scope.ExceptionHandler, out FinallyInfo info))
                        map.Finally[scope.ExceptionHandler] = info = new FinallyInfo();

                    if (scope.Type == ScopeType.Try)
                    {
                        // Try End Next
                        var scopeBlocks = new HashSet<IBasicBlock>(scope.GetBasicBlocks());
                        foreach(ILBlock block in scopeBlocks)
                            if((block.Flags & BlockFlags.ExitEHLeave) != 0 &&
                               (block.Targets.Count == 0 ||
                                block.Targets.Any(target => !scopeBlocks.Contains(target))))
                                foreach(BasicBlock<ILInstrList> target in block.Targets)
                                    info.TryEndNexts.Add((ILBlock) target);
                    }
                    else if(scope.Type == ScopeType.Handler)
                    {
                        // Finally End
                        IEnumerable<IBasicBlock> candidates;
                        if(scope.Children.Count > 0)
                            candidates = scope.Children
                                .Where(s => s.Type == ScopeType.None)
                                .SelectMany(s => s.GetBasicBlocks());
                        else candidates = scope.Content;
                        foreach(ILBlock block in candidates)
                            if((block.Flags & BlockFlags.ExitEHReturn) != 0 &&
                               block.Targets.Count == 0) info.FinallyEnds.Add(block);
                    }
                }
                if(scope.Type == ScopeType.Handler)
                    map.Starts.Add((ILBlock) scope.GetBasicBlocks().First());
            }
            foreach(ScopeBlock child in scope.Children)
                this.MapEHsInternal(child, map);
        }

        private void MatchHandlers(EHMap map, ref bool updated)
        {
            // handler start = 0xffffffff
            // finally end = next block of try end
            foreach(ILBlock start in map.Starts)
            {
                BlockKey key = this.Keys[start];
                if(key.Entry != 0xffffffff)
                {
                    key.Entry = 0xffffffff;
                    this.Keys[start] = key;
                    updated = true;
                }
            }
            foreach(FinallyInfo info in map.Finally.Values)
            {
                uint maxEnd = info.FinallyEnds.Max(block => this.Keys[block].Exit);
                uint maxEntry = info.TryEndNexts.Max(block => this.Keys[block].Entry);
                uint maxId = Math.Max(maxEnd, maxEntry);

                foreach(ILBlock block in info.FinallyEnds)
                {
                    BlockKey key = this.Keys[block];
                    if(key.Exit != maxId)
                    {
                        key.Exit = maxId;
                        this.Keys[block] = key;
                        updated = true;
                    }
                }

                foreach(ILBlock block in info.TryEndNexts)
                {
                    BlockKey key = this.Keys[block];
                    if(key.Entry != maxId)
                    {
                        key.Entry = maxId;
                        this.Keys[block] = key;
                        updated = true;
                    }
                }
            }
        }

        private struct BlockKey
        {
            public uint Entry;
            public uint Exit;
        }

        private class FinallyInfo
        {
            public readonly HashSet<ILBlock> FinallyEnds = new HashSet<ILBlock>();
            public readonly HashSet<ILBlock> TryEndNexts = new HashSet<ILBlock>();
        }

        private class EHMap
        {
            public readonly Dictionary<ExceptionHandler, FinallyInfo> Finally = new Dictionary<ExceptionHandler, FinallyInfo>();
            public readonly HashSet<ILBlock> Starts = new HashSet<ILBlock>();
        }
    }
}