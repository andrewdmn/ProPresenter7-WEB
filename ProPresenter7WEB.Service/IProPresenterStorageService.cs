using ProPresenter7WEB.Core;

namespace ProPresenter7WEB.Service
{
    public interface IProPresenterStorageService
    {
        string PresentationUuid { get; set; }

        void RemovePresentationUuid();

        string ApiAddress { get; }

        void SetApiAddress(string address, int port);
    }
}
