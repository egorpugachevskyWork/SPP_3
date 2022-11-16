using MainLibrary.Collectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                    if (!_namespaceTypes.ContainsKey(type.Namespace))
                    {
                        _namespaceTypes[type.Namespace] = new List<TypeInfoCollector>();
                    }
                    _namespaceTypes[type.Namespace].Add(new TypeInfoCollector(type));
                }
            }
            catch (Exception ex)
            {
                _path = null;
            }
           
        }
    }
}
