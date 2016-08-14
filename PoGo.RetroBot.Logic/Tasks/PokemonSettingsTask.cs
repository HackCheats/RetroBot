#region using directives

using System;
using System.Linq;
using System.Threading.Tasks;
using PoGo.RetroBot.Logic.Event;
using PoGo.RetroBot.Logic.PoGoUtils;
using PoGo.RetroBot.Logic.State;

#endregion

namespace PoGo.RetroBot.Logic.Tasks
{
    public class PokemonSettingsTask
    {
        public static async Task Execute(ISession session, Action<IEvent> action)
        {
            var settings = await session.Inventory.GetPokemonSettings();

            action(new PokemonSettingsEvent
            {
                Data = settings.ToList()
            });

            await DelayingUtils.Delay(session.LogicSettings.DelayBetweenPlayerActions, 0);
        }
    }
}
