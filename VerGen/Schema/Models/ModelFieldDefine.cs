using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml.Serialization;
using VerGen.Schema.Infrastructure;

namespace VerGen.Schema.Models
{
    /// <summary>
    /// 模型字段定义
    /// </summary>
    public class ModelFieldDefine : ViewModelBase
    {
        private string name;
        private string type;
        private bool isCalculated;
        private bool isReadonly;
        private bool isKey;
        private string associatedField;
        private EdmProperty edmProperty;
        private bool invalid;

        #region Properties, Indexers

        [XmlIgnore]
        public bool Invalid
        {
            get { return invalid; }
            set { SetField(ref invalid, value); }
        }

        /// <summary>
        /// 实体数据模型属性
        /// </summary>
        [XmlIgnore]
        public EdmProperty EdmProperty
        {
            get { return edmProperty; }
            set { SetField(ref edmProperty, value); }
        }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlAttribute]
        public string Name
        {
            get { return name; }
            set { SetField(ref name, value); }
        }

        /// <summary>
        /// 类型
        /// </summary>
        [XmlAttribute]
        public string Type
        {
            get { return type; }
            set { SetField(ref type, value); }
        }

        /// <summary>
        /// 是计算字段
        /// </summary>
        [XmlAttribute]
        public bool IsCalculated
        {
            get { return isCalculated; }
            set { SetField(ref isCalculated, value); }
        }

        /// <summary>
        /// 是只读字段
        /// </summary>
        [XmlAttribute]
        public bool IsReadonly
        {
            get { return isReadonly; }
            set { SetField(ref isReadonly, value); }
        }

        /// <summary>
        /// 是键
        /// </summary>
        [XmlAttribute]
        public bool IsKey
        {
            get { return isKey; }
            set { SetField(ref isKey, value); }
        }

        /// <summary>
        /// 关联字段
        /// </summary>
        [XmlAttribute]
        public string AssociatedField
        {
            get { return associatedField; }
            set { SetField(ref associatedField, value); }
        }

        /// <summary>
        /// 概念模型到视图模型
        /// </summary>
        public FieldMap CtoVMap { get; set; } = new FieldMap();

        /// <summary>
        /// 视图模型到概念模型
        /// </summary>
        public FieldMap VtoCMap { get; set; } = new FieldMap();

        #region 自定义特性

        public List<CustomAttrDefine> Customs { get; set; } = new List<CustomAttrDefine>();

        #endregion

        #region 显示特性

        public DisplayAttrDefine Display { get; set; } = new DisplayAttrDefine();

        public DisplayFieldAttrDefine DisplayField { get; set; } = new DisplayFieldAttrDefine();

        public DataTypeAttrDefine DataType { get; set; } = new DataTypeAttrDefine();

        public DisplayFormatAttrDefine DisplayFormat { get; set; } = new DisplayFormatAttrDefine();

        public UIHintAttrDefine UIHint { get; set; } = new UIHintAttrDefine();

        #endregion

        #region 验证特性

        public RequiredAttrDefine Required { get; set; } = new RequiredAttrDefine();

        public StringLengthAttrDefine StringLength { get; set; } = new StringLengthAttrDefine();

        public RangeAttrDefine Range { get; set; } = new RangeAttrDefine();

        public RegExpAttrDefine RegExp { get; set; } = new RegExpAttrDefine();

        #endregion

        #endregion

        public void LoadDynamicData(EdmProperty prop)
        {
            EdmProperty = prop;
        }
    }


}
