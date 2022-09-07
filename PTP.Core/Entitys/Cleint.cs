using PTP.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PTP.Core.Entitys
{
    [Table("Cleint")]
    public class Cleint : EntityBase 
    {
        public string username { get; set; }
    }
}
