using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace ClientAssetDependencyManager
{
    public class JSModule
    {
        private const bool USE_MINIFICATION = true;

        public JSModule(string moduleNameAndPath)
            : this(moduleNameAndPath, moduleNameAndPath)
        { }

        public JSModule(string moduleName, string modulePath)
        {
            _moduleName = moduleName;
            _modulePath = modulePath;
            _moduleVersion = new Version(1, 0);
            _requiredJSModules = new List<JSModule>();
            AssetManager.Register(_moduleName, this);
        }
        public JSModule Requires(JSModule requiredModule)
        {
            if (requiredModule.DependsUpon(this))
                throw new ArgumentException(
                    "Circular Dependencies are Not Allowed " +
                    "(Dependency Digraph is Acyclic!)"
                );

            _requiredJSModules.Add(requiredModule);
            return this;
        }
        private List<JSModule> _requiredJSModules { get; set; }

        public bool DependsUpon(JSModule possibleDependency)
        {
            bool thisModuleDependsOnProvidedModule = false;

            if (_requiredJSModules.Contains(possibleDependency))
            {
                thisModuleDependsOnProvidedModule = true;
            }
            else
            {
                foreach (var module in _requiredJSModules)
                {
                    thisModuleDependsOnProvidedModule =
                        thisModuleDependsOnProvidedModule ||
                        module.DependsUpon(possibleDependency);
                }
            }

            return thisModuleDependsOnProvidedModule;
        }

        public List<JSModule> GetRequiredJSModules()
        {
            var jsModuleList = new List<JSModule>();
            jsModuleList.Add(this);

            foreach (var module in _requiredJSModules)
            {
                jsModuleList.AddRange(module.GetRequiredJSModules());
            }
            return jsModuleList;
        }

        private List<JSModule> _cachedFilteredAndSortedRequiredModules;
        public List<JSModule> GetFilteredAndSortedRequiredJSModules()
        {
            List<JSModule> requiredJSModules = GetRequiredJSModules();
            requiredJSModules = requiredJSModules.Distinct().ToList();
            requiredJSModules.Sort(JSModule.DependsUponSortFunction);
            return requiredJSModules;
        }

        public override string ToString()
        {
            return _moduleName;
        }

        public static int DependsUponSortFunction(JSModule x, JSModule y)
        {
            return x.DependsUpon(y) ? 1 : -1;
        }

        private string _moduleName;
        public string ModuleName { get { return _moduleName; } }

        private string _modulePath { get; set; }

        private Version _moduleVersion;
        public Version ModuleVersion { get { return _moduleVersion; } }

        private bool _isFrozen = false;
        public void Freeze()
        {
            _isFrozen = true;
        }

        // TODO: Make file path implementation not stink
        private const String JSMODULE_BASE_PATH = @"D:\Dev\Dropbox\JSDependencies\JSDependencyManager\managedcontent\javascript\";
        private string _content = null;
        public string Content
        {
            get
            {
                if (_content == null)
                {
                    _content = MinifyJSContent(File.ReadAllText(JSMODULE_BASE_PATH + _modulePath)); 
                }

                return _content;
            }
        }

        private string MinifyJSContent(string rawContent)
        {
            string minifiedContent = rawContent;

            if (USE_MINIFICATION)
            {
                minifiedContent = new Minifier().MinifyJavaScript(rawContent);
            }
            return minifiedContent;
        }
    }
}
