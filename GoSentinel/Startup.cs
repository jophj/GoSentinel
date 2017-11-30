using ApiAiSDK;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GoSentinel.Bots.Controllers;
using GoSentinel.Services;
using GoSentinel.Models;
using GoSentinel.Services.Actions;
using GoSentinel.Services.ActionMappings;
using GoSentinel.Services.ActionResponse;

namespace GoSentinel
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(Configuration.GetSection("BotConfiguration").GetSection("Telegram").Get<TelegramBotConfiguration>());
            services.AddSingleton<IBotService, BotService>();

            services.AddSingleton<IActionController<AddPokemonFilterAction>, AddPokemonFilterActionController>();
            services.AddSingleton<IActionController<NearestPokemonAction>, NearestPokemonActionController>();

            services.AddSingleton<IActionResponseController<PokemonFilterActionResponse>, PokemonFilterActionResponseController>();
            services.AddSingleton<IActionResponseController<NearestPokemonActionResponse>, NearestPokemonActionResponseController>();

            services.AddSingleton<PokemonFilterActionResponseService, PokemonFilterActionResponseService>();
            services.AddSingleton<IPokemonFilterActionService, LogPokemonFilterActionService>();

            services.AddSingleton<IBotMessageController, BotMessageController>();
            services.AddSingleton<AiResponseToActionService, AiResponseToActionService>();

            services.AddSingleton<PokemonFilterMapping, PokemonFilterMapping>();
            services.AddSingleton<NearestPokemonMapping, NearestPokemonMapping>();

            var apiAiConfig = Configuration.GetSection("ApiAiConfiguration").Get<ApiAiConfiguration>();
            var config =
                new AIConfiguration(apiAiConfig.AccessToken, SupportedLanguage.FromLanguageTag(apiAiConfig.LanguageTag));

            var apiAi = new ApiAi(config);
            services.AddSingleton(apiAi);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseBotService();
        }
    }
}
