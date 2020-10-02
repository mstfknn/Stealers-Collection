using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Confuser.Core;
using Confuser.Core.Services;
using Confuser.Renamer.Analyzers;
using dnlib.DotNet;
using Microsoft.VisualBasic;

namespace Confuser.Renamer
{
    public interface INameService
    {
        VTableStorage GetVTables();

        void Analyze(IDnlibDef def);

        bool CanRename(object obj);
        void SetCanRename(object obj, bool val);

        void SetParam(IDnlibDef def, string name, string value);
        string GetParam(IDnlibDef def, string name);

        RenameMode GetRenameMode(object obj);
        void SetRenameMode(object obj, RenameMode val);
        void ReduceRenameMode(object obj, RenameMode val);

        string RandomName();

        void RegisterRenamer(IRenamer renamer);
        T FindRenamer<T>();
        void AddReference<T>(T obj, INameReference<T> reference);

        void SetOriginalName(object obj, string name);
        void SetOriginalNamespace(object obj, string ns);

        void MarkHelper(IDnlibDef def, IMarkerService marker, ConfuserComponent parentComp);
    }

    public class NameService : INameService
    {
        static readonly object CanRenameKey = new object();
        static readonly object RenameModeKey = new object();
        static readonly object ReferencesKey = new object();
        static readonly object OriginalNameKey = new object();
        static readonly object OriginalNamespaceKey = new object();

        readonly ConfuserContext context;
        readonly byte[] nameSeed;
        readonly RandomGenerator random;
        readonly VTableStorage storage;
        AnalyzePhase analyze;

        readonly HashSet<string> identifiers = new HashSet<string>();
        readonly byte[] nameId = new byte[8];
        readonly Dictionary<string, string> nameMap1 = new Dictionary<string, string>();
        readonly Dictionary<string, string> nameMap2 = new Dictionary<string, string>();
        internal ReversibleRenamer reversibleRenamer;

        public NameService(ConfuserContext context)
        {
            this.context = context;
            this.storage = new VTableStorage(context.Logger);
            this.random = context.Registry.GetService<IRandomService>().GetRandomGenerator(NameProtection._FullId);
            this.nameSeed = this.random.NextBytes(20);

            this.Renamers = new List<IRenamer> {
                new InterReferenceAnalyzer(),
                new VTableAnalyzer(),
                new TypeBlobAnalyzer(),
                new ResourceAnalyzer(),
                new LdtokenEnumAnalyzer()
            };
        }

        public IList<IRenamer> Renamers { get; private set; }
        public List<string> Names = new List<string>();
        private static readonly Random _rnd = new Random();

        private static Random kys = new Random();
        private static string charset, rename;

        private static Random _rand = new Random();

        public static string GetRandomLine(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

#pragma warning disable SCS0005 // Weak random generator
            int lineNumber = _rand.Next(0, lines.Length);
#pragma warning restore SCS0005 // Weak random generator

            return lines[lineNumber];
        }

        public static string RandomNameText()
        {
            charset = "他是说汉语的ｱ尺乇你他是说汉语的ｱ尺乇你他是说汉语的ｱ尺乇你abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789123456789123456789123456789123456789123456789ᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠᅠαβγδεζηθικλμνξοπρστυφχψωαβγδεζηθικλμνξοπρστυφχψωαβγδε";
            rename = "dark#5000-protector|";

            char[] chArray = new char[(20 - 1) + 1];
            int num = 20 - 1;
            for (int k = 0; k <= num; k++)
            {
                chArray[k] = charset[(int)Math.Round((double)Conversion.Int((float)(VBMath.Rnd() * charset.Length)))];
            }
            rename += new string(chArray);

            return rename;
        }

        public static string RandomNameCustom() => GetRandomLine(Path.Combine(Environment.CurrentDirectory, "Config", "Renamer.txt"));

        string namesdqsdq;

        public string RandomName()
        {

            this.namesdqsdq = File.Exists(Path.Combine(Environment.CurrentDirectory, "Config", "Renamer.txt")) ? RandomNameCustom() : RandomNameText();

            return this.namesdqsdq;
        }

        static string namesdqsdq1;

        public static string RandomNameStatic()
        {

            namesdqsdq1 = File.Exists(Path.Combine(Environment.CurrentDirectory, "Config", "Renamer.txt")) ? RandomNameCustom() : RandomNameText();

            return namesdqsdq1;
        }

