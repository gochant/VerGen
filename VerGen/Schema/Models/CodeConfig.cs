using System.Xml.Serialization;

namespace VerGen.Schema.Models
{
    public class CodeConfig
    {
        #region Properties, Indexers

        /// <summary>
        /// DTO路径
        /// </summary>
        [XmlAttribute]
        public string DtoPath { get; set; } = "Dtos";

        /// <summary>
        /// Model路径
        /// </summary>
        [XmlAttribute]
        public string EntityPath { get; set; } = "Models";

        /// <summary>
        /// Controller路径
        /// </summary>
        [XmlAttribute]
        public string ControllerPath { get; set; } = "Controllers";

        /// <summary>
        /// Service路径
        /// </summary>
        [XmlAttribute]
        public string ServicePath { get; set; } = "Services";

        /// <summary>
        /// MapProfile路径
        /// </summary>
        [XmlAttribute]
        public string MapProfilePath { get; set; } = "Maps";

        /// <summary>
        /// Js路径
        /// </summary>
        [XmlAttribute]
        public string JsPath { get; set; } = "app/modules/basic";
        [XmlAttribute]
        public string ServiceSuffix { get; set; } = "Service";
        [XmlAttribute]
        public string ControllerSuffix { get; set; } = "Controller";

        [XmlAttribute]
        public string DefaultEntityKeyType { get; set; } = "string";

        #endregion

        #region Methods

        //public string ResolveNamespace(string outputPath)
        //{
        //    return (SuggestNamespace ?? string.Empty) + "." + string.Join(".", outputPath.Split('/'));
        //}

        #endregion
    }
}