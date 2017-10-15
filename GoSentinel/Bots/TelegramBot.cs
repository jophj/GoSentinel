using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoSentinel.Models;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace GoSentinel.Bots
{
    public class TelegramBot : ITelegramBot
    {
        private readonly TelegramBotClient _client;
        private readonly ICollection<Action<Message>> _messageHandlers;

        public TelegramBot(TelegramBotConfiguration config)
        {
            _messageHandlers = new List<Action<Message>>();
            _client = new TelegramBotClient(config.Token);
            _client.StartReceiving();
            _client.OnMessage += OnMessage;

        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            foreach (var messageHandler in _messageHandlers)
            {
                messageHandler(e.Message);
            }
        }

        public void AddMessageHandler(Action<Message> messageHandler)
        {
            _messageHandlers.Add(messageHandler);
        }

        public async Task<Message> SendTextMessageAsync(long chatId, string text)
        {
            return await _client.SendTextMessageAsync(chatId, text);
        }
    }
}
