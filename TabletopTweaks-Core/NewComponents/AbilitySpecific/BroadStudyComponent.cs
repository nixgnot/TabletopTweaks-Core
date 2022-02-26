﻿using Kingmaker.Blueprints;
using Kingmaker.UnitLogic;
using TabletopTweaks.Core.NewUnitParts;

namespace TabletopTweaks.Core.NewComponents.AbilitySpecific {
    public class BroadStudyComponent : UnitFactComponentDelegate {
        public override void OnTurnOn() {
            base.Owner.Ensure<UnitPartBroadStudy>().AddEntry(CharacterClass, base.Fact);
        }

        public override void OnTurnOff() {
            base.Owner.Ensure<UnitPartBroadStudy>().RemoveEntry(base.Fact);
        }

        public BlueprintCharacterClassReference CharacterClass;
    }
}