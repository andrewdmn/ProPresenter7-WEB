using Microsoft.AspNetCore.Mvc;
using ProPresenter7WEB.Core;
using ProPresenter7WEB.Service;

namespace ProPresenter7WEB.WebServerApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlideController : BaseProPresenterController
    {
        private readonly IPresentationService _presentationService;

        public SlideController(
            IProPresenterStorageService proPresenterStorageService,
            IPresentationService presentationService)
            : base (proPresenterStorageService)
        {
            _presentationService = presentationService;
        }

        [HttpGet("Active/Index")]
        public async Task<ActiveSlideIndex?> GetActiveSlideIndex()
        {
            EnsureStorageServiceIsConfigured();

            _presentationService.BaseApiAddress = ProPresenterStorageService.ApiAddress;
            var presentationUuid = ProPresenterStorageService.PresentationUuid;
            
            var presentationActiveIndex = await _presentationService.GetActiveSlideIndexAsync();
            return presentationActiveIndex?.PresentationUuid == presentationUuid ? presentationActiveIndex : null;
        }
    }
}
