#region using directives

using PoGo.RetroBot.Logic.Common;

#endregion

namespace PoGo.RetroBot.Logic.Localization
{
    public interface ILocalizer
    {
        string GetFormat(TranslationString key);
        string GetFormat(TranslationString key, params object[] data);
    }

    public class Localizer : ILocalizer
    {
        public string GetFormat(TranslationString key)
        {
            return "";
        }

        public string GetFormat(TranslationString key, params object[] data)
        {
            return "";
        }
    }
}