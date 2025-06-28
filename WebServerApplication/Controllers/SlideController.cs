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

        [HttpGet("{slideIndex}/Image")]
        public async Task<IActionResult> GetSlideImage(int slideIndex)
        {
            EnsureStorageServiceIsConfigured();

            _presentationService.BaseApiAddress = ProPresenterStorageService.ApiAddress;
            var presentationUuid = ProPresenterStorageService.PresentationUuid;

            var slideImage = await _presentationService.GetSlideImageAsync(presentationUuid, slideIndex);

            return File(slideImage.Content, slideImage.ContentType);
        }

        [HttpGet("Next/Trigger")]
        public async Task TriggerNextSlide()
        {
            EnsureStorageServiceIsConfigured();

            _presentationService.BaseApiAddress = ProPresenterStorageService.ApiAddress;
            var presentationUuid = ProPresenterStorageService.PresentationUuid;

            await _presentationService.FocusPresentationAsync(presentationUuid);
            await _presentationService.TriggerNextSlideAsync(presentationUuid);
        }

        [HttpGet("Previous/Trigger")]
        public async Task TriggerPreviousSlide()
        {
            EnsureStorageServiceIsConfigured();

            _presentationService.BaseApiAddress = ProPresenterStorageService.ApiAddress;
            var presentationUuid = ProPresenterStorageService.PresentationUuid;

            await _presentationService.FocusPresentationAsync(presentationUuid);
            await _presentationService.TriggerPreviousSlideAsync(presentationUuid);
        }

        [HttpGet("{slideIndex}/Trigger")]
        public async Task TriggerSlide(int slideIndex)
        {
            EnsureStorageServiceIsConfigured();

            _presentationService.BaseApiAddress = ProPresenterStorageService.ApiAddress;
            var presentationUuid = ProPresenterStorageService.PresentationUuid;

            await _presentationService.FocusPresentationAsync(presentationUuid);
            await _presentationService.TriggerSlideAsync(presentationUuid, slideIndex);
        }

        [HttpGet("Active/Index")]
        public async Task<ActiveSlideIndex?> GetActiveSlideIndex()
        {
            EnsureStorageServiceIsConfigured();

            _presentationService.BaseApiAddress = ProPresenterStorageService.ApiAddress;
            var presentationUuid = ProPresenterStorageService.PresentationUuid;
            
            var presentationActiveIndex = await _presentationService.GetActiveSlideIndexAsync();
            return presentationActiveIndex.PresentationUuid == presentationUuid ? presentationActiveIndex : null;
        }
    }
}
