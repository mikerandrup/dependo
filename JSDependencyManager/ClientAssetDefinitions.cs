using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAssetDependencyManager
{
    public static class ClientAssetDefinitions
    {
        public static void Define()
        {
            // TODO: find a better way to have universal 'first' dependency
            var baseModule = new JSModule("base", "base.js");

            var underScore = new JSModule("underscore", "lib/underscore.js")
                .Requires(baseModule);

            var jQuery = new JSModule("jquery", "lib/jquery.js")
                .Requires(baseModule);

            var seeds = new JSModule("seeds", "modules/seeds.js")
                .Requires(underScore);

            var stem = new JSModule("stem", "modules/stem.js")
                .Requires(jQuery);

            var peel = new JSModule("peel", "modules/peel.js")
                .Requires(underScore);

            var wasp = new JSModule("wasp", "modules/wasp.js")
                .Requires(jQuery);

            var apple = new JSModule("apple", "modules/apple.js")
                .Requires(peel)
                .Requires(seeds);

            var grapes = new JSModule("grapes", "modules/grapes.js")
                .Requires(stem)
                .Requires(seeds);

            var fig = new JSModule("fig", "modules/fig.js")
                .Requires(seeds)
                .Requires(wasp);

            var orange = new JSModule("orange", "modules/orange.js")
                .Requires(peel)
                .Requires(stem);

            var fruitPage = new JSModule("page-fruits", "pages/fruits.js")
                .Requires(apple)
                .Requires(orange)
                .Requires(grapes);

            var treePage = new JSModule("page-trees", "pages/trees.js")
                .Requires(apple)
                .Requires(orange)
                .Requires(fig);

            AssetManager.FreezeModules();
        }
    }
}
