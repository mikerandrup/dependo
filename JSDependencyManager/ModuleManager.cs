using System;
using System.Collections.Generic;

namespace ClientAssetDependencyManager
{
    public static class AssetManager
    {
        public static JSModule Retrieve(string moduleName)
        {
            if (!RegisteredModules.ContainsKey(moduleName))
            {
                throw new ArgumentException(
                    String.Format(
                        "No module named {0} has been registered.", 
                        moduleName
                    )
                );
            }
            return RegisteredModules[moduleName];
        }

        public static void Register(String moduleName, JSModule module)
        {
            if (RegisteredModules.ContainsKey(moduleName))
            {
                throw new ArgumentException(
                    String.Format(
                        "A module named {0} has already been registered. " +
                        "All modules must have unique names",
                        moduleName
                    )
                );
            }
            RegisteredModules[moduleName] = module;
        }

        private static Dictionary<String, JSModule> _registeredModules;
        private static Dictionary<String, JSModule> RegisteredModules
        {
            get
            {
                if (_registeredModules == null)
                    _registeredModules = new Dictionary<string, JSModule>();

                return _registeredModules;
            }
        }

        private static bool _modulesAreFrozen = false;
        /// <summary>
        /// Enables caching of recursive checks within modules by marking them as immutable
        /// </summary>
        public static void FreezeModules()
        {
            if (_modulesAreFrozen)
                throw new InvalidOperationException("All Modules are already Immutable.");

            foreach (KeyValuePair<string, JSModule> entry in RegisteredModules)
            {
                entry.Value.Freeze();
            }
        }
    }
}
