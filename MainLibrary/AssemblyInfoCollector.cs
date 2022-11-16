using MainLibrary.Collectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary
{
    public class AssemblyInfoCollector
    {
        private Dictionary<string, List<TypeInfoCollector>> _namespaceTypes = new Dictionary<string, List<TypeInfoCollector>>();

        private string _path;

        public AssemblyInfoCollector(string pathToDll)
        {
            _path = pathToDll;
            FillAssemblyTypes();
            ProcessExtensionTypes();
        }

        private void FillAssemblyTypes()
        {
            try
            {
                var asm = Assembly.LoadFrom(_path);
                foreach(var type in asm.GetTypes())
                {
                    if (!_namespaceTypes.ContainsKey(type.Namespace) && !type.IsDefined(typeof(CompilerGeneratedAttribute)))
                    {
                        _namespaceTypes[type.Namespace] = new List<TypeInfoCollector>();
                    }
                    if (!type.IsDefined(typeof(CompilerGeneratedAttribute)))
                        _namespaceTypes[type.Namespace].Add(new TypeInfoCollector(type));
                }
            }
            catch (Exception ex)
            {
                _path = null;
            }
           
        }

        private void ProcessExtensionTypes()
        {
            var extensionMethods = CheckForExtensionMethods();
            foreach (var method in extensionMethods)
            {
                var type = method.Parametrs[0].ParameterType;
                AddForExtensionType(type, method);
            }
        }

        private void AddForExtensionType(Type typeInfo, MethodInfoCollector methodCollector)
        {

            if (!_namespaceTypes.ContainsKey(typeInfo.Namespace))
            {
                _namespaceTypes[typeInfo.Namespace] = new List<TypeInfoCollector>();
            }
            var typeCollector = _namespaceTypes[typeInfo.Namespace].Find(t => t.TypeInfo.FullName.Equals(typeInfo.FullName));
            if (typeCollector is null)
            {
                _namespaceTypes[typeInfo.Namespace].Add(new TypeInfoCollector(typeInfo));
            }
            _namespaceTypes[typeInfo.Namespace].Find(t => t.TypeInfo.FullName.Equals(typeInfo.FullName)).Methods.Add(methodCollector);
        }

        private List<MethodInfoCollector> CheckForExtensionMethods()
        {
            var allExtensionMethods = new List<MethodInfoCollector>();

            foreach(var key in _namespaceTypes.Keys)
            {
                var typesForChecking = _namespaceTypes[key];
                int i = 0; 
                while (typesForChecking.Count > i)
                {
                    var extMethods = GetExtensionMethods(typesForChecking[i]);
                    if (extMethods.Count != 0)
                    {
                        allExtensionMethods.AddRange(extMethods);
                        //TO DO Try for find Type of first argument
                        //If not then creating that type in its namespace
                        //typesForChecking.RemoveAt(i);
                    }
                    i++;
                }
            }

            return allExtensionMethods;
        }

        private List<MethodInfoCollector> GetExtensionMethods(TypeInfoCollector typeCollector)
        {

            List<MethodInfoCollector> extensionMethods = new List<MethodInfoCollector>();
            foreach(var method in typeCollector.Methods)
            {
                if (!method.Extension.Equals(""))
                {
                    var copyMethod = method;
                    extensionMethods.Add(method);
                }
                
            }

            return extensionMethods;
        }

        public void ConsoleWrite()
        {
            int step = 1;
            foreach(var key in _namespaceTypes.Keys)
            {
                Console.WriteLine(key);

                    var namespaceTypes = _namespaceTypes[key];
                    step++;
                    foreach(var namespaceType in namespaceTypes)
                    {
                        Console.WriteLine(namespaceType.ToString());
                       // step++;

                        foreach (var field in namespaceType.Fields)
                        {
                            for (int i = 0; i < step; i++)
                            {
                                Console.Write("   ");
                            }
                            Console.WriteLine(field.ToString());
                        }


                        foreach(var constructor in namespaceType.Constructors)
                        {
                            for (int i = 0; i < step; i++)
                            {
                                Console.Write("   ");
                            }
                            Console.WriteLine(constructor.ToString());
                        }


                        foreach (var method in namespaceType.Methods)
                        {
                            for (int i = 0; i < step; i++)
                            {
                                Console.Write("   ");
                            }
                            Console.WriteLine(method.ToString());
                        }

                        foreach (var property in namespaceType.Properties)
                        {
                            for (int i = 0; i < step; i++)
                            {
                                Console.Write("   ");
                            }
                            Console.WriteLine(property.ToString());
                        }
                    }

                    step--;

            }
        }
    }
}
