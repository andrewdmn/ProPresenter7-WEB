using ProPresenter7WEB.Service.Exceptions;

namespace ProPresenter7WEB.Service
{
    public class ProPresenterStorageService : IProPresenterStorageService
    {
        private string? _apiAddress;
        private string? _presentationUuid;

        public string PresentationUuid
        {
            get
            {
                if (_presentationUuid == null)
                {
                    throw new ProPresenterStorageServiceException("The presentation is not chosen. Please contact an operator.");
                }

                return _presentationUuid;
            }

            set
            {
                _presentationUuid = value;
            }
        }

        public void RemovePresentationUuid()
        {
            _presentationUuid = null;
        }

        public string ApiAddress
        {
            get
            {
                if (_apiAddress == null)
                {
                    throw new ProPresenterStorageServiceException("ProPresenter API address is not set.");
                }

                return _apiAddress;
            }
        }

        public void SetApiAddress(string address, int port)
        {
            _apiAddress = $"http://{address}:{port}";
        }
    }
}
