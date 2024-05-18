using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace KitchenAngryPotStack
{
    [UpdateInGroup(typeof(LowPriorityInteractionGroup), OrderLast = true)]
    public class MismatchedProviderStartsFire : ItemInteractionSystem, IModSystem
    {
        protected override InteractionType RequiredType => InteractionType.Grab;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!data.Context.Require(data.Target, out CItemProvider provider) ||
                provider.ProvidedItem != -486398094)
                return false;

            if (!data.Context.Require(data.Interactor, out CItemHolder interactHolder))
                return false;

            if (interactHolder.HeldItem == default || !data.Context.Require(interactHolder.HeldItem, out CItem item))
                return false;

            if (provider.ProvidedItem == item.ID)
                return false;

            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            data.Context.Set(data.Target, default(CIsOnFire));
        }
    }
}
