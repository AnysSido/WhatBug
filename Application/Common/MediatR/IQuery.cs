using MediatR;

namespace WhatBug.Application.Common.MediatR
{
    public interface IQuery<T> : IRequest<T> where T : Response { }
}
