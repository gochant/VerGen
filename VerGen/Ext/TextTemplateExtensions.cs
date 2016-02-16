using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using VerGen.Schema.Models;
using VerGen.TemplateParameters;
using VerGen.Utility;

namespace VerGen.Ext
{
    public static class TextTemplateExtensions
    {
        public static ITemplateParameter InitializeForInterface(this ITemplateParameter param,
            EntitySet set, CodeConfig config, object additionalOptions = null)
        {
            (param as IServiceParameter)?.InitServiceParam(set, config);
            (param as IEntityParameter)?.InitEntityParam(set, config);
            (param as IControllerParameter)?.InitControllerParam(set, config);
            return param;
        }

        public static void InitServiceParam(this IServiceParameter param,
            EntitySet set, CodeConfig config)
        {
            param.ServiceName = set.ElementType.Name + config.ServiceSuffix;
        }

        public static void InitEntityParam(this IEntityParameter param, EntitySet set, CodeConfig config)
        {
            param.EntityKeyType = set.ElementType.GetKeyType();
            param.EntityName = set.ElementType.Name;
        }

        public static void InitControllerParam(this IControllerParameter param,
            EntitySet set, CodeConfig config)
        {
            param.EntitySetName = set.Name;
        }

        public static string GetKeyType(this EntityType type)
        {
            var typeUsage = type.KeyMembers.FirstOrDefault()?.TypeUsage;
            var typeName = new TypeHelper().GetTypeName(typeUsage);
            return typeName;
        }
    }
}