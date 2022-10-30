using System;

namespace EmployeeMCrud.Models
{   
    public class StatusResult<T>
    {
        public StatusResult(){
            Status = "FAILED";
        }
        public string Status;
        public string Message;
        public T Result;
    }
}

