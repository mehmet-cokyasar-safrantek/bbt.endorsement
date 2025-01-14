﻿namespace Worker.App.Models
{
    public class ContractModel
    {
        public StartRequest StartRequest { get; set; }
        public Guid InstanceId { get; set; }
        public bool Device { get; set; }

        public bool Approved { get; set; }
        public bool Completed { get; set; }
        public bool IsProcess { get; set; }
        public bool RetryEnd { get; set; }
        public int Limit { get; set; }

    }


    public class StartRequest
    {
        /// <summary>
        /// Unique Id of order. Id is corrolation key of workflow also. 
        /// </summary>
        public Guid Id { get; set; }
        public string Title { get; set; }
        public OrderConfig Config { get; set; }
        public ReferenceClass Reference { get; set; }
        public List<DocumentClass> Documents { get; set; }
        public ApproverClass Approver { get; set; }

        public class DocumentClass
        {
            public int DocumentType { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            //public Dictionary<string, string> FormParameters { get; set; }

           // public IFormFile Files { get; set; }
            public ContentType Type { get; set; }
            public ActionClass[] Actions { get; set; } // Options

            public class ActionClass
            {
                public bool IsDefault { get; set; }
                public string Title { get; set; }
                public ActionType Type { get; set; }
            }
        }
        public class ApproverClass
        {
            //public long Customer { get; set; }
            //public long Approver { get; set; }
            public int Type { get; set; }
            public string Value { get; set; }
            public string NameSurname { get; set; }
        }
        public class ReferenceClass
        {
            public string Process { get; set; }
            public string State { get; set; }
            public string ProcessNo { get; set; }
            public CallbackClass Callback { get; set; }

        }
        public class CallbackClass
        {
            public CalbackMode Mode { get; set; }
            public string URL { get; set; }
        }
        public class OrderConfig
        {
            public int MaxRetryCount { get; set; }
            public string RetryFrequence { get; set; }
            public int ExpireInMinutes { get; set; }
            public string NotifyMessageSMS { get; set; }
            public string NotifyMessagePush { get; set; }
            public string RenotifyMessageSMS { get; set; }
            public string RenotifyMessagePush { get; set; }
        }
    }
    public enum ContentType { HTML, PDF, PlainText }
    public enum CalbackMode { Completed, Verbose }
    public enum ActionType { Approve, Reject }
}
