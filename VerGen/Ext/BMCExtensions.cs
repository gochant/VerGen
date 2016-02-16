using System.Collections.Generic;
using System.Linq;
using VerGen.Schema.Infrastructure;
using VerGen.Schema.Models;
using VerGen.TemplateParameters;

namespace VerGen.Ext
{
    public static class BMCExtensions
    {
        public static List<DtoParameter> GetDtoParameters(this BusinessModelContainer container)
        {
            var result = new List<DtoParameter>();
            foreach (var package in container.Packages)
            {
                var prs = package.ViewModels.Select(d => new DtoParameter(d.Name));
                foreach (var pr in prs)
                {
                    pr.Initialize(package, container.Config);
                    result.Add(pr);
                }
            }
            return result;
        }

        public static List<MapProfileParameter> GetMapProfileParameters(this BusinessModelContainer container)
        {
            return container.Packages
                .Select(package => (MapProfileParameter)(new MapProfileParameter().Initialize(package, container.Config)))
                .ToList();
        }

        public static List<ControllerParameter> GetControllerParameters(this BusinessModelContainer container)
        {
            return container.Packages.Select(p => (ControllerParameter)(new ControllerParameter().Initialize(p, container.Config)))
                .ToList();
        }

        public static List<ServiceParameter> GetServiceParameters(this BusinessModelContainer container)
        {
            return container.Packages.Select(p => (ServiceParameter)(new ServiceParameter().Initialize(p, container.Config))).ToList();
        }



        public static ViewModelDefine GetViewModel(this BusinessModelPackage package,  string name)
        {
            return package.ViewModels.FirstOrDefault(d => d.Name == name);
        }

        public static ModelFieldDefine GetField(this CommonModelDefine model, string name)
        {
            return model.Fields.FirstOrDefault(d => d.Name == name);
        }

        /// <summary>
        /// 获取所有的数据标注字符串
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDataAnnotationStrings(this ModelFieldDefine field)
        {
            return (from propertyInfo in field.GetType().GetProperties()
                    where typeof(IDataAnnontationAttrDefine).IsAssignableFrom(propertyInfo.PropertyType)
                    select (IDataAnnontationAttrDefine)propertyInfo.GetValue(field) into attr
                    where attr.IsApplicable && attr.Enabled
                    select attr.ToDataAnnotationString()).ToList();
        }
    }
}