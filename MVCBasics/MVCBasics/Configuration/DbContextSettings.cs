﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasics
{
    public class DbContextSettings
    {
        public bool Enabled { get; set; }
        public string ConnectionString { get; set; }
    }
}