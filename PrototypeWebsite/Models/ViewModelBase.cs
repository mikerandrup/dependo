using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ClientAssetDependencyManager;

namespace PrototypeWebsite.Models
{
    public class ViewModelBase
    {
        public string JSModuleName { get; set; }

        public string JavascriptDependenciesToJSON()
        {
            string jsonString;

            if (String.IsNullOrWhiteSpace(JSModuleName))
            {
                jsonString = String.Empty;
            }
            else {
                List<JSModule> moduleList =
                    AssetManager
                    .Retrieve(JSModuleName)
                    .GetFilteredAndSortedRequiredJSModules();

                int sequenceIndex = 0;
                var moduleInfoList = new List<Object>();
                foreach (var module in moduleList)
                {
                    moduleInfoList.Add(
                        new TransportModel()
                        {
                            name = module.ModuleName,
                            version = module.ModuleVersion.ToString(),
                            content = String.Empty,
                            sequence = sequenceIndex++
                        }
                     );
                }

                jsonString = new JavaScriptSerializer().Serialize(moduleInfoList);
            }

            return jsonString;
        }
    }
}