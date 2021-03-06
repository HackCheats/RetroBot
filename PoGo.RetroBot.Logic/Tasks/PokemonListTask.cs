#region using directives

using System;
using System.Linq;
using System.Threading.Tasks;
using PoGo.RetroBot.Logic.Event;
using PoGo.RetroBot.Logic.PoGoUtils;
using PoGo.RetroBot.Logic.State;
using PoGo.RetroBot.Logic.Utils;

#endregion

namespace PoGo.RetroBot.Logic.Tasks
{
    public class PokemonListTask
    {
        public static async Task Execute(ISession session, Action<IEvent> action)
        {
            // Refresh inventory so that the player stats are fresh
            await session.Inventory.RefreshCachedInventory();

            var myPokemonSettings = await session.Inventory.GetPokemonSettings();
            var pokemonSettings = myPokemonSettings.ToList();

            var myPokemonFamilies = await session.Inventory.GetPokemonFamilies();
            var pokemonFamilies = myPokemonFamilies.ToArray();

            var allPokemonInBag = await session.Inventory.GetHighestsCp(1000);

            var pkmWithIv = allPokemonInBag.Select(p => {
                var settings = pokemonSettings.Single(x => x.PokemonId == p.PokemonId);
                return Tuple.Create(
                    p,
                    PokemonInfo.CalculatePokemonPerfection(p),
                    pokemonFamilies.Single(x => settings.FamilyId == x.FamilyId).Candy_
                );
            });

            action(new PokemonListEvent
            {
                PokemonList = pkmWithIv.ToList()
            });

            await DelayingUtils.Delay(session.LogicSettings.DelayBetweenPlayerActions, 0);
        }
    }
}
