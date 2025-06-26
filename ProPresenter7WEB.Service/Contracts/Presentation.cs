using System.Text.Json.Serialization;

namespace ProPresenter7WEB.Service.Contracts
{
    public class Presentation
    {
        [JsonPropertyName("id")]
        public required Id Id { get; set; }

        [JsonPropertyName("groups")]
        public required Group[] Groups { get; set; }
    }
}
