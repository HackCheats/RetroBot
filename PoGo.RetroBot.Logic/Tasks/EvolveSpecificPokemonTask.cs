using PoGo.PokeMobBot.Logic.Event;
using PoGo.PokeMobBot.Logic.State;
using PoGo.RetroBot.Logic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoGo.RetroBot.Logic.Tasks
{
    public class EvolveSpecificPokemonTask
    {
        public static async Task Execute(ISession session, string pokemonId)
        {
            var id = ulong.Parse(pokemonId);

            var all = await session.Inventory.GetPokemons();
            var pokemons = all.OrderByDescending(x => x.Cp).ThenBy(n => n.StaminaMax);
            var pokemon = pokemons.FirstOrDefault(p => p.Id == id);

            if (pokemon == null) return;

            var evolveResponse = await session.Client.Inventory.EvolvePokemon(pokemon.Id);

            session.EventDispatcher.Send(new PokemonEvolveEvent
            {
                Id = pokemon.PokemonId,
                Exp = evolveResponse.ExperienceAwarded,
                UniqueId = pokemon.Id,
                Result = evolveResponse.Result
            });

            await DelayingUtils.Delay(session.LogicSettings.DelayBetweenPlayerActions, 0);
        }
    }
}
