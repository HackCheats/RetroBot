#region using directives

using System.Linq;
using System.Threading.Tasks;

using POGOProtos.Inventory.Item;
using PoGo.RetroBot.Logic.State;
using PoGo.RetroBot.Logic.Event;
using PoGo.RetroBot.Logic.Utils;
using System;

#endregion
namespace PoGo.RetroBot.Logic.Tasks
{
    public class InventoryListTask
    {
        public static async Task Execute(ISession session, Action<IEvent> action)
        {
            // Refresh inventory so that the player stats are fresh
            await session.Inventory.RefreshCachedInventory();

            var inventory = await session.Inventory.GetItems();

            action(
                new InventoryListEvent
                {
                    Items = inventory.ToList()
                });

            await DelayingUtils.Delay(session.LogicSettings.DelayBetweenPlayerActions, 0);
        }
    }
}
