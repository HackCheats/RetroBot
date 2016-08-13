﻿#region using directives

using System.Threading;
using System.Threading.Tasks;
using PoGo.RetroBot.Logic.Tasks;

#endregion

namespace PoGo.RetroBot.Logic.State
{
    public class InfoState : IState
    {
        public async Task<IState> Execute(ISession session, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await DisplayPokemonStatsTask.Execute(session);
            return new FarmState();
        }
    }
}
