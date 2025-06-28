using Microsoft.AspNetCore.Mvc;
using ProPresenter7WEB.Core;
using ProPresenter7WEB.Service;

namespace ProPresenter7WEB.WebServerApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresentationController : BaseProPresenterController
    {
        private readonly IPresentationService _presentationService;

        public PresentationController(
            IPresentationService presentationService,
            IProPresenterStorageService proPresenterStorageService)
            : base (proPresenterStorageService)
        {
            _presentationService = presentationService;
        }

        [HttpGet]
        public async Task<Presentation> Get()
        {
            EnsureStorageServiceIsConfigured();

            _presentationService.BaseApiAddress = ProPresenterStorageService.ApiAddress;
            var presentationUuid = ProPresenterStorageService.PresentationUuid;

            var presentation = await _presentationService.GetPresentationAsync(presentationUuid);
            return presentation;
        }
    }
}