        public VTableStorage GetVTables() => this.storage;

        public bool CanRename(object obj)
        {
            if (obj is IDnlibDef)
            {
                if (this.analyze == null)
                    this.analyze = this.context.Pipeline.FindPhase<AnalyzePhase>();

                var prot = (NameProtection)this.analyze.Parent;
                ProtectionSettings parameters = ProtectionParameters.GetParameters(this.context, (IDnlibDef)obj);
                return parameters == null || !parameters.ContainsKey(prot) ? false : this.context.Annotations.Get(obj, CanRenameKey, true);
            }
            return false;
        }

        public void SetCanRename(object obj, bool val) => this.context.Annotations.Set(obj, CanRenameKey, val);

        public void SetParam(IDnlibDef def, string name, string value)
        {
            ProtectionSettings param = ProtectionParameters.GetParameters(this.context, def);
            if (param == null)
                ProtectionParameters.SetParameters(this.context, def, param = new ProtectionSettings());
            if (!param.TryGetValue(this.analyze.Parent, out Dictionary<string, string> nameParam))
                param[this.analyze.Parent] = nameParam = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            nameParam[name] = value;
        }

        public string GetParam(IDnlibDef def, string name)
        {
            ProtectionSettings param = ProtectionParameters.GetParameters(this.context, def);
            if (param == null)
                return null;
            return !param.TryGetValue(this.analyze.Parent, out Dictionary<string, string> nameParam) ? null : nameParam.GetValueOrDefault(name);
        }

        public RenameMode GetRenameMode(object obj) => this.context.Annotations.Get(obj, RenameModeKey, RenameMode.Unicode);

        public void SetRenameMode(object obj, RenameMode val) => this.context.Annotations.Set(obj, RenameModeKey, val);

        public void ReduceRenameMode(object obj, RenameMode val)
        {
            RenameMode original = this.GetRenameMode(obj);
            if (original < val)
                this.context.Annotations.Set(obj, RenameModeKey, val);
        }

        public void AddReference<T>(T obj, INameReference<T> reference) => this.context.Annotations.GetOrCreate(obj, ReferencesKey, key => new List<INameReference>()).Add(reference);

        public void Analyze(IDnlibDef def)
        {
            if (this.analyze == null)
                this.analyze = this.context.Pipeline.FindPhase<AnalyzePhase>();

            this.SetOriginalName(def, def.Name);
            if (def is TypeDef)
            {
                this.GetVTables().GetVTable((TypeDef)def);
                this.SetOriginalNamespace(def, ((TypeDef)def).Namespace);
            }
            this.analyze.Analyze(this, this.context, ProtectionParameters.Empty, def, true);
        }

        public void SetNameId(uint id)
        {
            for (int i = this.nameId.Length - 1; i >= 0; i--)
            {
                this.nameId[i] = (byte)(id & 0xff);
                id >>= 8;
            }
        }

        void IncrementNameId()
        {
            for (int i = this.nameId.Length - 1; i >= 0; i--)
            {
                this.nameId[i]++;
                if (this.nameId[i] != 0)
                    break;
            }
        }
 
        string ObfuscateNameInternal(byte[] hash, RenameMode mode)
        {
            switch (mode)
            {
                case RenameMode.Empty:
                    return "";
                case RenameMode.Unicode:
                    return this.RandomName();
                case RenameMode.Letters:
                    return this.RandomName();
                case RenameMode.ASCII:
                    return this.RandomName();
                case RenameMode.Decodable:
                    this.IncrementNameId();
                    return this.RandomName();
                case RenameMode.Sequential:
                    this.IncrementNameId();
                    return this.RandomName();
                default:

                    throw new NotSupportedException($"Rename mode '{mode}' is not supported.");
            }
        }

        string ParseGenericName(string name, out int? count)
        {
            if (name.LastIndexOf('`') != -1)
            {
                int index = name.LastIndexOf('`');
                if (int.TryParse(name.Substring(index + 1), out int c))
                {
                    count = c;
                    return name.Substring(0, index);
                }
            }
            count = null;
            return name;
        }

        private string MakeGenericName(string name, int? count) => count == null ? name : $"{name}`{count.Value}";

        public void SetOriginalName(object obj, string name)
        {
            this.identifiers.Add(name);
            this.context.Annotations.Set(obj, OriginalNameKey, name);
        }

