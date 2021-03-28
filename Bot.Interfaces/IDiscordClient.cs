using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bot.Interfaces
{
    public interface IDiscordClient
    {
        DiscordSocketClient Client { get; set; }
        bool IsRunning { get; }

        Task RunBot(IServiceProvider provider, IConfiguration configuration,  string discordToken = null);
    }
}