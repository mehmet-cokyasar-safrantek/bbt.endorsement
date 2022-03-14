﻿using Worker.App.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Worker.App.Domain.Entities
{
    public class Reference : AuditableEntity
    {
        public Reference()
        {
            Callbacks = new HashSet<Callback>();
        }

        [Key]
        public string OrderId { get; set; }

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

        public virtual Order Approval { get; set; }
        public virtual ICollection<Callback> Callbacks { get; set; }
    }
}
