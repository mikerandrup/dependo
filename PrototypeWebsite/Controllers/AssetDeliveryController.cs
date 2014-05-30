using System.Collections.Generic;
using System.Web.Mvc;
using ClientAssetDependencyManager;

namespace PrototypeWebsite.Controllers
{
    public class AssetDeliveryController : Controller
    {
        public ActionResult JavaScript(List<TransportModel> neededModules)
        {
            foreach (TransportModel model in neededModules)
            {
                model.content = AssetManager.Retrieve(model.name).Content;
            }
            return Json(neededModules);
        }
    }
}
