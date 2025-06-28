using System.Text.Json.Serialization;

namespace ProPresenter7WEB.Service.Contracts
{
    public class PresentationIndex
    {
        [JsonPropertyName("index")]
        public required int Index { get; set; }

        [JsonPropertyName("presentation_id")]
        public required Id PresentationId { get; set; }

    }
}
