using System.Threading.Tasks;
using PoGo.RetroBot.CLI.WebSocketHandler.GetCommands.Tasks;
using PoGo.RetroBot.Logic.State;
using SuperSocket.WebSocket;

namespace PoGo.RetroBot.CLI.WebSocketHandler.GetCommands
{
    public class GetPokemonSettingsHandler : IWebSocketRequestHandler
    {
        public string Command { get; private set;}

        public GetPokemonSettingsHandler()
        {
            Command = "GetPokemonSettings";
        }

        public async Task Handle(ISession session, WebSocketSession webSocketSession, dynamic message)
        {
            await GetPokemonSettingsTask.Execute(session, webSocketSession, (string)message.RequestID);
        }
    }
}
