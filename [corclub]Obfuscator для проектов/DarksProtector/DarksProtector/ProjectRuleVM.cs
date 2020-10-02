using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Confuser.Core;
using Confuser.Core.Project;
using Confuser.Core.Project.Patterns;

namespace DarksProtector
{
    internal interface IRuleContainer
    {
        IList<ProjectRuleVM> Rules { get; }
    }

    public class ProjectRuleVM : ViewModelBase, IViewModel<Rule>
    {
        private readonly Rule rule;
        private string error;
        private PatternExpression exp;

        public ProjectRuleVM(ProjectVM parent, Rule rule)
        {
            this.Project = parent;
            this.rule = rule;

            ObservableCollection<ProjectSettingVM<Protection>> protections = Utils.Wrap(rule, setting => new ProjectSettingVM<Protection>(parent, setting));
            protections.CollectionChanged += (sender, e) => parent.IsModified = true;
            this.Protections = protections;

            this.ParseExpression();
        }

        public ProjectVM Project { get; }

        public string Pattern
        {
            get => this.rule.Pattern;
            set
            {
                if (this.SetProperty(this.rule.Pattern != value, val => this.rule.Pattern = val, value, "Pattern"))
                {
                    this.Project.IsModified = true;
                    this.ParseExpression();
                }
            }
        }

        public PatternExpression Expression
        {
            get => this.exp;
            set => this.SetProperty(ref this.exp, value, "Expression");
        }

        public string ExpressionError
        {
            get => this.error;
            set => this.SetProperty(ref this.error, value, "ExpressionError");
        }

        public ProtectionPreset Preset
        {
            get => this.rule.Preset;
            set
            {
                if (this.SetProperty(this.rule.Preset != value, val => this.rule.Preset = val, value, "Preset"))
                    this.Project.IsModified = true;
            }
        }

        public bool Inherit
        {
            get => this.rule.Inherit;
            set
            {
                if (this.SetProperty(this.rule.Inherit != value, val => this.rule.Inherit = val, value, "Inherit"))
                    this.Project.IsModified = true;
            }
        }

        public IList<ProjectSettingVM<Protection>> Protections { get; private set; }

        Rule IViewModel<Rule>.Model => this.rule;

        void ParseExpression()
        {
            if (this.Pattern == null)
                return;
            PatternExpression expression;
            try
            {
                expression = new PatternParser().Parse(this.Pattern);
                this.ExpressionError = null;
            }
            catch (Exception e)
            {
                this.ExpressionError = e.Message;
                expression = null;
            }
            this.Expression = expression;
        }
    }
}