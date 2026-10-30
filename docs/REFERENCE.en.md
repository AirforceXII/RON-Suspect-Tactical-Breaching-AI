# MLO squad breaching: field reference

This guide describes the stage12 Sins asset configuration: **stack up → kick → enable the Stinger throw branch → enter**. It does not incorporate the later 2–4-person grouping, timeout, ram, or C2 experiments.

The detailed [Chinese walkthrough](UASSETGUI-GUIDE.zh-CN.md) contains real UAssetGUI screenshots. These are current captures of the existing result assets, not screenshots taken during a new gameplay test.

## Find the actual map entry point

For Sins of the Father, the relevant spawn configurations use `AIDataTable_Hotel` rows `sp_m_shooter_a` and `sp_m_shooter_c`, archetype `Suspect_Terrorist`, and `DefaultSquadRules`. Do not infer a map's data table from the table's name. The 41 suspect spawn records are candidates, not a promise that every run spawns 41 suspects.

For a different map, trace its actual row, archetype, squad names, and rules before applying this pattern. Shared assets can affect other consumers, but this does not establish that all maps work.

## The changes

All paths below are relative to `ReadyOrNot/Content/`.

| Asset | Edit |
| --- | --- |
| `Blueprints/AI/Squads/DefaultSquadRules` | Preserve the original 3 `SquadActivityData` entries. Copy the official BreachandClear and SoloBreach entries from `MLO_SquadRules`, resulting in 5. The field inside each entry is `SquadsActivityClass`. |
| `Blueprints/AI/Archetypes/Core/Suspect_Terrorist` | Preserve the original 7 `SquadActions`, including existing Support. Copy Leader and Solo mappings from `Suspect_Tactical`, resulting in 9. |
| `Blueprints/DataTables/AI/AIDataTable_Hotel` | Change only the two specified rows: add 3 breach role tags, set 8 mesh entries per row to `TowerMLO_01`, and add 3 Stingers to every loadout. |
| `Blueprints/AI/ActionPresets/AP_Breach_Leader`, `AP_Breach_Follower`, `AP_SoloBreach` | Optional testing change: root `Action.Weight` 5→20. Existing gates, considerations, cooldowns, and action-end logic remain unchanged. |
| `Blueprints/AI/Squads/SquadActivities/BP_Squad_BreachandClear` | Change the `SetCanUseItem` function body to assign true and return, preserving the original downstream state machine. |

The added action mappings are:

```text
Squads.BreachAndClear.Leader → AP_Breach_Leader.AP_Breach_Leader
Squads.BreachAndClear.Solo   → AP_SoloBreach.AP_SoloBreach
```

The existing `Squads.BreachAndClear.Support` mapping points to `AP_Breach_Follower`. A copied entry's descriptive `Name` may still contain “Leader”; the actual `ActionTag` and `Preset` pairing determines the mapping.

The two role containers retain their original 5 tags and append:

```text
Squads.BreachAndClear.Leader
Squads.BreachAndClear.Support
Squads.BreachAndClear.Solo
```

## What item lets suspects throw?

The existing game grenade class is added to **`WeaponLoadouts → each loadout → Grenades`**:

```text
/Game/Blueprints/Items/WeaponsRevised/Grenade_Stinger_V2.Grenade_Stinger_V2_C
```

Soft-reference fields:

```text
PackageName: /Game/Blueprints/Items/WeaponsRevised/Grenade_Stinger_V2
AssetName:   Grenade_Stinger_V2_C
SubPath:     empty
```

`sp_m_shooter_a` has 3 alternative loadouts and `sp_m_shooter_c` has 2. Each receives 3 references, for 15 references across 5 alternatives. This does not mean every suspect gets 15 grenades. Primary firearms, secondary firearms, and `ForcedAIItems` were not changed to enable these throws; no launcher was added.

The original Tower rows have 3 Flashbang references per loadout. Their array structure and quantity were used as the template, while the target Hotel rows received Stinger references. The global Flashbang class was not replaced.

## UAssetGUI and compiled Blueprint logic

The actual captures use UAssetGUI v1.1.0, engine selection 5.3, and the 1.5.3 usmap. Preserve `.uasset/.uexp` pairs and the full dependency tree. The original build pipeline parsed and rewrote these using UE5.3 rules.

UAssetGUI can locate and display ordinary fields, object references, and compiled bytecode data. It is not the Unreal Editor Blueprint graph editor. The original implementation used typed UAssetAPI operations for complete cross-package structures and the function rewrite.

Changing the CDO's `CanUseItem` default alone is insufficient: the original `SetCanUseItem` function calculates it again. The stage12 function was reduced to:

```text
EX_LetBool(original CanUseItem field pointer, EX_True)
EX_Return(EX_Nothing)
EX_EndOfScript
```

The result has 3 expressions instead of 14, with `ScriptBytecodeSize` 390→14 for this exact version. The original Ubergraph, metadata, other functions, and kick method `Kick=3` were preserved. See the [typed edit fragment](../examples/enable-grenade-branch.cs). Do not reproduce this by editing a fixed offset or only changing the size field.

The downstream flow still sets `SetUseGrenade(CanUseItem)` and uses the original `PerformBreachAndUseItemStage`. The original squad logic chooses the thrower. Stock, execution conditions, and interruptions still apply; enabling the branch is not a guarantee of a successful throw on every door. Independent SoloBreach and normal push/pull interactions were not rewritten.

## How the AI folders relate

Spawn configuration selects the data-table row and squad rules. The row supplies the archetype, role eligibility, speech, and equipment. The archetype maps role tags to action presets; presets reference gates, considerations, and the action implementation. The squad manager selects an activity, which coordinates with personal action/activity objects.

`BP_Squad_BreachandClear` is squad-level, `BP_BreachAndClearActivity` is personal, and `KickDoorActivity` is a more specific execution activity. A loaded class or a stored reference is not proof that it is the current executing activity.

`Gates` concern eligibility, `Considerations` provide evaluation input, `CombatMoves` provide movement behavior, and `FactionManagers` concern factions. These folders are not executed sequentially. Their assets were not edited for this stage. See the [full folder explanation](AI-FOLDERS.zh-CN.md).

## Verification scope

Earlier Sins high-weight testing captured actual squad activity progression and current `KickDoorActivity` samples, with matching target-door identities; the player later confirmed a visible kick in replay. Stage12 equipment, bytecode, and package contents passed offline checks.

This tutorial does not turn that evidence into a claim that every member stacks up, throws, enters, or clears successfully. It also does not transfer the separate UE4SS counter-wallbang mod's multiplayer result to this asset-based breaching package. See the [evidence notes](EVIDENCE.zh-CN.md).
