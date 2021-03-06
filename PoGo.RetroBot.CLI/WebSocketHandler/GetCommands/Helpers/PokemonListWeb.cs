﻿using POGOProtos.Data;

namespace PoGo.RetroBot.CLI.WebSocketHandler.GetCommands
{
    public class PokemonListWeb
    {
        public PokemonData Base;

        public PokemonListWeb(PokemonData data)
        {
            Base = data;
        }

        public double IvPerfection
        {
            get
            {
                return Logic.PoGoUtils.PokemonInfo.CalculatePokemonPerfection(Base);
            }
        }

    }
}



