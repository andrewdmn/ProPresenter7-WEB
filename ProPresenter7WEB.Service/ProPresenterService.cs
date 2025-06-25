namespace ProPresenter7WEB.Service
{
    public class ProPresenterService : IProPresenterService
    {
        // TODO: It is possible that ApiAddress can be null when calling the API
        // for example on desktop app connection was canceled.
        // In such cases I'd like to notify web ui with a corresponding exception like "Connection to ProPresenter is not established."
        // Perhaps this check should be done on middleware level...
        public string? ApiAddress { get; private set; }

        public void SetApiAddress(string address, int port)
        {
            ApiAddress = $"http://{address}:{port}";
        }
    }
}
