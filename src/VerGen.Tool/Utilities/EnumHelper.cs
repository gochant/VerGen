using System;
using System.Collections.Generic;
using System.Linq;
using VerGen.Tool.Extensions;
using VerGen.Tool.UI.Infrastructure;

namespace VerGen.Tool.Utilities
{
    public class EnumHelper
    {
        public static List<string> GetDisplays<T>() where T : struct
        {
            var t = typeof(T);
            return !t.IsEnum ? null : Enum.GetValues(t).Cast<Enum>().Select(x => x.GetDisplay()).ToList();
        }

        public static List<string> GetDescriptions<T>() where T : struct
        {
            var t = typeof(T);
            return !t.IsEnum ? null : Enum.GetValues(t).Cast<Enum>().Select(x => x.GetDescription()).ToList();
        }

        public static IEnumerable<SelectListItem> GetSelectList<T>(Func<Enum, string> textSelector = null, Func<Enum, string> valueSelector = null)
        {
            var t = typeof(T);
            if (textSelector == null) textSelector = (x) => x.GetDisplay();
            if (valueSelector == null) valueSelector = (x) => Convert.ToInt32(x).ToString();

            var result = !t.IsEnum ? new List<SelectListItem>() : Enum.GetValues(t).Cast<Enum>()
                .Select(x => new SelectListItem
                {
                    Text = textSelector(x),
                    Value = valueSelector(x)
                }).ToList();
            return result;
        }

        public static T Parse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}