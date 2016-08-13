using POGOProtos.Inventory.Item;

namespace PoGo.RetroBot.Logic.Event
{
    public class UseBerryEvent : IEvent
    {
        public ItemId BerryType;
        public int Count;
    }
}