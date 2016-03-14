namespace VerGen.TemplateParameters
{
    public interface IServiceParameter
    {


        /// <summary>
        /// 服务命名空间
        /// </summary>
        string ServiceNamespace { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        string ServiceName { get; set; }

    }
}