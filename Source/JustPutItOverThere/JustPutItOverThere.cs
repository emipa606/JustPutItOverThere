using HarmonyLib;
using Verse;

namespace JustPutItOverThere;

[StaticConstructorOnStartup]
public class JustPutItOverThere
{
    static JustPutItOverThere()
    {
        new Harmony("Mlie.JustPutItOverThere").PatchAll();
    }

    public static void LogMessage(string message)
    {
#if DEBUG
        Log.Message($"[JustPutItOverThere]: {message}");
#endif
    }
}