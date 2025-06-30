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

        [HttpGet("{presentationUuid}/{slideIndex}/Image")]
        public async Task<IActionResult> GetSlideImage(string presentationUuid, int slideIndex)
        {
            EnsureStorageServiceIsConfigured();
            ValidatePresentationUuid(presentationUuid);

            _presentationService.BaseApiAddress = ProPresenterStorageService.ApiAddress;

            var slideImage = await _presentationService.GetSlideImageAsync(presentationUuid, slideIndex);

            return File(slideImage.Content, slideImage.ContentType);
        }

        [HttpGet("{presentationUuid}/Next/Trigger")]
        public async Task TriggerNextSlide(string presentationUuid)
        {
            EnsureStorageServiceIsConfigured();
            ValidatePresentationUuid(presentationUuid);

            _presentationService.BaseApiAddress = ProPresenterStorageService.ApiAddress;

            await _presentationService.FocusPresentationAsync(presentationUuid);
            await _presentationService.TriggerNextSlideAsync(presentationUuid);
        }

        [HttpGet("{presentationUuid}/Previous/Trigger")]
        public async Task TriggerPreviousSlide(string presentationUuid)
        {
            EnsureStorageServiceIsConfigured();
            ValidatePresentationUuid(presentationUuid);

            _presentationService.BaseApiAddress = ProPresenterStorageService.ApiAddress;

            await _presentationService.FocusPresentationAsync(presentationUuid);
            await _presentationService.TriggerPreviousSlideAsync(presentationUuid);
        }

        [HttpGet("{presentationUuid}/{slideIndex}/Trigger")]
        public async Task TriggerSlide(string presentationUuid, int slideIndex)
        {
            EnsureStorageServiceIsConfigured();
            ValidatePresentationUuid(presentationUuid);

            _presentationService.BaseApiAddress = ProPresenterStorageService.ApiAddress;

            await _presentationService.FocusPresentationAsync(presentationUuid);
            await _presentationService.TriggerSlideAsync(presentationUuid, slideIndex);
        }

        private void ValidatePresentationUuid(string presentationUuid)
        {
            if (presentationUuid != ProPresenterStorageService.PresentationUuid)
            {
                throw new BadHttpRequestException($"Presentation with uuid: {presentationUuid} doesn't exist or is not selected.");
            }
        }
    }
}
