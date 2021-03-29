using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace BotClient.CommonModules
{
    public class AllModule : ModuleBase
    {
        private static IMessageChannel _channel;
        private static IUser _user;
        private static IUser _messageAuthor;
        private static IChannel _currentChannel;

        private Interfaces.IDiscordClient _discordClient;

        public AllModule(Interfaces.IDiscordClient client)
        {
            _discordClient = client;
        }

        [Command("help"), Summary("Shows help information")]
        public async Task Help()
        {
            string help = string.Format("Xin chào! :tada:{0}{0}" +
                "Đang viết con bot nè." +
                "{0}........." +
                "{0}{0}......... ", Environment.NewLine);

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription(help);
            await ReplyAsync(string.Empty, false, embed.Build());
        }
    }
}