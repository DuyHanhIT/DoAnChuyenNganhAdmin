using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class BaseJsonResult
    {
        public bool isSuccess { get; set; }
        public int status { get; set; }
        public string Message { get; set; }
        public object data { get; set; }

        public int? numPages { get; set; }

        public BaseJsonResult(bool isSuccess, int status, string message, object data, int? numPages)
        {
            this.isSuccess = isSuccess;
            this.status = status;
            Message = message;
            this.data = data;
            this.numPages = numPages;
        }
    }
}