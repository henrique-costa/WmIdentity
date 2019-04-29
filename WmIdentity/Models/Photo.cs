﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmIdentity.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }
        public string Path { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
