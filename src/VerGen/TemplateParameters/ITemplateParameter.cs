using VerGen.Enums;
using VerGen.Schema.Models;

namespace VerGen.TemplateParameters
{
    public interface ITemplateParameter
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        string TemplateName { get; set; }

        /// <summary>
        /// 文件输出路径
        /// </summary>
        string OutputPath { get; set; }

        /// <summary>
        /// 参数组件类型
        /// </summary>
        ComponentType Type { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        string FileName { get; set; }

        ITemplateParameter Initialize(BusinessModelPackage package, CodeConfig config, object addtionalOptions = null);
    }
}