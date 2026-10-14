# Land unit integration verification

Verified 14 October 2026 in the open `Assets/Scenes/Map01.unity` project through Coplay MCP, Unity **6000.3.26f1**. Scope: connect the 15 remaining land illustrations using the existing game structure, preserving starting units. This is not a standalone-build test or a reimport with the committed **6000.3.19f1** editor.

## Before and after

Before the change, a live Unity audit found 5 registered LandUnitData assets, 12 enum entries and 15 sprites absent from the runtime catalog. It returned `passed = false` for the requirement to register all 20.

After the change, Unity loaded **20 LandUnitData assets, 20 enum entries and 20 Map01 references**, with unique sprites, matching enum/index values, valid imported Sprite references and no missing entries. The scene was not dirty after import. Enum values 0–11 retain their original meanings, and the original five ScriptableObjects are unchanged.

## Runtime prefab and interaction checks

Each of the 20 entries was instantiated temporarily from the original `Assets/Prefabs/Units/LandUnit.prefab`, initialized with its real catalog data and placed on an empty land hex. For each entry, the following checks passed:

- Renderer uses the exact assigned sprite; name, type, Strength, Movement, Visual Range and horse equipment match its data.
- The sprite's pointer collider matches its bounds and is enabled.
- An EventSystem raycast at the sprite center resolves to that unit.
- Dispatching the left `pointerClick` event through the raycast hierarchy selects it.
- An adjacent land destination appears in the movement overlay's legal destinations.
- Dispatching only a right `pointerClick` event at that destination starts movement to the correct hex.

These are **simulated Unity UI events**, not a new physical mouse test. For all 20 entries this test checks movement initiation; it does not wait for each one's animation to finish.

| ID | Enum | Sprite | Prefab / selection / movement-start checks |
| --- | --- | --- | --- |
| 0 | FreeColonists | FreeColonist | Pass |
| 1 | VeteranSoldiers | Soldier | Pass |
| 2 | HardyPioneers | HardyPioneer | Pass |
| 3 | PlainIndians | PlainIndians | Pass |
| 4 | TropicalIndians | TropicalIndians | Pass |
| 5 | SeasonedScouts | Scout | Pass |
| 6 | Farmers | Farmer | Pass |
| 7 | Fishermen | Fisherman | Pass |
| 8 | Lumberjacks | Lumberjack | Pass |
| 9 | Carpenters | Carpenter | Pass |
| 10 | OreMiners | OreMiner | Pass |
| 11 | Blacksmiths | Blacksmith | Pass |
| 12 | FurTrappers | FurTrapper | Pass |
| 13 | FurTraders | FurTrader | Pass |
| 14 | CottonPlanters | CottonPlanter | Pass |
| 15 | CottonWeavers | CottonWeaver | Pass |
| 16 | Missionaries | Missionary | Pass |
| 17 | Statesmen | Statesman | Pass |
| 18 | Gunsmiths | Gunsmith | Pass |
| 19 | Criminals | Criminal | Pass |

A separate scout test ran through the normal frame updates until arrival: from hex `(3,8)` to `(2,9)`, cost 2, Movement **3 → 1**, `IsMoving = false`, destination reached. Its horse count was 50 and Visual Range 2. A 1104 × 581 Game view capture was inspected: the original sprite/flag footprint, scout image, unit name and movement highlights rendered. A contact sheet of all 20 source PNGs was also inspected; this does not replace individual in-game visual inspection of every pose at every zoom level.

All temporary units were removed. The player's unit count was **3 before and 3 after**; every European faction still had its starting ship and two passengers. The previous selection and camera position were restored, `Application.runInBackground` was restored to false, only the two native input devices remained, and no temporary test object remained. Play mode was left running. Final Play-mode Console read returned **0 errors / 0 warnings**. An earlier refresh wait timed out and emitted a Coplay warning; subsequent editor state, compilation and runtime checks succeeded.

## Static and source checks

- Removing only the 15 appended land references makes Map01 match its previous committed text exactly. No scene hierarchy or other serialized block changed.
- The LandUnit class body, GameManager, LandUnitData class and LandUnit prefab match the previous commit. Only enum declarations changed in gameplay code.
- Original five land assets are unchanged; all 15 new asset GUIDs are unique across Assets metadata.
- All **57 PNG hashes** still match the asset manifest. All **20 land PNGs** additionally match the embedded media bytes in the original `week4UnitIcon.pptx`.
- New data uses the manual's nonsoldier baseline with the documented scout exception. Historical context and its limits are recorded in [LAND_UNIT_REFERENCES.md](LAND_UNIT_REFERENCES.md).
- A separate read-only code/data review reported **no actionable findings**. That reviewer did not run Unity or independently validate historical sources.

Local diagnostic code, JSON reports and screenshots are in the Git-ignored `Temp/InteractionDiagnostics` directory. This document preserves the test scope for GitHub review without publishing temporary Unity data.

## How to inspect the added content

Stop Play mode, select the scene's GameManager and expand **Land Unit Data**, or inspect `Assets/Scripts/LandUnitSO/005_SeasonedScouts.asset` through `019_Criminals.asset`. Each references a different retained assignment sprite. As approved, adding the catalog does **not** spawn 15 extra units at game start or add a recruitment/profession system. Default play still begins with the same ship and passengers.
