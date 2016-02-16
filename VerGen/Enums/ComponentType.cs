using System.ComponentModel.DataAnnotations;

namespace VerGen.Enums
{
    public enum ComponentType
    {
        [Display(Name = "概念模型")]
        Model,
        [Display(Name = "视图模型")]
        Dto,
        [Display(Name = "模型映射")]
        MapProfile,
        [Display(Name = "业务逻辑层")]
        Service,
        [Display(Name = "接口API层")]
        Controller,
        [Display(Name = "前端UI")]
        Script
    }
}