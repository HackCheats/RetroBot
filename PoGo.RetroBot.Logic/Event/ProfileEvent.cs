#region using directives

using POGOProtos.Networking.Responses;

#endregion

namespace PoGo.RetroBot.Logic.Event
{
    public class ProfileEvent : IEvent
    {
        public GetPlayerResponse Profile;
    }
}