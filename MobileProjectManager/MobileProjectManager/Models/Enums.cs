using System;
using System.Collections.Generic;
using System.Text;

namespace MobileProjectManager.Models
{
    public enum TaskStatus { Opened, InProgress, Completed, Confirmed, Closed};
    public enum NotificationType
    {
        InviteToTeam,
        InviteAccepted,
        InviteDenied,
        WorkerAddedToProject,
        TaskReportApproved,
        TaskReportDeclined,
        TaskCompleteReport,
        GetTaskReport
    }
}
