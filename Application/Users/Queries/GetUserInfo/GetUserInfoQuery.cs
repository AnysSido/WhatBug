using MediatR;

namespace WhatBug.Application.Users.Queries.GetUserInfo
{
    public class GetUserInfoQuery : IRequest<UserInfoDTO>
    {
        public int UserId { get; set; }
    }
}
