using System.Collections.ObjectModel;
using System.Xml.Serialization;
using VerGen.Enums;
using VerGen.Schema.Models;
using VerGen.Tool.UI.Infrastructure;
using VerGen.Tool.Utilities;

namespace VerGen.Tool.UI.ViewModels
{
    public class CrudUserStoryViewModel : CrudUserStory
    {
        [XmlIgnore]
        public ObservableCollection<SelectListItem> ComponentSelectedList { get; set; }

        public void Initialize()
        {
            //if(ComponentSelectedList == null || ComponentSelectedList.Count == 0)
            //{
            //    ComponentSelectedList =  new ObservableCollection<SelectListItem>(EnumHelper.GetSelectList<ComponentType>());
            //}
        }
    }
}
