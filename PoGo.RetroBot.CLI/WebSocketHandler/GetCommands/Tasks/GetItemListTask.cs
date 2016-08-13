using System.Threading.Tasks;
using PoGo.RetroBot.CLI.WebSocketHandler.GetCommands.Events;
using PoGo.RetroBot.Logic.State;
using SuperSocket.WebSocket;

namespace PoGo.RetroBot.CLI.WebSocketHandler.GetCommands.Tasks
{
    class GetItemListTask
    {
        public static async Task Execute(ISession session, WebSocketSession webSocketSession, string requestID)
        {
            var allItems = await session.Inventory.GetItems();
            webSocketSession.Send(EncodingHelper.Serialize(new ItemListResponce(allItems, requestID)));
        }
    }
}
