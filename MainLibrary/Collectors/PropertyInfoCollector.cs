using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Collectors
{
    public class PropertyInfoCollector
    {
        public string ModificatorGet { get; } = ""; 

        public string ModificatorSet { get; } = "";

        public string ReturnTypeName { get; }

        public string Name { get; }

        public PropertyInfoCollector(PropertyInfo property)
        {
            ReturnTypeName = TypesConverter.ChangeTypeName(property.PropertyType.Name) + " ";
            Name = property.Name + " ";
            if (property.CanRead)
            {
                var method = property.GetGetMethod();   
                if (method.IsPublic)
                {
                    ModificatorGet = "public ";
                }
                else if (method.IsPrivate)
                {
                    ModificatorGet = "private ";
                }
            }
            else
            {
                ModificatorGet = "";
            }
            if (property.CanWrite)
            {
                var method = property.GetGetMethod();
                if (method.IsPublic)
                {
                    ModificatorSet = "public ";
                }
                else if (method.IsPrivate)
                {
                    ModificatorSet = "private ";
                }
            }
            else
            {
                ModificatorSet = "";
            }
            
        }

        public override string ToString()
        {
            var result = ReturnTypeName + Name + "{";
            if (!ModificatorGet.Equals(""))
            {
                result += ModificatorGet + "get; ";
            }
            if (!ModificatorSet.Equals(""))
            {
                result += ModificatorSet + "set; ";
            }
            result += "}";
            return result; 
        }

       
    }
}
