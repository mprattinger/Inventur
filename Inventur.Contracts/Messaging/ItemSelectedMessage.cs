﻿using Inventur.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Contracts.Messaging
{
    public class ItemSelectedMessage
    {
        public InventurItem SelectedItem { get; set; }
    }
}
