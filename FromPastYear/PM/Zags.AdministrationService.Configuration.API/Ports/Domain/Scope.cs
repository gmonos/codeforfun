﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.AdministrationService.Configuration.API.Ports.Domain
{
    public class Scope
    {
        public List<Role> Roles { get; set; }

        public List<Environment> Environments { get; set; }
            
    }
}
