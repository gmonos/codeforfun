using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.AdministrationService.Configuration.API.Ports.Domain
{
    public class Variable
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public Scope Scope { get; set; }

    }
}
