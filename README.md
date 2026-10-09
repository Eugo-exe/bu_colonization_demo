# Colonization assignment checkpoint

**In progress — not submission-ready.** Iroquois artwork is provisional, three generated alternatives are awaiting approval and complete saved files, and Game view, the submission gameplay route and live MCP connectivity are untested.

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

Game view rendering and the submission route (landing, forest clearing, town creation and unit dragging) remain unverified. Coplay package import does not prove a live MCP connection. The requested package is pinned to commit `aa5fc638d623f56178d50329d9ad8c541a57fe66`. No credentials, login or persistent MCP configuration were created.

This is an assignment checkpoint for review and continuation. It is not a completed submission or a claim that the outstanding checks passed.
