using System.Text;
using Microsoft.Azure.ServiceBus;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Web.Api.Extensions;

namespace Suitsupply.Tailoring.Web.Api.Messaging.Converters
{
    public static class MessageConverter
    {
        public static TCommand ConvertToCommand<TCommand>(this Message message)
            where TCommand : ICommand
        {
            var messageBody = Encoding.UTF8.GetString(message.Body);
            return messageBody.FromJson<TCommand>();
        }
    }
}