using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdminShoesStoreDACN.Models
{
    public class BaseResult
    {
        public bool isSuccess { get; set; }
        public int status { get; set; }
        public string Message { get; set; }
        public object data { get; set; }


       

        public BaseResult(bool isSuccess, int status, string message, object data)
        {
            this.isSuccess = isSuccess;
            this.status = status;
            Message = message;
            this.data = data;
        }


        /*public BaseResult(bool isSuccess, int status, string Message, object data)
        {
            this.isSuccess = isSuccess;
            this.status = status;
            this.Message = Message;
            this.data = data;
        }*/
    }
}