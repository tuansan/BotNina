using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Bot.CommonModules
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

        [Command("sendmessage"), Summary("Initializes a new MessageSend")]
        public async Task SendMessage(IMessageChannel channel)
        {
            _channel = channel;
            _messageAuthor = Context.User;
            _currentChannel = Context.Channel;

            _discordClient.Client.MessageReceived -= Client_MessageReceived;
            _discordClient.Client.MessageReceived += Client_MessageReceived;
            await ReplyAsync();
        }

        [Command("sendmessage"), Summary("Initializes a new MessageSend")]
        public async Task SendMessageWithMention(IMessageChannel channel, IUser user)
        {
            _user = user;
            await SendMessage(channel);
        }

        private async Task Client_MessageReceived(Discord.WebSocket.SocketMessage arg)
        {
            if (!arg.Author.IsBot && arg.Author == _messageAuthor && arg.Channel == _currentChannel)
            {
                _discordClient.Client.MessageReceived -= Client_MessageReceived;
                EmbedBuilder embed = null;

                if (arg.Attachments.Count > 0)
                {
                    embed = new EmbedBuilder();
                    foreach (Attachment attachment in arg.Attachments)
                    {
                        // check if it is an image is only possible by checking width/height
                        if (attachment.Width != null)
                        {
                            embed.WithImageUrl(attachment.Url);
                        }
                        else
                        {
                            embed.WithDescription(attachment.Url);
                        }
                    }
                }

                await _channel.SendMessageAsync(_user?.Mention + " " + arg.Content, false, embed.Build());
                _channel = null;
                _user = null;
                _messageAuthor = null;
                _currentChannel = null;
            }
        }
    }
}
