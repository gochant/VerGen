namespace VerGen.TemplateParameters
{
    public interface IEntityParameter
    {
        /// <summary>
        /// 实体命名空间
        /// </summary>
        string EntityNamespace { get; set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        string EntityName { get; set; }

        /// <summary>
        /// 实体键类型
        /// </summary>
        string EntityKeyType { get; set; }
    }
}