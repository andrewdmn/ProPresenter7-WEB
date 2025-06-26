using Microsoft.AspNetCore.Mvc;
using ProPresenter7WEB.Core;
using ProPresenter7WEB.Service;
using ProPresenter7WEB.Service.Exceptions;

namespace ProPresenter7WEB.WebServerApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresentationController : ControllerBase
    {
        private readonly IPresentationService _presentationService;
        private readonly IProPresenterStorageService _proPresenterStorageService;

        public PresentationController(
            IPresentationService presentationService,
            IProPresenterStorageService proPresenterStorageService)
        {
            _presentationService = presentationService;
            _proPresenterStorageService = proPresenterStorageService;
        }

        [HttpGet]
        public async Task<Presentation> Get()
        {
            if (_proPresenterStorageService.ApiAddress is null)
                throw new ProPresenterApiAddressNotSetException();

            if (_proPresenterStorageService.PresentationUuid is null)
                throw new ProPresenterApiServiceException("Presentation is not selected.");

            var presentationUuid = _proPresenterStorageService.PresentationUuid;
            _presentationService.BaseApiAddress = _proPresenterStorageService.ApiAddress;

            return await _presentationService.GetPresentationAsync(presentationUuid);
        }
    }
}
