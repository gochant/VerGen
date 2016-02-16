namespace VerGen.Tool.UI.ViewModels
{
    /// <summary>
    /// 实体集列表项
    /// </summary>
    public class EntitySetListItem
    {
        /// <summary>
        /// 数据集名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模型名称
        /// </summary>
        public string ModelName { get; set; }

        public string Display { get; set; }

        public bool IsCreatedDefine { get; set; }
    }
}