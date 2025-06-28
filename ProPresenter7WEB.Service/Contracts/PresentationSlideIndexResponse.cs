using System.Text.Json.Serialization;

namespace ProPresenter7WEB.Service.Contracts
{
    public class PresentationSlideIndexResponse
    {
        [JsonPropertyName("presentation_index")]
        public PresentationIndex? PresentationIndex { get; set; }
    }
}
