# Colonization assignment checkpoint

**In progress — not submission-ready.** Live Coplay MCP connectivity and the gameplay interaction route were checked on 14 October 2026. Ship/passenger click routing and the hex's incomplete click area are fixed. A grid and legal-move highlights are available; the user confirmed a stationary right-only mouse click moves the ship. The personal Git destination is [Eugo-exe/bu_colonization_demo](https://github.com/Eugo-exe/bu_colonization_demo). All 20 land unit images are now connected to the existing data catalog. The artwork limitations below remain outstanding. See [interaction verification](../INTERACTION_VERIFICATION.md).

The committed project version remains **6000.3.19f1**. Earlier asset validation used Unity **6000.3.26f1** in a separate working copy with pre-existing local upgrades. Unrelated Unity-version, render-setting and package-version upgrades are excluded from this checkpoint. The exact published project tree has not been reimported with its declared editor version.

## Assignment contents

`Assets/Textures/AssignmentArtwork` contains 11 terrain PNGs, 7 forest PNGs, 20 land unit PNGs, 6 naval PNGs and 13 faction PNGs. Texture2D sources are imported as single Sprites. `asset-manifest.json` records provenance, GUIDs, dimensions and committed-byte hashes; text metadata hashes use LF line endings.

Runtime data contains 11 Hex SO, **20 Land SO**, 6 Naval SO and 13 Faction SO. All **20 land enum entries** have a distinct sprite and are registered in Map01 using the original LandUnitData class, GameManager array and LandUnit prefab. Values 0–11 remain stable; eight missing names are appended as 12–19. The original five land data assets and starting ship/passenger/native composition are unchanged. The new data does not add profession bonuses or a recruitment system. See [land integration verification](../LAND_UNIT_VERIFICATION.md) and [historical sources and scope](../LAND_UNIT_REFERENCES.md). Four additional naval SO remain registered in Map01.

## Artwork limitations

- England uses the St George cross, with the saved source record recorded in the manifest.
- Apache uses a contemporary regional desert landscape, not a historical flag or official tribal seal. Its GUID is preserved; 1254 px at PPU 489.84375 retains a 2.56-unit width.
- Iroquois retains **provisional contemporary belt/tree UI artwork**. It is not presented as a colonial-era or official flag. A longhouse alternative remains pending.
- Maya uses a contemporary maize UI motif, not a historical flag or official seal.
- Sioux is the existing game faction key. Its water/wild-rice UI motif has Dakota design context and is not presented as a universal Sioux flag.
- Naval art is shared across factions and retains **English markings inside the ship images**.
- Three generated alternatives are **waiting for approval and completed saves**. No partial transfer is included. Earlier SVG sources are excluded from this checkpoint.

All 20 land PNGs were compared byte-for-byte with the embedded images in the original `week4UnitIcon.pptx`. No land art was regenerated. A review of the 15 newly connected roles against period sources is recorded in [LAND_UNIT_REFERENCES.md](../LAND_UNIT_REFERENCES.md); these are contextual game illustrations, not verified reconstructions of every costume detail. Remaining retained source attribution is documented without a claim that every image is historically accurate.

## Verification scope and remaining work

Earlier working-copy batch verification completed at 2026-10-09 18:56:49 UTC: 57/57 Texture2D/Sprite imports, 11/5/6/13 data references, Map01 registration, inactive HexFog/HexDark, Coplay package import and the preserved Apache width passed with zero failures. This evidence applies to that validation copy; it does not establish gameplay, rendering, MCP connectivity or an import of the published tree with its declared editor version.

Live verification on 14 October 2026 used the open `Map01` scene in Unity **6000.3.26f1** through Coplay MCP. A regression check reproduced a passenger's blocked landing before the fix and passed afterward. Ship selection, sea movement, passenger cycling, completed landing, forest clearing, town construction, opening/closing the town panel and dragging a worker were checked with simulated Input System events. Game view screenshots were inspected at 1104 x 581. This does not establish a standalone build or compatibility with the declared **6000.3.19f1** editor version. Full details and test conditions are in [INTERACTION_VERIFICATION.md](../INTERACTION_VERIFICATION.md).

Follow-up testing reproduced missed clicks in the upper/lower parts of a hex: the original rectangular collider was only 1.4 units high. A six-point polygon now matches the 2.55-unit grid footprint. With normal focus/input settings restored and no virtual devices, a native right-button press/release recorded no held left button, zero drag distance, a legal highlighted destination, movement start and completed arrival. Green destination cells update when movement is exhausted. A zero-Movement passenger now remains aboard when a landing command is rejected.

The requested Coplay package is pinned to commit `aa5fc638d623f56178d50329d9ad8c541a57fe66`. No credentials, login or persistent MCP configuration were created.

## Controls

Click the **Game** view after entering Play mode so Unity receives keyboard/mouse input.

| Input | Action |
| --- | --- |
| Left click a friendly unit | Select that unit |
| Right click an adjacent hex | Move the selected unit if its movement points and terrain permit |
| G | Hide/show the faint grid; movement highlights remain visible |
| Tab | Cycle the ship and its passengers; passengers start hidden aboard the ship |
| Tab, then right click adjacent land | Disembark the selected passenger when the ship is beside the coast |
| P / Backspace | Order a pioneer to clear forest / finish the turn |
| B / Backspace | Order a pioneer to build on cleared land / finish the turn |
| Enter / Escape | Open the town under the selected unit / close the town panel |

This demo uses those controls; it has no general ship interaction menu.

Fog of war is now enabled in the saved `Assets/Prefabs/Hex.prefab`: both `HexFog` and `HexDark` are active. Starting a new Play session hides unexplored hexes and reveals the player unit's position and neighbouring hexes. Previously visited areas outside current sight use the lighter fog. This setting is stored in the prefab used by Map01, rather than in temporary generated Play-mode hexes. Earlier all-visible verification describes the prior configuration. Backspace ends the turn; wait for AI movement to finish before moving again.

The cyan hex is the current unit's position. Green outlines/fills mark adjacent destinations affordable for that unit; their numbers show movement cost. A red hovered hex is not a movement destination. When Movement reaches zero, press Backspace to finish the turn. The overlay is noninteractive and hides while the town panel is open. The top-left unit/Movement/help panel was removed at the user's request; grid lines, current-hex outlines, legal-move highlights and movement-cost numbers remain.

## Personal repository

The `student` remote points to `https://github.com/Eugo-exe/bu_colonization_demo.git`; `main` tracks `student/main`. Push subsequent committed changes with `git push student main`. The original `origin` remote is retained as the source reference. Use the personal repository URL for the assignment handoff.

This is an assignment checkpoint for review and continuation. It is not a completed submission or a claim that the outstanding checks passed.
