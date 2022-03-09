﻿using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Reference : AuditableEntity
    {
        public Reference()
        {
            Callbacks = new HashSet<Callback>();
        }

        [Key]
        public string ApprovalId { get; set; } = null!;
        public string InstanceId { get; set; } = null!;

        /// <summary>
        /// Süreç
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Aşama
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// İşlem No
        /// </summary>
        public string Number { get; set; }

        public virtual Approval Approval { get; set; }
        public virtual ICollection<Callback> Callbacks { get; set; }
    }
}
