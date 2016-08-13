using System.Threading.Tasks;
using PoGo.RetroBot.CLI.WebSocketHandler.GetCommands.Events;
using PoGo.RetroBot.Logic.State;
using SuperSocket.WebSocket;

namespace PoGo.RetroBot.CLI.WebSocketHandler.GetCommands.Tasks
{
    class GetPokemonSettingsTask
    {
    
        public static async Task Execute(ISession session, WebSocketSession webSocketSession, string requestID)
        {
            var settings = await session.Inventory.GetPokemonSettings();
            webSocketSession.Send(EncodingHelper.Serialize(new WebResponce()
            {
                Command = "PokemonSettings",
                Data = settings,
                RequestID = requestID
            }));
        }

    }
}
