namespace WhatBug.Application.Common.Security
{
    public interface IRequireAuthorization
    {
    }

    public interface IRequireProjectAuthorization : IRequireAuthorization
    {
        int ProjectId { get; }
    }

    public interface IRequireIssueAuthorization : IRequireAuthorization
    {
        string IssueId { get; }
    }
}
