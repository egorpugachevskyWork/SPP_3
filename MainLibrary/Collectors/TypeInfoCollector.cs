using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Collectors
{
    public class TypeInfoCollector
    {
        private const BindingFlags _flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;// | BindingFlags.DeclaredOnly;
        public string Name { get; }

        public Type TypeInfo { get; }

        public List<FieldInfoCollector> Fields { get; }

        public List<MethodInfoCollector> Methods { get; }

        public List<PropertyInfoCollector> Properties { get; }

        public List<ConstructorInfoCollector> Constructors { get; }

        public TypeInfoCollector(Type t)
        {
            TypeInfo = t;
            Name = t.Name;

            Fields = t.GetFields(_flags).Where(f => !f.IsDefined(typeof(CompilerGeneratedAttribute))).Select(f => new FieldInfoCollector(f) ).ToList();
            Methods = t.GetMethods(_flags).Where(m => !m.IsConstructor && !m.IsDefined(typeof(CompilerGeneratedAttribute))).Select(m => new MethodInfoCollector(m)).ToList();
            Properties = t.GetProperties(_flags).Where(p => !p.IsDefined(typeof(CompilerGeneratedAttribute))).Select(p => new PropertyInfoCollector(p)).ToList();
            Constructors = t.GetConstructors(_flags).Where(c => !c.IsDefined(typeof(CompilerGeneratedAttribute))).Select(c => new ConstructorInfoCollector(c)).ToList();  
        }

        public void AddExtensionMethods(List<MethodInfoCollector> methods)
        {
            Methods.AddRange(methods);
        }

        public override string ToString()
        {
            
            var modificator = TypeInfo.IsPublic ? "public " : "non-public ";
            var result = modificator;
            
            if (TypeInfo.IsInterface)
            {
                result += "interface ";
            }
            else if (TypeInfo.IsAbstract)
            {
                result += "abstract class";

            }
            else
            {
                result += "class ";
            }
            
            result += Name;
            return result;
        }
    }
}
