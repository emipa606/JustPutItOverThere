using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace JustPutItOverThere;

[HarmonyPatch(typeof(Pawn_TraderTracker), "GiveSoldThingToPlayer")]
public static class Pawn_TraderTracker_GiveSoldThingToPlayer
{
    public static bool Prefix(ref Thing toGive, int countToGive, Pawn playerNegotiator, Pawn ___pawn)
    {
        if (toGive is Pawn)
        {
            JustPutItOverThere.LogMessage("Trading pawn, vanilla can handle it");
            return true;
        }

        if (toGive.ParentHolder is not Pawn_InventoryTracker inventory || inventory.pawn == null)
        {
            JustPutItOverThere.LogMessage("Parentholder could not be found, not pawn");
            return true;
        }

        var carrier = inventory.pawn;
        if (carrier.RaceProps.Animal)
        {
            JustPutItOverThere.LogMessage($"{carrier} is animal, looking for another");
            var currentCarrier = carrier;
            var lord = inventory.pawn.GetLord();
            if (lord.CurLordToil.ToString().ToLower().Contains("exitmap"))
            {
                JustPutItOverThere.LogMessage($"Current toil, {lord.CurLordToil}, does not allow staying around");
                return true;
            }

            var validCarrier = lord.ownedPawns.Where(pawn =>
                    !pawn.RaceProps.Animal && pawn.health.capacities.CapableOf(PawnCapacityDefOf.Manipulation) &&
                    !pawn.CurJobDef.defName.ToLower().Contains("haul"))
                .OrderBy(pawn => pawn.Position.DistanceTo(currentCarrier.Position));

            if (!validCarrier.Any())
            {
                JustPutItOverThere.LogMessage("Could not find any free carriers");
                return true;
            }

            carrier = validCarrier.First();
        }

        var positionHeld = toGive.PositionHeld;
        var mapHeld = toGive.MapHeld;
        var thing = toGive.SplitOff(countToGive);
        thing.PreTraded(TradeAction.PlayerBuys, playerNegotiator, ___pawn);
        GenPlace.TryPlaceThing(thing, positionHeld, mapHeld, ThingPlaceMode.Near);

        if (!carrier.CanReserveAndReach(thing, PathEndMode.ClosestTouch, carrier.NormalMaxDanger()))
        {
            JustPutItOverThere.LogMessage($"{carrier} can not CanReserveAndReach item, aborting");
            ___pawn.GetLord()?.extraForbiddenThings.Add(thing);
            return false;
        }

        var haulJob = HaulAIUtility.HaulToStorageJob(carrier, thing);

        if (haulJob == null)
        {
            JustPutItOverThere.LogMessage($"{carrier} could not generate haul-job");
            ___pawn.GetLord()?.extraForbiddenThings.Add(thing);
            return false;
        }

        carrier.jobs.TryTakeOrderedJob(haulJob, JobTag.UnspecifiedLordDuty);
        return false;
    }
}