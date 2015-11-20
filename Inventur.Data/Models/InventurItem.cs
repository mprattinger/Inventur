using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Data.Models
{
    public class InventurItem
    {
        public int ID { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ChangedAt { get; set; }

        public string EANCode { get; set; }
        public int Amount { get; set; }

        public bool Exported { get; set; }
    }
}
