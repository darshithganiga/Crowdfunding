namespace Crowdfunding.Exceptions
{
    public class DataBaseException : BaseException
    {
        public DataBaseException(string message) : base(message, StatusCodes.Status500InternalServerError) {
        
        
        }


    }
}
