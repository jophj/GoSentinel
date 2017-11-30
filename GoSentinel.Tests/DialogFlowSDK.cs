using System;
using Xunit;
using ApiAiSDK;
using System.Collections.Generic;
using System.Linq;
using Action = GoSentinel.Data.Action;

namespace GoSentinel.Tests
{
    public class DialogFlowSdk
    {
        private readonly ApiAi _apiAi;

        public DialogFlowSdk()
        {
            var config = new AIConfiguration("7f798b1c53e34efcad868e2e54a95275", SupportedLanguage.Italian);
            _apiAi = new ApiAi(config);
        }
        [Fact]
        public void Should_return_200()
        {
            var response = _apiAi.TextRequest("aggiungi rattata cp 1200");
            Assert.Equal(200, response.Status.Code);
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
