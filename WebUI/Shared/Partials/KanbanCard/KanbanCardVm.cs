namespace WhatBug.WebUI.Shared.Partials.KanbanCard
{
    public class KanbanCardVm
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string IssueTypeIconColor { get; set; }
        public string IssueTypeIconWebName { get; set; }
        public string PriorityIconColor { get; set; }
        public string PriorityIconWebName { get; set; }
        public string AssigneeEmail { get; set; }
    }
}