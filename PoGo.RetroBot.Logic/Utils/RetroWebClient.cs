using System;
using System.Net;

namespace PoGo.RetroBot.Logic.Utils
{
    public class RetroWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 10000;
            return w;
        }
    }
}
