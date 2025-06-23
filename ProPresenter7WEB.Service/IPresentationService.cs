using ProPresenter7WEB.Core;

namespace ProPresenter7WEB.Service
{
    public interface IPresentationService
    {
        Task<Presentation> GetPresentationAsync(string presentationUuid);
    }
}
