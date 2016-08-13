using System.Collections.Generic;
using POGOProtos.Inventory.Item;

namespace PoGo.RetroBot.Logic.Event
{
    public class InventoryListEvent : IEvent
    {
        public List<ItemData> Items;
    }
}
