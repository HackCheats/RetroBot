using PoGo.RetroBot.Logic.State;
using PoGo.RetroBot.Logic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoGo.RetroBot.Logic.Tasks
{
    public class TransferPokemonTask
    {
        public static async Task Execute(ISession session, string pokemonId)
        {
            var id = ulong.Parse(pokemonId);

            var all = await session.Inventory.GetPokemons();
            var pokemons = all.OrderBy(x => x.Cp).ThenBy(n => n.StaminaMax);
            var pokemon = pokemons.FirstOrDefault(p => p.Id == id);

            if (pokemon == null) return;

            var pokemonSettings = await session.Inventory.GetPokemonSettings();
            var pokemonFamilies = await session.Inventory.GetPokemonFamilies();

            await session.Client.Inventory.TransferPokemon(id);
            await session.Inventory.DeletePokemonFromInvById(id);

            var bestPokemonOfType = (session.LogicSettings.PrioritizeIvOverCp
                ? await session.Inventory.GetHighestPokemonOfTypeByIv(pokemon)
                : await session.Inventory.GetHighestPokemonOfTypeByCp(pokemon)) ?? pokemon;

            var setting = pokemonSettings.Single(q => q.PokemonId == pokemon.PokemonId);
            var family = pokemonFamilies.First(q => q.FamilyId == setting.FamilyId);

            family.Candy_++;

            // Broadcast event as everyone would benefit
            session.EventDispatcher.Send(new Logic.Event.TransferPokemonEvent
            {
                Id = pokemon.PokemonId,
                Perfection = Logic.PoGoUtils.PokemonInfo.CalculatePokemonPerfection(pokemon),
                Cp = pokemon.Cp,
                BestCp = bestPokemonOfType.Cp,
                BestPerfection = Logic.PoGoUtils.PokemonInfo.CalculatePokemonPerfection(bestPokemonOfType),
                FamilyCandies = family.Candy_
            });

            await DelayingUtils.Delay(session.LogicSettings.DelayBetweenPlayerActions, 0);
        }

        public static Task Execute(ISession session, ulong pokemonId)
        {
            throw new NotImplementedException();
        }
    }
}
