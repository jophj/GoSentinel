using System;
using System.Text;
using GoSentinel.Data;

namespace GoSentinel.Services.Messages
{
    public class GymStateMessageService : IMessageService<GymStateActionResponse>
    {
        public string Generate(GymStateActionResponse actionResponse)
        {
            if (actionResponse == null)
            {
                throw new ArgumentNullException();
            }

            if (actionResponse.GymState == null)
            {
                throw new ArgumentException("Gym state should not be null");
            }

            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendLine(
                $"*{actionResponse.GymState.Name}* at {actionResponse.GymState.Timestamp}"
            );

            return messageBuilder.ToString();
        }
    }
}
