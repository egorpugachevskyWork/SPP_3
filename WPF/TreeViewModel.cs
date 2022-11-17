using MainLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF
{
    public class TreeViewModel : INotifyPropertyChanged
    {
        private Node _selectedNode;

        private CustomCommand _openFileCommand;

        public ObservableCollection<Node> Namespaces { 
            get { return _namespace; }
            set
            {
                _namespace = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Node> _namespace;
        public Node SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                _selectedNode = value;
                OnPropertyChanged("SelectedNode");
            }
        }

            
        public CustomCommand OpenDllCommand
        {
            get
            {
                return _openFileCommand ??= new CustomCommand(obj =>
                {
                    try
                    {
                        var openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "dll files(*.dll) |*.dll| All files(*.*) |*.*";
                        openFileDialog.ShowDialog();
                        var path = openFileDialog.FileName;
                        ManageTreeData(path);

                    }

                    catch (Exception ex)
                    {

                    }

                });
            }
        }

        private void ManageTreeData(string path)
        {
            var asmCollector = new AssemblyInfoCollector(path);
            var Nodes = new ObservableCollection<Node>();
            var namespaces = asmCollector.Namespaces;
            foreach (var data in namespaces.Keys)
            {
                var node  = new Node(); 
                node.Data = data;
                foreach(var type in namespaces[data])
                {
                    var typeNode = new Node();

                    typeNode.Data = type.ToString();

                    foreach(var field in type.Fields)
                    {
                        var fieldNodeData = new Node();
                        fieldNodeData.Data = field.ToString();
                        typeNode.Nodes.Add(fieldNodeData);
                    }
                    foreach (var method in type.Methods)
                    {
                        var methodNodeData = new Node();
                        methodNodeData.Data = method.ToString();
                        typeNode.Nodes.Add(methodNodeData);
                    }
                    foreach (var property in type.Properties)
                    {
                        var propertyNodeData = new Node();
                        propertyNodeData.Data = property.ToString();
                        typeNode.Nodes.Add(propertyNodeData);
                    }
                    foreach (var constructor in type.Constructors)
                    {
                        var constructorNodeData = new Node();
                        constructorNodeData.Data = constructor.ToString();
                        typeNode.Nodes.Add(constructorNodeData);
                    }
                    node.Nodes.Add(typeNode);

                }
                Nodes.Add(node);
            }
            Namespaces = Nodes;
            
        }

        public TreeViewModel() { }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
