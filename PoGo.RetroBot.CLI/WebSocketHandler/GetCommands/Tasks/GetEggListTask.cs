using System.Linq;
using System.Threading.Tasks;
using PoGo.RetroBot.CLI.WebSocketHandler.GetCommands.Events;
using PoGo.RetroBot.CLI.WebSocketHandler.GetCommands.Helpers;
using PoGo.RetroBot.Logic.State;
using POGOProtos.Inventory.Item;
using SuperSocket.WebSocket;

namespace PoGo.RetroBot.CLI.WebSocketHandler.GetCommands.Tasks
{
    class GetEggListTask
    {

        public static async Task Execute(ISession session, WebSocketSession webSocketSession, string requestID)
        {
            var incubators = (await session.Inventory.GetEggIncubators())
                .Where(x => x.UsesRemaining > 0 || x.ItemId == ItemId.ItemIncubatorBasicUnlimited)
                .OrderByDescending(x => x.ItemId == ItemId.ItemIncubatorBasicUnlimited)
                .ToList();

            var unusedEggs = (await session.Inventory.GetEggs())
                .Where(x => string.IsNullOrEmpty(x.EggIncubatorId))
                .OrderBy(x => x.EggKmWalkedTarget - x.EggKmWalkedStart)
                .ToList();

    
             var list =  new EggListWeb
             {
                 Incubators = incubators,
                 UnusedEggs = unusedEggs
             };
            webSocketSession.Send(EncodingHelper.Serialize(new EggListResponce(list,requestID)));
        }
    }
}
