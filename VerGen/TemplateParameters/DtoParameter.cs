using System;
using System.Collections.Generic;
using VerGen.Enums;
using VerGen.Ext;
using VerGen.Schema.Models;

namespace VerGen.TemplateParameters
{
    [Serializable]
    public class DtoParameter : ITemplateParameter, IEntityParameter
    {
        public DtoParameter(string name)
        {
            Name = name;
        }

        public string Namespace { get; set; }
        public string Name { get; set; }

        public string Display { get; set; }

        public string Inheritance { get; set; }

        public List<PropertyParameter> Properties { get; set; } = new List<PropertyParameter>();
        public string TemplateName { get; set; } = "Dto.tt";
        public string OutputPath { get; set; }
        public ComponentType Type { get; set; } = ComponentType.Dto;
        public string FileName { get; set; }
        public ITemplateParameter Initialize(BusinessModelPackage package, CodeConfig config, object addtionalOptions = null)
        {
            this.InitializeForInterface(package.Set, config);
            OutputPath = config.DtoPath;
            FileName = Name + ".cs";

            var vm = package.GetViewModel(Name);
            if (vm == null) return this;

          //  Namespace = config.ResolveNamespace(config.DtoPath);
            Inheritance = "IDto";
            Display = vm.DisplayName ?? Name;

            foreach (var field in vm.Fields)
            {
                var fieldMetadata = package.CommonModel.GetField(field.Name);
                if (fieldMetadata == null) continue;
                // TODO: 没有处理默认值
                var propParam = new PropertyParameter
                {
                    Name = field.Name,
                    Type = fieldMetadata.Type,
                    Accessibility = "public",
                    Annotations = fieldMetadata.GetDataAnnotationStrings(),
                    Display = fieldMetadata.Display?.Name ?? fieldMetadata.Name
                };
                Properties.Add(propParam);
            }

            return this;
        }

        public string EntityNamespace { get; set; }
        public string EntityName { get; set; }
        public string EntityKeyType { get; set; }
    }
}