using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.SDK
{
    public class SSResponse
    {
		private ResponseCode _responsecode;

		public ResponseCode ResponseCode
		{
			get { return _responsecode; }	
            internal set { _responsecode = value; }
		}

        private string? _message;

        public string Message
        {
            get { return _message; }
            internal set { _message = value; }
        }

        public static SSResponse Create(ResponseCode errorCode = ResponseCode.Success, string message = "" )
		{
            SSResponse response = new SSResponse();
            response.ResponseCode = errorCode;
            response.Message = message;

            return response;
		}
    }
}
