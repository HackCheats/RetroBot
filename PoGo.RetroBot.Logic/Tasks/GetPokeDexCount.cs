using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PoGo.RetroBot.Logic.Common;
using PoGo.RetroBot.Logic.Logging;
using PoGo.RetroBot.Logic.State;

namespace PoGo.RetroBot.Logic.Tasks
{
    class GetPokeDexCount
    {
        public static async Task Execute(ISession session, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await session.Inventory.RefreshCachedInventory();

            var PokeDex = await session.Inventory.GetPokeDexItems();
            var _totalUniqueEncounters = PokeDex.Select(i => new { Pokemon = i.InventoryItemData.PokedexEntry.PokemonId, Captures = i.InventoryItemData.PokedexEntry.TimesCaptured });
            var _totalCaptures = _totalUniqueEncounters.Count(i => i.Captures > 0);
            var _totalData = PokeDex.Count();
            
            Logger.Write(session.Translation.GetTranslation(TranslationString.AmountPkmSeenCaught, _totalData, _totalCaptures));
        }
    }
}