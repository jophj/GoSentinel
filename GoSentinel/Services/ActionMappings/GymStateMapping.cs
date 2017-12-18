using System;
using ApiAiSDK.Model;
using GoSentinel.Data;

namespace GoSentinel.Services.ActionMappings
{
    public class GymStateMapping : IAiResponseMapper<GymStateAction>
    {
        public GymStateAction Map(AIResponse response)
        {
            string gymName;
            try
            {
                gymName = response.Result.Parameters["GymName"].ToString();
            }
            catch (Exception e)
            {
                gymName = null;
                Console.Error.WriteLine(e);
            }

            return new GymStateAction()
            {
                GymName = gymName
            };
        }
    }
}
