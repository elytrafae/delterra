
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Delterra.Content.CustomModTypes.ModSetBonuses {
    public abstract class ModSetBonus : ModType, ILocalizedModType {
        public virtual string LocalizationCategory => "ModSetBonuses";

        public int Type { get; internal set; }

        public virtual LocalizedText SetBonusText => this.GetLocalization(nameof(SetBonusText), () => "");

        protected sealed override void Register() {
            ModTypeLookup<ModSetBonus>.Register(this);
            Type = ModSetBonusLoader.Add(this);
            _ = SetBonusText;
        }

        public abstract bool IsSetMatching(Item head, Item body, Item legs);
        public abstract void UpdateSetBonus(Player player);
    }
}
