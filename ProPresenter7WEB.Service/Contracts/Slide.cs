using System.Text.Json.Serialization;

namespace ProPresenter7WEB.Service.Contracts
{
    public class Slide
    {
        [JsonPropertyName("label")]
        public required string Label { get; set; }

        [JsonPropertyName("enabled")]
        public required bool Enabled { get; set; }
    }
}
