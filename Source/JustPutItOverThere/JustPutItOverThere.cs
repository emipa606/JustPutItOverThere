using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

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

    public static Job GetHaulJob(Pawn p, Thing t)
    {
        var currentPriority = StoreUtility.CurrentStoragePriorityOf(t);
        if (!StoreUtility.TryFindBestBetterStorageFor(t, p, p.Map, currentPriority, p.Faction, out var storeCell,
                out var haulDestination))
        {
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