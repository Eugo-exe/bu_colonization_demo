# Colonization assignment checkpoint

**In progress — not submission-ready.** Live Coplay MCP connectivity and the gameplay interaction route were checked on 14 October 2026. The ship/passenger click obstruction is fixed. The personal Git destination is [Eugo-exe/bu_colonization_demo](https://github.com/Eugo-exe/bu_colonization_demo), a private repository. The unused land artwork and the artwork limitations below remain outstanding. See [interaction verification](INTERACTION_VERIFICATION.md).

The committed project version remains **6000.3.19f1**. Earlier asset validation used Unity **6000.3.26f1** in a separate working copy with pre-existing local upgrades. Unrelated Unity-version, render-setting and package-version upgrades are excluded from this checkpoint. The exact published project tree has not been reimported with its declared editor version.

## Assignment contents

`Assets/Textures/AssignmentArtwork` contains 11 terrain PNGs, 7 forest PNGs, 20 land unit PNGs, 6 naval PNGs and 13 faction PNGs. Texture2D sources are imported as single Sprites. `asset-manifest.json` records provenance, GUIDs, dimensions and committed-byte hashes; text metadata hashes use LF line endings.

Runtime data remains 11 Hex SO, **5 Land SO**, 6 Naval SO and 13 Faction SO. The land enum has **12 entries**. **20 land images do not mean 20 playable unit types.** Four additional naval SO are registered in Map01, while existing initial Caravel spawning logic, gameplay scripts, equipment flags and existing combat/movement values remain unchanged.

## Artwork limitations

- England uses the St George cross, with the saved source record recorded in the manifest.
- Apache uses a contemporary regional desert landscape, not a historical flag or official tribal seal. Its GUID is preserved; 1254 px at PPU 489.84375 retains a 2.56-unit width.
- Iroquois retains **provisional contemporary belt/tree UI artwork**. It is not presented as a colonial-era or official flag. A longhouse alternative remains pending.
- Maya uses a contemporary maize UI motif, not a historical flag or official seal.
- Sioux is the existing game faction key. Its water/wild-rice UI motif has Dakota design context and is not presented as a universal Sioux flag.
- Naval art is shared across factions and retains **English markings inside the ship images**.
- Three generated alternatives are **waiting for approval and completed saves**. No partial transfer is included. Earlier SVG sources are excluded from this checkpoint.

Remaining retained source attribution is documented without a new claim that every image is historically accurate.

## Verification scope and remaining work

Earlier working-copy batch verification completed at 2026-10-09 18:56:49 UTC: 57/57 Texture2D/Sprite imports, 11/5/6/13 data references, Map01 registration, inactive HexFog/HexDark, Coplay package import and the preserved Apache width passed with zero failures. This evidence applies to that validation copy; it does not establish gameplay, rendering, MCP connectivity or an import of the published tree with its declared editor version.

Live verification on 14 October 2026 used the open `Map01` scene in Unity **6000.3.26f1** through Coplay MCP. A regression check reproduced a passenger's blocked landing before the fix and passed afterward. Ship selection, sea movement, passenger cycling, completed landing, forest clearing, town construction, opening/closing the town panel and dragging a worker were checked with simulated Input System events. Game view screenshots were inspected at 1104 x 581. This does not establish a standalone build or compatibility with the declared **6000.3.19f1** editor version. Full details and test conditions are in [INTERACTION_VERIFICATION.md](INTERACTION_VERIFICATION.md).

The requested Coplay package is pinned to commit `aa5fc638d623f56178d50329d9ad8c541a57fe66`. No credentials, login or persistent MCP configuration were created.

## Controls

Click the **Game** view after entering Play mode so Unity receives keyboard/mouse input.

| Input | Action |
| --- | --- |
| Left click a friendly unit | Select that unit |
| Right click an adjacent hex | Move the selected unit if its movement points and terrain permit |
| Tab | Cycle the ship and its passengers; passengers start hidden aboard the ship |
| Tab, then right click adjacent land | Disembark the selected passenger when the ship is beside the coast |
| P / Backspace | Order a pioneer to clear forest / finish the turn |
| B / Backspace | Order a pioneer to build on cleared land / finish the turn |
| Enter / Escape | Open the town under the selected unit / close the town panel |

This demo uses those controls; it has no general ship interaction menu.

## Personal repository

The `student` remote points to `https://github.com/Eugo-exe/bu_colonization_demo.git`; `main` tracks `student/main`. Push subsequent committed changes with `git push student main`. The original `origin` remote is retained as the source reference. The personal repository is private, so an instructor needs repository access to review a submitted link.

This is an assignment checkpoint for review and continuation. It is not a completed submission or a claim that the outstanding checks passed.
