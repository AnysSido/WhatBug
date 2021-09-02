using System;
using System.Collections.Generic;

namespace WhatBug.Application.Common.Result
{
    public static class Errors
    {
        public static class Admin
        {
            public static class Roles
            {
                public static Error NameIsTaken(string roleName) =>
                    new Error("RoleNameIsTaken", $"A role with the name {roleName} already exists");
            }
        }

        public static class Issues
        {
            public static Error ProjectNotFound(int projectId) =>
                new Error("ProjectForIssueNotFound", $"Could not find project with id {projectId}");

            public static Error PriorityNotFound(int priorityId) =>
                new Error("PriorityForIssueNotFound", $"Could not find priority with id {priorityId}");

            public static Error IssueTypeNotFound(int issueTypeId) =>
                new Error("IssueTypeForIssueNotFound", $"Could not find issue type with id {issueTypeId}");

            public static Error CommendAuthorNotFound(string issueId, int authorId) =>
                new Error("AuthorForIssueCommentNotFound", $"Could not find user with id {authorId} " +
                        $"for new comment on issue {issueId}");
            public static Error IssueNotFound(string issueId) =>
                new Error("IssueForCommentNotFound", $"Could not find issue with id {issueId}");

            public static Error AttachmentTooBig(string fileName, long maxFileSize) =>
                new Error("IssueAttachmentTooBig", $"Attachment {fileName} exceeds the maximum allowed file size ({maxFileSize})");

            public static Error ReporterNotFound(int reporterId) =>
                new Error("ReporterForIssueNotFound", $"Could not find reporter user with id {reporterId}");

            public static Error AssigneeNotFound(int assigneeId) =>
                new Error("AssigneeForIssueNotFound", $"Could not find assignee user with id {assigneeId}");

            public static Error FileTypeNotAllowed(string extension) =>
                new Error("FileTypeNotAllowed", $"File extension {extension} is not allowed");
        }

        public static class PermissionScheme
        {
            public static Error NameIsTaken(string schemeName) =>
                new Error("SchemeNameIsTaken", $"A permission scheme with the name {schemeName} already exists");
        }
    }

}
