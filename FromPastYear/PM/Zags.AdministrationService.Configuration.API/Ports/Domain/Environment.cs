﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.AdministrationService.Configuration.API.Ports.Domain
{
    public class Environment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int SortOrder { get; set; }


    }
}
