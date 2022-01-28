using System;

namespace WhatBug.Application.Common.Exceptions
{

    [Serializable]
    public class InvalidAttachmentException : Exception
    {
        public InvalidAttachmentException() { }
        public InvalidAttachmentException(string message) : base(message) { }
        public InvalidAttachmentException(string message, Exception inner) : base(message, inner) { }
        protected InvalidAttachmentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
