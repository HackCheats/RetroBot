using System.Linq;
using System.Threading.Tasks;
using PoGo.RetroBot.CLI.WebSocketHandler.GetCommands.Events;
using PoGo.RetroBot.CLI.WebSocketHandler.GetCommands.Helpers;
using PoGo.RetroBot.Logic.State;
using SuperSocket.WebSocket;

namespace PoGo.RetroBot.CLI.WebSocketHandler.GetCommands.Tasks
{
    class GetTrainerProfileTask
    {
        public static async Task Execute(ISession session, WebSocketSession webSocketSession, string requestID)
        {
            var playerStats = (await session.Inventory.GetPlayerStats()).FirstOrDefault();
            if (playerStats == null)
                return;
            var tmpData = new TrainerProfileWeb(session.Profile.PlayerData, playerStats);
            webSocketSession.Send(EncodingHelper.Serialize(new TrainerProfileResponce(tmpData, requestID)));
        }
    }
}
