using System;
using System.Collections.Generic;
using System.Linq;
using VerGen.Enums;
using VerGen.Ext;
using VerGen.Schema.Models;

namespace VerGen.TemplateParameters
{
    [Serializable]
    public class MapProfileParameter : ITemplateParameter
    {
        public string EntityNamespace { get; set; }
        public string DtoNamespace { get; set; }
        public string MapProfileNamespace { get; set; }
        public string EntityName { get; set; }

        public List<string> CToVNames { get; set; }

        public Dictionary<string, List<string>> CToVExpressions { get; set; } = new Dictionary<string, List<string>>();

        public List<string> VToCNames { get; set; }

        public Dictionary<string, List<string>> VToCExpressions { get; set; } = new Dictionary<string, List<string>>();

        public string TemplateName { get; set; }
        public string OutputPath { get; set; }
        public ComponentType Type { get; set; }
        public string FileName { get; set; }
        public ITemplateParameter Initialize(BusinessModelPackage package, CodeConfig config, object addtionalOptions = null)
        {
            this.InitializeForInterface(package.Set, config);

            EntityName = package.Name;
            CToVNames = package.ViewModels.Select(d => d.Name).ToList();
            foreach (var name in CToVNames)
            {
                var vm = package.GetViewModel(name);
                var vmExps = (from vmField in vm.Fields
                              select package.CommonModel.GetField(vmField.Name) into field
                              where field.CtoVMap.IsUsedAsExpression && !string.IsNullOrWhiteSpace(field.CtoVMap.SourceCode)
                              select $".ForMember(d => d.{field.Name}, opt => opt.MapFrom(d => {field.CtoVMap.SourceCode}))")
                              .ToList();

                CToVExpressions.Add(name, vmExps);
            }
            VToCNames = package.ViewModels.Where(d => d.EnableReverseMap).Select(d => d.Name).ToList();
            foreach (var name in VToCNames)
            {
                var vm = package.GetViewModel(name);
                var vmExps = (from vmField in vm.Fields
                              select package.CommonModel.GetField(vmField.Name) into field
                              where field.VtoCMap.IsUsedAsExpression && !string.IsNullOrWhiteSpace(field.VtoCMap.SourceCode)
                              select $".ForMember(d => d.{field.AssociatedField ?? field.Name}, opt => opt.MapFrom(d => {field.VtoCMap.SourceCode}))")
                              .ToList();

                VToCExpressions.Add(name, vmExps);
            }

            return this;
        }
    }
}