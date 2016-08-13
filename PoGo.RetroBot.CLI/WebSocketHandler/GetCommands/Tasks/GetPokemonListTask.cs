using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoGo.RetroBot.CLI.WebSocketHandler.GetCommands.Events;
using PoGo.RetroBot.Logic.State;
using SuperSocket.WebSocket;

namespace PoGo.RetroBot.CLI.WebSocketHandler.GetCommands.Tasks
{
    class GetPokemonListTask
    {
    
        public static async Task Execute(ISession session, WebSocketSession webSocketSession, string requestID)
        {
            var allPokemonInBag = await session.Inventory.GetHighestsCp(1000);
            var list = new List<PokemonListWeb>();
            allPokemonInBag.ToList().ForEach(o => list.Add(new PokemonListWeb(o)));
            webSocketSession.Send(EncodingHelper.Serialize(new PokemonListResponce(list,requestID)));
        }

    }
}
