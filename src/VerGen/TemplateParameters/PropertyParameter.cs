using System;
using System.Collections.Generic;

namespace VerGen.TemplateParameters
{

    [Serializable]
    public class PropertyParameter
    {
        public string Name { get; set; }

        public string Accessibility { get; set; }

        public string Type { get; set; }

        public string Display { get; set; }

        public string DefaultValue { get; set; }

        public List<string> Annotations { get; set; }

    }
}