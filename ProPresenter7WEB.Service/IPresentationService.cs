using ProPresenter7WEB.Core;

namespace ProPresenter7WEB.Service
{
    public interface IPresentationService
    {
        string? BaseApiAddress { get; set; }

        Task<Presentation> GetPresentationAsync(string presentationUuid);
    }
}
