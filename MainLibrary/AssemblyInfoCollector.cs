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
