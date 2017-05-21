using System;
using Zags.ProductFactory.Domain;

namespace ZAGS.ProductFactory.WebAPI.Models
{
    public class PackModel
    {
        
        public PackModel()
        {
            
        }

        public Int32 Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }

}
