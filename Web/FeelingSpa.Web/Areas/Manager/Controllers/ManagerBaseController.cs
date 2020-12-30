namespace FeelingSpa.Web.Areas.Manager.Controllers
{
    using System.Web.Mvc;

    using FeelingSpa.Common;
    using FeelingSpa.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Manager")]
    [Area("Manager")]
    public class ManagerBaseController : BaseController
    {
    }
}
