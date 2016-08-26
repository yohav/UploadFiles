using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UploadFiles
{
    public static class EnumText
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            object[] attribs = field.GetCustomAttributes(typeof(Description), true);
            if (attribs.Length > 0)
            {
                return ((Description)attribs[0]).Text;
            }
            return string.Empty;
        }
    }
}
