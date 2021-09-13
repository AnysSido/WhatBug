using MediatR;
using WhatBug.Application.Common.Models;

namespace WhatBug.Application.Common.MediatR
{
    public interface IQuery<T> : IRequest<T> where T : Response { }
}
