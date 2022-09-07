using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PTP.Core.Entities
{
    public abstract class EntityBase
    {
        [Column("Id")]

        public int Id { get; set; }

        


    }

    public class EntityBaseFilter
    {
        public string Keyword { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string OrderBy { get; set; }

        public bool? DescendingDirection { get; set; }

        public string Direction
        {
            get => DescendingDirection.HasValue && DescendingDirection.Value ? "desc" : "asc";
        }
    }
}
