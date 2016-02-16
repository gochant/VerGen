namespace VerGen.TemplateParameters
{
    public interface IControllerParameter
    {
        string ControllerNamespace { get; set; }
        string EntitySetName { get; set; }
        string EditDtoName { get; set; }
        string ItemDtoName { get; set; }

        string DtoNamespace { get; set; }

    }
}