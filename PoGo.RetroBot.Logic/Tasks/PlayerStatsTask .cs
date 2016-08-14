#region using directives

using System.Linq;
using System.Threading.Tasks;
using PoGo.RetroBot.Logic.Event;
using PoGo.RetroBot.Logic.State;
using POGOProtos.Inventory.Item;
using System;

#endregion

namespace PoGo.RetroBot.Logic.Tasks
{
    public class PlayerStatsTask
    {
        public static async Task Execute(ISession session, Action<IEvent> action)
        {
            var PlayersProfile = (await session.Inventory.GetPlayerStats())
                .ToList();
            
            action(
                new PlayerStatsEvent
                {
                    PlayerStats = PlayersProfile,
                });

            await Task.Delay(session.LogicSettings.DelayBetweenPlayerActions);
        }
    }
}
