using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Collectors
{
    public class TypeInfoCollector
    {
        private const BindingFlags _flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;
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

            Fields = t.GetFields(_flags).Select(f => new FieldInfoCollector(f) ).ToList();
            Methods = t.GetMethods(_flags).Where(m => !m.IsConstructor).Select(m => new MethodInfoCollector(m)).ToList();
            Properties = t.GetProperties(_flags).Select(p => new PropertyInfoCollector(p)).ToList();
            Constructors = t.GetConstructors(_flags).Select(c => new ConstructorInfoCollector(c)).ToList();  
        }
    }
}
