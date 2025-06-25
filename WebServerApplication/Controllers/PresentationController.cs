using Microsoft.AspNetCore.Mvc;
using ProPresenter7WEB.Core;
using ProPresenter7WEB.Service;

namespace ProPresenter7WEB.WebServerApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresentationController : ControllerBase
    {
        private readonly IPresentationService _presentationService;
        private readonly IPresentationStorageService _presentationStorageService;

        public PresentationController(
            IPresentationService presentationService,
            IPresentationStorageService presentationStorageService)
        {
            _presentationService = presentationService;
            _presentationStorageService = presentationStorageService;
        }

        [HttpGet]
        public async Task<Presentation> Get()
        {
            var presentationUuid = _presentationStorageService.GetPresentationUuid();

            if (presentationUuid == null)
            {
                throw new BadHttpRequestException("Presentation is not selected on the desktop application.");
            }

            return await _presentationService.GetPresentationAsync(presentationUuid);
        }
    }
}
