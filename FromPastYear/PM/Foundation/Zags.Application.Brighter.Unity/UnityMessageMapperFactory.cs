using Microsoft.Practices.Unity;
using paramore.brighter.commandprocessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zags.Disptacher.Brighter.Unity
{
    public class UnityMessageMapperFactory : IAmAMessageMapperFactory
    {
        private readonly UnityContainer _container;

        public UnityMessageMapperFactory(UnityContainer container)
        {
            _container = container;
        }

        public IAmAMessageMapper Create(Type messageMapperType)
        {
            return (IAmAMessageMapper)_container.Resolve(messageMapperType);
        }
    }
}
