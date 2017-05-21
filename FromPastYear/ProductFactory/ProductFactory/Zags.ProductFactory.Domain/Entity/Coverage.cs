using Zags.Domain.Entity;

namespace Zags.ProductFactory.Domain
{
    public class Coverage : EntityBase
    {
        public Coverage()
        {

        }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsMandatory { get; set; }

       
    }
}
