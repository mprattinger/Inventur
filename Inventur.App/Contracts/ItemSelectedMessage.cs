using Inventur.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.App.Contracts
{
    public class ItemSelectedMessage
    {
        public InventurItem SelectedItem { get; set; }
    }
}
