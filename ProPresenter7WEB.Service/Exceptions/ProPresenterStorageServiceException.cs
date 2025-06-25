namespace ProPresenter7WEB.Service.Exceptions
{
    public class ProPresenterStorageServiceException : Exception
    {
        // TODO: Catch exceptions of this type on middleware layer and wrap them into BadRequestExceptions
        public ProPresenterStorageServiceException(string message)
            : base (message)
        {
        }
    }
}
