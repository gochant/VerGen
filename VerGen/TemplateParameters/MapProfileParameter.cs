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

        public List<string> VToCNames { get; set; }

        public string TemplateName { get; set; } = "MapProfile.tt";
        public string OutputPath { get; set; }
        public ComponentType Type { get; set; }
        public string FileName { get; set; }
        public ITemplateParameter Initialize(BusinessModelPackage package, CodeConfig config, object addtionalOptions = null)
        {
            this.InitializeForInterface(package.Set, config);
            EntityName = package.Name;
            CToVNames = package.ViewModels.Select(d => d.Name).ToList();
            VToCNames = package.ViewModels.Where(d => d.EnableReverseMap).Select(d => d.Name).ToList();
            return this;
        }
    }
}