        public void SetOriginalNamespace(object obj, string ns)
        {
            this.identifiers.Add(ns);
            this.context.Annotations.Set(obj, OriginalNamespaceKey, ns);
        }

        public void RegisterRenamer(IRenamer renamer) => this.Renamers.Add(renamer);

        public T FindRenamer<T>() => this.Renamers.OfType<T>().Single();

        public static void MarkHelperStatic(IDnlibDef def, IMarkerService marker, ConfuserComponent parentComp)
        {
            if (marker.IsMarked(def))
                return;
            if (def is MethodDef method)
            {
                method.Access = MethodAttributes.Assembly;
                if (!method.IsSpecialName && !method.IsRuntimeSpecialName && !method.DeclaringType.IsDelegate())
                    method.Name = RandomNameStatic();
            }
            else if (def is FieldDef field)
            {
                field.Access = FieldAttributes.Assembly;
                if (!field.IsSpecialName && !field.IsRuntimeSpecialName)
                    field.Name = RandomNameStatic();
            }
            else if (def is TypeDef type)
            {
                type.Visibility = type.DeclaringType == null ? TypeAttributes.NotPublic : TypeAttributes.NestedAssembly;
                type.Namespace = RandomNameStatic();
                if (!type.IsSpecialName && !type.IsRuntimeSpecialName)
                    type.Name = RandomNameStatic();
            }

            marker.Mark(def, parentComp);
        }

        public void MarkHelper(IDnlibDef def, IMarkerService marker, ConfuserComponent parentComp)
        {
            if (marker.IsMarked(def))
                return;
            if (def is MethodDef method)
            {
                method.Access = MethodAttributes.Assembly;
                if (!method.IsSpecialName && !method.IsRuntimeSpecialName && !method.DeclaringType.IsDelegate())
                    method.Name = this.RandomName();
            }
            else if (def is FieldDef field)
            {
                field.Access = FieldAttributes.Assembly;
                if (!field.IsSpecialName && !field.IsRuntimeSpecialName)
                    field.Name = this.RandomName();
            }
            else if (def is TypeDef type)
            {
                type.Visibility = type.DeclaringType == null ? TypeAttributes.NotPublic : TypeAttributes.NestedAssembly;
                type.Namespace = this.RandomName();
                if (!type.IsSpecialName && !type.IsRuntimeSpecialName)
                    type.Name = this.RandomName();
            }
            this.SetCanRename(def, false);
            this.Analyze(def);
            marker.Mark(def, parentComp);
        }

        #region Charsets

        static readonly char[] asciiCharset = Enumerable.Range(32, 95)
                                                        .Select(ord => (char)ord)
                                                        .Except(new[] { '.' })
                                                        .ToArray();

        static readonly char[] letterCharset = Enumerable.Range(0, 26)
                                                         .SelectMany(ord => new[] { (char)('a' + ord), (char)('A' + ord) })
                                                         .ToArray();

        static readonly char[] alphaNumCharset = Enumerable.Range(0, 26)
                                                           .SelectMany(ord => new[] { (char)('a' + ord), (char)('A' + ord) })
                                                           .Concat(Enumerable.Range(0, 10).Select(ord => (char)('0' + ord)))
                                                           .ToArray();

        // Especially chosen, just to mess with people.
        // Inspired by: http://xkcd.com/1137/ :D
        static readonly char[] unicodeCharset = new char[] { }
            .Concat(Enumerable.Range(0x200b, 5).Select(ord => (char)ord))
            .Concat(Enumerable.Range(0x2029, 6).Select(ord => (char)ord))
            .Concat(Enumerable.Range(0x206a, 6).Select(ord => (char)ord))
            .Except(new[] { '\u2029' })
            .ToArray();

        #endregion

        public RandomGenerator GetRandom() => this.random;

        public IList<INameReference> GetReferences(object obj) => this.context.Annotations.GetLazy(obj, ReferencesKey, key => new List<INameReference>());

        public string GetOriginalName(object obj) => this.context.Annotations.Get(obj, OriginalNameKey, "");

        public string GetOriginalNamespace(object obj) => this.context.Annotations.Get(obj, OriginalNamespaceKey, "");

        public ICollection<KeyValuePair<string, string>> GetNameMap() => this.nameMap2;
    }
}