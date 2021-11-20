using System.Collections.Generic;

namespace PublicWorkflow.Application.Constants
{
    public static class Messages
    {
        //public static List<string> GeneratePermissionsForModule(string module)
        //{
        //    return new List<string>()
        //    {
        //        $"Permissions.{module}.Create",
        //        $"Permissions.{module}.View",
        //        $"Permissions.{module}.Edit",
        //        $"Permissions.{module}.Delete",
        //    };
        //}

        public static class ProcessJob
        {
            public const string CompletedRejected = "Process completed with rejection";
            public const string CompletedApproved = "Process completed with approval";
            public const string Delete = "Process deleted successfully";
        }
        public static class ProcessLevel
        {
            public const string CompletedRejected = "Approval level rejected";
            public const string CompletedApproved = "Approval level approved";
            public const string Delete = "Approval level deleted successfully";
        }

        //public static class Process
        //{
        //    public const string View = "Permissions.Process.View";
        //    public const string Create = "Permissions.Process.Create";
        //    public const string Edit = "Permissions.Process.Edit";
        //    public const string Delete = "Permissions.Process.Delete";
        //}

        //public static class Users
        //{
        //    public const string View = "Permissions.Users.View";
        //    public const string Create = "Permissions.Users.Create";
        //    public const string Edit = "Permissions.Users.Edit";
        //    public const string Delete = "Permissions.Users.Delete";
        //}

        //public static class Approval
        //{
        //    public const string View = "Permissions.Approval.View";
        //    public const string Create = "Permissions.Approval.Create";
        //    public const string Edit = "Permissions.Approval.Edit";
        //    public const string Delete = "Permissions.Approval.Delete";
        //}
        //public static class Organization
        //{
        //    public const string View = "Permissions.Organization.View";
        //    public const string Create = "Permissions.Organization.Create";
        //    public const string Edit = "Permissions.Organization.Edit";
        //    public const string Delete = "Permissions.Organization.Delete";
        //}
        //public static class Config
        //{
        //    public const string View = "Permissions.Config.View";
        //    public const string Create = "Permissions.Config.Create";
        //    public const string Edit = "Permissions.Config.Edit";
        //    public const string Delete = "Permissions.Config.Delete";
        //}
    }
}