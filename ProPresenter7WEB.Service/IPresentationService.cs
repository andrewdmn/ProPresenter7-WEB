using ProPresenter7WEB.Core;

namespace ProPresenter7WEB.Service
{
    public interface IPresentationService
    {
        string? BaseApiAddress { get; set; }

        Task<Presentation> GetPresentationAsync(string presentationUuid);

        Task<WebImage> GetSlideImageAsync(string presentationUuid, int slideIndex);

        Task TriggerNextSlideAsync(string presentationUuid);

        Task TriggerPreviousSlideAsync(string presentationUuid);

        Task TriggerSlideAsync(string presentationUuid, int slideIndex);

        Task FocusPresentationAsync(string presentationUuid);

        Task<ActiveSlideIndex> GetActiveSlideIndexAsync();
    }
}
