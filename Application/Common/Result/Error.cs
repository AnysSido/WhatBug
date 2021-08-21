namespace WhatBug.Application.Common.Result
{
    public sealed class Error
    {
        public string Code { get; }
        public string Message { get; set; }

        internal Error(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
