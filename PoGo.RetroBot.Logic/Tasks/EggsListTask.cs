#region using directives

using System;
using System.Linq;
using System.Threading.Tasks;
using PoGo.RetroBot.Logic.Event;
using PoGo.RetroBot.Logic.State;
using PoGo.RetroBot.Logic.Utils;
using POGOProtos.Inventory.Item;

#endregion

namespace PoGo.RetroBot.Logic.Tasks
{
    public class EggsListTask
    {
        public static async Task Execute(ISession session, Action<IEvent> action)
        {
            // Refresh inventory so that the player stats are fresh
            await session.Inventory.RefreshCachedInventory();

            var playerStats = (await session.Inventory.GetPlayerStats()).FirstOrDefault();
            if (playerStats == null)
                return;

            var kmWalked = playerStats.KmWalked;

            var incubators = (await session.Inventory.GetEggIncubators())
                .Where(x => x.UsesRemaining > 0 || x.ItemId == ItemId.ItemIncubatorBasicUnlimited)
                .OrderByDescending(x => x.ItemId == ItemId.ItemIncubatorBasicUnlimited)
                .ToList();

            var unusedEggs = (await session.Inventory.GetEggs())
                .Where(x => string.IsNullOrEmpty(x.EggIncubatorId))
                .OrderBy(x => x.EggKmWalkedTarget - x.EggKmWalkedStart)
                .ToList();

            action(
                new EggsListEvent
                {
                    PlayerKmWalked = kmWalked,
                    Incubators = incubators,
                    UnusedEggs = unusedEggs
                });

            await DelayingUtils.Delay(session.LogicSettings.DelayBetweenPlayerActions, 0);
        }
    }
}
