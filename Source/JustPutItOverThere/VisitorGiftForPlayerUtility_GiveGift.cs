using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace JustPutItOverThere;

[HarmonyPatch(typeof(VisitorGiftForPlayerUtility), "GiveGift")]
public static class VisitorGiftForPlayerUtility_GiveGift
{
    public static void Postfix(List<Pawn> possibleGivers, List<Thing> gifts)
    {
        foreach (var gift in gifts)
        {
            var validCarrier = possibleGivers.Where(pawn =>
                    !pawn.RaceProps.Animal && pawn.health.capacities.CapableOf(PawnCapacityDefOf.Manipulation) &&
                    !pawn.CurJobDef.defName.ToLower().Contains("haul"))
                .OrderBy(pawn => pawn.Position.DistanceTo(gift.Position));
            if (!validCarrier.Any())
            {
                JustPutItOverThere.LogMessage("Could not find any free carriers");
                return;
            }

            var carrier = validCarrier.First();
            if (!carrier.CanReserveAndReach(gift, PathEndMode.ClosestTouch, carrier.NormalMaxDanger()))
            {
                JustPutItOverThere.LogMessage($"{carrier} can not CanReserveAndReach {gift}, aborting");
                continue;
            }

            var haulJob = HaulAIUtility.HaulToStorageJob(carrier, gift);

            if (haulJob == null)
            {
                JustPutItOverThere.LogMessage($"{carrier} could not generate haul-job");
                continue;
            }

            carrier.jobs.TryTakeOrderedJob(haulJob, JobTag.UnspecifiedLordDuty, true);
        }
    }
}