
namespace Core.CommonModel.Result
{

    public class ServiceResult
    { 
        public bool Status { get; set; } = true;
        public ResultCode StatusCode { get; set; }   = ResultCode.Success;      
        public string Message { get; set; } = "Operation is success!";

        public ServiceResult Success(string _message)
        {
            Status = true;
            Message = _message;
            return this;
        }
    }
    public class ServiceResult<T> 
    {
        public bool Status { get; set; } = true;
        public ResultCode StatusCode { get; set; } = ResultCode.Success;    
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
