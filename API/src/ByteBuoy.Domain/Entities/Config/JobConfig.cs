﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBuoy.Domain.Entities.Config
{
    public class JobConfig
    {
        public int Version { get; set; }
        public string Host { get; set; } = null!;
        public string Apikey { get; set; } = null!;
        public List<Job> Jobs { get; set; } = [];
    }
}
