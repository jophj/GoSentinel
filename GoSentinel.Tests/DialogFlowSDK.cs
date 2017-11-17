using System;
using Xunit;
using ApiAiSDK;
using ApiAiSDK.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoSentinel.Models;
using Action = GoSentinel.Models.Action;

namespace GoSentinel.Tests
{
    public class DialogFlowSdk
    {
        private AIConfiguration config;
        private readonly ApiAi _apiAi;

        public DialogFlowSdk()
        {
            config = new AIConfiguration("7f798b1c53e34efcad868e2e54a95275", SupportedLanguage.Italian);
            _apiAi = new ApiAi(config);
        }
        [Fact]
        public void Should_return_200()
        {
            var response = _apiAi.TextRequest("aggiungi rattata cp 1200");
            Assert.Equal(response.Status.Code, 200);
        }

        [Fact]
        public void Should_Recognize_AddPokemonFilter_Action()
        {
            ICollection<String> queries = new List<String>()
            {
                "metti rattata sopra il 90%",
                "metti pidgey",
                "notifica rattata iv 100%",
                "notifica pidgey cp 123",
                "aggiungi rattata lv 30"
            };
            var allResults = queries.Select(q => _apiAi.TextRequest(q));
            Assert.All(allResults, r => Assert.Equal(r.Result.Action, Action.AddPokemonFilter));
        }
    }
}
