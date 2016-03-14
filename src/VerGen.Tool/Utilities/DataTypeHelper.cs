using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VerGen.Tool.UI.Infrastructure;

namespace VerGen.Tool.Utilities
{
    public class DataTypeHelper
    {
        public static ObservableCollection<SelectListItem> GetDataTypeList()
        {
            var data = new List<SelectListItem>
            {
                new SelectListItem {Text = "日期时间", Value = "DateTime" },
                new SelectListItem {Text = "日期", Value = "Date" },
                new SelectListItem {Text = "时间", Value = "Time" },
                new SelectListItem {Text = "期间", Value = "Duration" },
                new SelectListItem {Text = "电话号码", Value = "PhoneNumber" },
                new SelectListItem {Text = "货币", Value = "Currency" },
                new SelectListItem {Text = "文本", Value = "Text" },
                new SelectListItem {Text = "HTML", Value = "Html" },
                new SelectListItem {Text = "多行文本", Value = "MultilineText" },
                new SelectListItem {Text = "邮箱地址", Value = "EmailAddress" },
                new SelectListItem {Text = "密码", Value = "Password" },
                new SelectListItem {Text = "网址", Value = "Url" },
                new SelectListItem {Text = "图片地址", Value = "ImageUrl" },
                new SelectListItem {Text = "信用卡", Value = "CreditCard" },
                new SelectListItem {Text = "邮政编码", Value = "PostalCode" },
                new SelectListItem {Text = "上传", Value = "Upload" },
                new SelectListItem {Text = "自定义", Value = "Custom" }
            };
            return new ObservableCollection<SelectListItem>(data.Select(d => new SelectListItem
            {
                Text = $"{d.Text}({d.Value})",
                Value = d.Value
            }));
        }
    }
}