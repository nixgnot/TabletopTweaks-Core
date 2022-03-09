﻿using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using TabletopTweaks.Core.Utilities;
using static TabletopTweaks.Core.Main;

namespace TabletopTweaks.Core.Bugfixes.Units {
    static class Bosses {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            static void Postfix() {
                if (Initialized) return;
                Initialized = true;
                TTTContext.Logger.LogHeader("Patching Bosses");
                PatchStauntonVane();
            }
        }
        static void PatchStauntonVane() {
            if (TTTContext.Fixes.Units.Bosses.IsDisabled("StauntonVane")) { return; }

            var StauntonVane_Boss = BlueprintTools.GetBlueprint<BlueprintUnit>("88f8535a8db0154488d5e72d74e0e466");
            var WarpriestClass = BlueprintTools.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");
            var WarpriestFeatSelection = BlueprintTools.GetBlueprintReference<BlueprintFeatureSelectionReference>("303fd456ddb14437946e344bad9a893b");
            var FighterFeatSelection = BlueprintTools.GetBlueprintReference<BlueprintFeatureSelectionReference>("41c8486641f7d6d4283ca9dae4147a9f");

            StauntonVane_Boss
                .GetComponent<AddClassLevels>(c => c.m_CharacterClass.Equals(WarpriestClass))?
                .GetSelection(c => c.m_Selection.Equals(FighterFeatSelection))?
                .TemporaryContext(c => {
                    c.m_Selection = WarpriestFeatSelection;
                });
            TTTContext.Logger.LogPatch(StauntonVane_Boss);
        }
    }
}
