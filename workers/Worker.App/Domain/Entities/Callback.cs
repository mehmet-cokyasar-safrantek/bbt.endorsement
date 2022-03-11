﻿using Worker.App.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Worker.App.Domain.Entities
{
    public class Callback : AuditableEntity
    {
        public string CallbackId { get; set; }
        [Key]
        public string ReferenceId { get; set; }

        public string Mode { get; set; } = null!;
        public string Url { get; set; } = null!;

        public virtual Reference Reference { get; set; } = null!;
    }
}