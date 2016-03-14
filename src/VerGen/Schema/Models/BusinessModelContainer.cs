using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;
using VerGen.Utility;

namespace VerGen.Schema.Models
{
    public class BusinessModelContainer : ViewModelBase
    {
        #region Properties, Indexers

        [XmlIgnore]
        public EdmItemCollection Edm { get; set; }

        /// <summary>
        /// EDM文件路径
        /// </summary>
        public string EdmFilePath { get; set; }

        public string SaveFilePath { get; set; }

        /// <summary>
        /// 代码配置
        /// </summary>
        [XmlIgnore]
        public CodeConfig Config { get; set; } = new CodeConfig();

        /// <summary>
        /// 模型包列表
        /// </summary>
        public ObservableCollection<BusinessModelPackage> Packages { get; set; }
            = new ObservableCollection<BusinessModelPackage>();

        #endregion

        #region Methods

        public IEnumerable<EntitySet> GetEntitySets()
        {
            return Edm.GetItems<EntityContainer>()?.FirstOrDefault()?.EntitySets;
        }

        public EntitySet GetEntitySet(string elementName)
        {
            return GetEntitySets().FirstOrDefault(d => d.ElementType.Name == elementName);
        }

        public void LoadEdm(string edmPath = null)
        {
            var itemCollection = new EdmMetadataLoader().CreateEdmItemCollection(edmPath);
            LoadEdm((EdmItemCollection)itemCollection);
        }

        public void LoadEdm(EdmItemCollection edm)
        {
            Edm = edm;
            var invalids = new List<string>();
            foreach (var package in Packages)
            {
                var set = GetEntitySet(package.Name);
                if(set == null)
                {
                    package.Invalid = true;
                }
                else
                {
                    package.LoadDynamicData(set);
                }
            }
        }

        public void LoadEdm(IEnumerable<GlobalItem> edm)
        {
            Edm = (EdmItemCollection)edm;
            LoadEdm(Edm);
        }

        public static T LoadFromFile<T>(string fileName, XmlSerializer serializer = null) where T : BusinessModelContainer, new()
        {
            if (File.Exists(fileName))
            {
                try
                {
                    T result;
                    if (serializer == null)
                    {
                        serializer = CreateSerializer<T>();
                        var xmlDoc = File.ReadAllText(fileName);
                        xmlDoc = RemoveAllNamespaces(xmlDoc);
                        result = (T)serializer.Deserialize(new StringReader(xmlDoc));
                    }
                    else
                    {
                        result = (T)serializer.Deserialize(File.OpenRead(fileName));
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    // 创建新的
                    var result = new T
                    {
                        SaveFilePath = fileName
                    };
                    return result;
                }
            }
            return null;
        }

        public static XmlSerializer CreateSerializer<T>()
        {
            var serializer = new XmlSerializer(typeof(T), string.Empty);
            return serializer;
        }

        public static string RemoveAllNamespaces(string xmlDocument)
        {
            XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

            return xmlDocumentWithoutNs.ToString();
        }

        //Core recursion function
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            var xElement = xmlDocument.HasElements ? new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)))
                : new XElement(xmlDocument.Name.LocalName, xmlDocument.Value);

            foreach (var attribute in xmlDocument.Attributes())
            {
                if (attribute.Name.LocalName != "type" ||
                    string.IsNullOrEmpty(attribute.Name.NamespaceName))
                {
                    xElement.Add(attribute);
                }
            }
         
            return xElement;
        }

        #endregion


    }
}