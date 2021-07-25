namespace TheGreatGrape.Web.Areas.Administration.Controllers
{
    using TheGreatGrape.Common;
    using TheGreatGrape.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
