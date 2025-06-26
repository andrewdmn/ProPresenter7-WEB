using System.Text.Json.Serialization;

namespace ProPresenter7WEB.Service.Contracts
{
    public class PresentationResponse
    {
        [JsonPropertyName("presentation")]
        public required Presentation Presentation { get; set; }
    }
}
