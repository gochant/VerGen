namespace VerGen.TemplateParameters
{
    public interface IEntityParameter
    {
        /// <summary>
        /// ʵ�������ռ�
        /// </summary>
        string EntityNamespace { get; set; }

        /// <summary>
        /// ʵ������
        /// </summary>
        string EntityName { get; set; }

        /// <summary>
        /// ʵ�������
        /// </summary>
        string EntityKeyType { get; set; }
    }
}