using System.Text.Json.Serialization;

namespace ProPresenter7WEB.Service.Contracts
{
    public class Group
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("slides")]
        public required Slide[] Slides { get; set; }
    }
}
