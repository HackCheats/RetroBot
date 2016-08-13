#region using directives

using PoGo.RetroBot.Logic.State;
using PoGo.RetroBot.Logic.Tasks;

#endregion

namespace PoGo.RetroBot.Logic.Service
{
    public class BotService
    {
        public ILogin LoginTask;
        public ISession Session;

        public void Run()
        {
            LoginTask.DoLogin();
        }
    }
}