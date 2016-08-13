namespace PoGo.RetroBot.Logic.Event
{
    public class NoticeEvent : IEvent
    {
        public string Message = "";

        public override string ToString()
        {
            return Message;
        }
    }
}