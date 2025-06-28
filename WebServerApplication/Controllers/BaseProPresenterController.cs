using Microsoft.AspNetCore.Mvc;
using ProPresenter7WEB.Service;
using ProPresenter7WEB.Service.Exceptions;

namespace ProPresenter7WEB.WebServerApplication.Controllers
{
    public abstract class BaseProPresenterController : ControllerBase
    {
        private readonly IProPresenterStorageService _proPresenterStorageService;

        protected BaseProPresenterController(IProPresenterStorageService proPresenterStorageService)
        {
            _proPresenterStorageService = proPresenterStorageService;
        }

        protected IProPresenterStorageService ProPresenterStorageService => _proPresenterStorageService;

        protected void EnsureStorageServiceIsConfigured()
        {
            if (_proPresenterStorageService.ApiAddress is null)
                throw new ProPresenterApiAddressNotSetException();

            if (_proPresenterStorageService.PresentationUuid is null)
                throw new ProPresenterApiServiceException("Presentation is not selected.");
        }
    }
}
