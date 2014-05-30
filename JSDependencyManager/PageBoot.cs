using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ClientAssetDependencyManager
{
    public static class PageBoot
    {
        // NOTE: In real implementation, path will be handled differently (embedded resource in DLL, content in a web project, or relative path)
        private const String PATH_TO_PAGEBOOT_FOLDER = @"D:\Dev\Dropbox\JSDependencies\JSDependencyManager\pageboot\";

        public static IHtmlString GetJavaScript()
        {
            return new HtmlString(File.ReadAllText(PATH_TO_PAGEBOOT_FOLDER + "pageboot.js"));
        }
        public static IHtmlString GetStyleSheet()
        {
            return new HtmlString(File.ReadAllText(PATH_TO_PAGEBOOT_FOLDER + "pageboot.css"));
        }
    }
}