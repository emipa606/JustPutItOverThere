using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace JustPutItOverThere;

[StaticConstructorOnStartup]
public static class JustPutItOverThere
{
    static JustPutItOverThere()
    {
        new Harmony("Mlie.JustPutItOverThere").PatchAll();
    }

    public static void LogMessage(string message)
    {
        if (Prefs.DevMode)
        {
            Log.Message($"[JustPutItOverThere]: {message}");
        }
    }

    public static Job GetHaulJob(Pawn p, Thing t)
    {
        var currentPriority = StoreUtility.CurrentStoragePriorityOf(t);
        if (!StoreUtility.TryFindBestBetterStorageFor(t, p, p.Map, currentPriority, Faction.OfPlayer, out var storeCell,
                out var haulDestination))
        {
            LogMessage(
                $"Could not find better storage for {t}, current priority={currentPriority}, storeCell={storeCell}, haulDestination={haulDestination}");
            JobFailReason.Is(HaulAIUtility.NoEmptyPlaceLowerTrans);
            return null;
        }

        switch (haulDestination)
        {
            case ISlotGroupParent:
                return HaulAIUtility.HaulToCellStorageJob(p, t, storeCell, false);
            case Thing thing when thing.TryGetInnerInteractableThingOwner() != null:
                return HaulAIUtility.HaulToContainerJob(p, t, thing);
            default:
                Log.Error(
                    $"Don't know how to handle HaulToStorageJob for storage {haulDestination.ToStringSafe()}. thing={t.ToStringSafe()}");
                return null;
        }
    }
}