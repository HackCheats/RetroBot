using System.Threading.Tasks;
using PoGo.RetroBot.Logic.State;
using SuperSocket.WebSocket;

namespace PoGo.RetroBot.CLI.WebSocketHandler
{
    interface IWebSocketRequestHandler
    {
        string Command { get; }
        Task Handle(ISession session, WebSocketSession webSocketSession, dynamic message);
    }
}
