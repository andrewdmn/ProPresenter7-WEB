using Microsoft.AspNetCore.Mvc;
using ProPresenter7WEB.Service;
using ProPresenter7WEB.Service.Exceptions;

namespace ProPresenter7WEB.WebServerApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlideController : ControllerBase
    {
        private readonly IProPresenterStorageService _proPresenterStorageService;
        private readonly IPresentationService _presentationService;

        public SlideController(
            IProPresenterStorageService proPresenterStorageService,
            IPresentationService presentationService)
        {
            _proPresenterStorageService = proPresenterStorageService;
            _presentationService = presentationService;
        }

        [HttpGet("Next/Trigger")]
        public async Task TriggerNextSlide()
        {
            ValidateStorageServiceParams();

            _presentationService.BaseApiAddress = _proPresenterStorageService.ApiAddress;

            await _presentationService.TriggerNextSlideAsync(_proPresenterStorageService.PresentationUuid);
        }

        [HttpGet("Previous/Trigger")]
        public async Task TriggerPreviousSlide()
        {
            ValidateStorageServiceParams();

            _presentationService.BaseApiAddress = _proPresenterStorageService.ApiAddress;

            await _presentationService.TriggerPreviousSlideAsync(_proPresenterStorageService.PresentationUuid);
        }

        [HttpGet("{slideIndex}/Trigger")]
        public async Task TriggerSlide(int slideIndex)
        {
            ValidateStorageServiceParams();

            _presentationService.BaseApiAddress = _proPresenterStorageService.ApiAddress;

            await _presentationService.TriggerSlideAsync(_proPresenterStorageService.PresentationUuid, slideIndex);
        }

        private void ValidateStorageServiceParams()
        {
            if (_proPresenterStorageService.ApiAddress is null)
                throw new ProPresenterApiAddressNotSetException();

            if (_proPresenterStorageService.PresentationUuid is null)
                throw new ProPresenterApiServiceException("Presentation is not selected.");
        }
    }
}
