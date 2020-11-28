namespace FeelingSpa.Web.Areas.Administration.Controllers
{
    using FeelingSpa.Common;
    using FeelingSpa.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
