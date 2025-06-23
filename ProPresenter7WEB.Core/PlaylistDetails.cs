namespace ProPresenter7WEB.Core
{
    public class PlaylistDetails
    {
        public required string Uuid { get; set; }

        public required string Name { get; set; }

        public required IEnumerable<PresentationItem> Presentations { get; set; }
    }
}
