using Newtonsoft.Json;
using paramore.brighter.commandprocessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zags.OrganizationService.Application.Ports.Commands;

namespace Zags.OrganizationService.Application.Ports.Mappers
{


    public class AddOrganizationCommandMessageMapper : IAmAMessageMapper<AddOrganizationCommand>
    {
        public Message MapToMessage(AddOrganizationCommand request)
        {
            var header = new MessageHeader(messageId: request.Id, topic: "Organization.Add", messageType: MessageType.MT_COMMAND);
            var body = new MessageBody(JsonConvert.SerializeObject(request));
            var message = new Message(header, body);
            return message;
        }

        public AddOrganizationCommand MapToRequest(Message message)
        {
            return JsonConvert.DeserializeObject<AddOrganizationCommand>(message.Body.Value);

        }
    }
}
