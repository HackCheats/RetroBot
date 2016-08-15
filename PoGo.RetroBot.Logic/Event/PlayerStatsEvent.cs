#region using directives

using System.Linq;
using System.Threading.Tasks;

using POGOProtos.Inventory.Item;
using PoGo.RetroBot.Logic.State;
using PoGo.RetroBot.Logic.Event;
using POGOProtos.Data.Player;
using System.Collections.Generic;

#endregion

namespace PoGo.RetroBot.Logic.Tasks
{
    public class PlayerStatsEvent : IEvent

    {
        public List<PlayerStats> PlayerStats { get; set; }
    }
}
