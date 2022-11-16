using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Collectors
{
    public static class TypesConverter
    {
        private static string[] _systemTypes = new string[] {
                "Boolean",
                "Byte",
                "SByte",
                "Char",
                "Decimal",
                "Double",
                "Single",
                "Int32",
                "UInt32",
                "IntPtr",
                "UIntPtr",
                "Int64",
                "UInt64",
                "Int16",
                "UInt16",
                "Object",
                "String"
            };



        private static string[] _types = new string[]
            {
                "bool",
                "byte",
                "sbyte",
                "char",
                "decimal",
                "double",
                "float",
                "int",
                "uint",
                "nint",
                "nuint",
                "long",
                "ulong",
                "short",
                "ushort",
                "object",
                "string"
            };


        public static string ChangeTypeName(string typeName)
        {
            if (typeName.Equals("List`1"))
            {
                Console.WriteLine();
            }
            var fullIndex = Array.FindIndex(_systemTypes, t => typeName.Contains(t));
            if (fullIndex != -1)
            {
                typeName = typeName.Replace(_systemTypes[fullIndex], _types[fullIndex]);
            }

            return typeName;
        }

        public static string ChangeGenericName(string typeName)
        {
            var fullListName = "System.Collection.Generic.List`1";
            var shortListName = "List";
            int times = 0;
            if (typeName.Contains(fullListName))
            {
                times++;
                typeName = typeName.Replace(fullListName, shortListName);
            }
            if (typeName.Contains("["))
            {
                times++;
                typeName = typeName.Replace("[", "<");
            }
            if (typeName.Contains("]"))
            {
                times++;
                typeName = typeName.Replace("]", ">");
            }
            typeName = ChangeTypeName(typeName);

            return typeName;
        }
        

    }
}
