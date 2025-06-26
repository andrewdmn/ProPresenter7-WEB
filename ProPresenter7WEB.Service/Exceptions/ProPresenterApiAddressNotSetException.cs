namespace ProPresenter7WEB.Service.Exceptions
{
    public class ProPresenterApiAddressNotSetException : ProPresenterApiServiceException
    {
        public ProPresenterApiAddressNotSetException()
            : base("ProPresenter API address is not set.")
        {
        }
    }
}
