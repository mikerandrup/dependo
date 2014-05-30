using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAssetDependencyManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientAssetDefinitions.Define();

            JSModule treePage = AssetManager.Retrieve("page-trees");

            List<JSModule> requiredJSModules = treePage.GetFilteredAndSortedRequiredJSModules();

            var sb = new StringBuilder();
            foreach (JSModule module in requiredJSModules) {
                sb.AppendLine(module.ToString());
            }
            Console.WriteLine(sb.ToString());
        }

    }
}
