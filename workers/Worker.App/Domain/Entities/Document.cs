﻿using System.ComponentModel.DataAnnotations;

namespace Worker.App.Domain.Entities
{
    public class Document
    {
        public string DocumentId { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public string Content { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }
        public virtual ICollection<Action> Actions { get; set; }

        public virtual Order Order { get; set; }
    }
}
