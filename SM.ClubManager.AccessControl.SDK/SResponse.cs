using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.SDK
{
    public class SResponse
    {
		private ErrorCode _code;

		public ErrorCode Code
		{
			get { return _code; }	
            internal set { _code = value; }
		}

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            internal set { _errorMessage = value; }
        }

        internal static SResponse GetResponseObject(ErrorCode errorCode = ErrorCode.Success, string errorMessage = "" )
		{
            SResponse response = new SResponse();
            response.Code = errorCode;
            response.ErrorMessage = errorMessage;

            return response;
		}

		internal SResponse() 
        {
            _errorMessage = "";
            _code = ErrorCode.Unknown;
        }
    }
}
