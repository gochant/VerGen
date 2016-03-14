using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml.Serialization;
using VerGen.Schema.Models;
using VerGen.Utility;

namespace VerGen.Tool.UI.ViewModels
{
    /// <summary>
    /// 模型字段定义
    /// </summary>
    public class ModelFieldDefineViewModel : ModelFieldDefine
    {

        #region Methods

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="prop"></param>
        public void Initialize(EdmProperty prop)
        {
            var type = new TypeHelper().GetTypeName(prop.TypeUsage);

            Initialize(prop.Name, type, prop.Documentation?.Summary);

            Required.Enabled = !prop.Nullable;
            // 设置字符串最大长度
            if (prop.IsMaxLength && prop.MaxLength.HasValue)
            {
                StringLength.Enabled = true;
                StringLength.Max = prop.MaxLength.Value;
            }

            LoadDynamicData(prop);
        }

        public void Initialize(string name, string type = "string", string display = null)
        {
            Name = name;
            Display.Name = (Display.Name == name || string.IsNullOrWhiteSpace(Display.Name))
                ? (display ?? Name) : Display.Name;
            Type = type;
        }


        public string Validate()
        {
            // todo
            return null;
        }

        //public void LoadDynamicData(EdmProperty prop)
        //{
        //    EdmProperty = prop;
        //}

        public override void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            base.PropertyChangedHandler(sender, e);
            if(e.PropertyName == "Type")
            {
                // 只有 string 才适用 StringLength 规则
                if (Type != "string")
                {
                    StringLength.IsApplicable = false;
                }

                Range.Type = Type;
            }
        }

        #endregion
    }


}
