namespace WebAPICigam.Services
{
    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {

        }

        public bool StatusOk { get; set; }

        public T Result { get; set; }

        public ServiceResponse(bool statusOk, List<string> errorMessages)         
        {
        }

        public static ServiceResponse<T> Success(T result)
        {
            return new ServiceResponse<T>
            {
                StatusOk = true,
                Result = result
            };
        }

        public static ServiceResponse<T> Failure(string error) => Failure(new List<string> { error });

        public static ServiceResponse<T> Failure(List<string> errors) => new ServiceResponse<T>(statusOk: false, errorMessages: errors);

        public string MessageError { get; set; }

    }
}
