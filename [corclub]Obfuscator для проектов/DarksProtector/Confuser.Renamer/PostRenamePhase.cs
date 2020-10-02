using Confuser.Core;
using dnlib.DotNet;

namespace Confuser.Renamer
{
    internal class PostRenamePhase : ProtectionPhase {
		public PostRenamePhase(NameProtection parent)
			: base(parent) { }

        public override bool ProcessAll => true;

        public override ProtectionTargets Targets => ProtectionTargets.AllDefinitions;

        public override string Name => "Post-renaming";

        protected override void Execute(ConfuserContext context, ProtectionParameters parameters) {
			var service = (NameService)context.Registry.GetService<INameService>();

			foreach (IRenamer renamer in service.Renamers) {
				foreach (IDnlibDef def in parameters.Targets)
					renamer.PostRename(context, service, parameters, def);
				context.CheckCancellation();
			}
		}
	}
}