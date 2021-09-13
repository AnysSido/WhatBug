using MediatR;

namespace WhatBug.Application.Common.MediatR
{
    public interface ICommand<T> : IRequest<T> where T : Response { }
}
