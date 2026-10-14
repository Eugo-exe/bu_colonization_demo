# Ship movement and grid verification

Verified on 14 October 2026 in the open `Map01` scene using Unity 6000.3.26f1 and Coplay MCP. The latest change fixes the hex's incomplete click area and adds the user-approved movement grid/HUD to the existing UIManager. The Hex prefab's collider changes from a small rectangle to a full hexagon; its child hierarchy is preserved. Scene hierarchy, artwork references, unit stats, starting spawns and gameplay types are retained. Terrain/budget validation is shared between move execution and the overlay.

## Cause and fix

Native mouse capture established that real button events and UI action callbacks reached the game. The old Hex prefab used a 2.4 x 1.4 BoxCollider2D inside a roughly 2.55 x 2.55 visible hex. The upper/lower visible areas were outside every hex collider, so clicks there had no terrain target. The collider is now a six-point polygon matching the existing grid footprint, without enlarging it into neighbouring cells.

Faint grid edges show those click areas. The current hex is cyan, eligible adjacent destinations are green with movement costs, and an ineligible hovered hex is red. A noninteractive HUD shows the selected unit, movement remaining, boarding context and controls; G toggles the background grid. The grid/HUD hides with the town panel. It does not add pathfinding or change the one-adjacent-hex command.

An additional regression showed a passenger with zero movement could detach from its ship even though the base movement command was rejected. Shared `CanMoveToHex` checks now prevent this state change and keep the green destinations consistent with movement cost and terrain.

The earlier ship/passenger routing fix is retained:

The ship's sprite collider extends over adjacent terrain. With a passenger selected using Tab, right clicking that terrain can hit the ship first. The previous handler only resolved the hex beneath the sprite when the clicked unit itself was selected. It checked the ship's own hex against the passenger's hex, found no adjacent destination and did nothing.

The handler now also resolves clicks received by another unit on the selected unit's hex. It identifies the underlying hex first, forwards only to another unit located on that destination hex, and otherwise orders the selected unit to move there. Units on the passenger's original hex no longer intercept the landing command.

## Latest results

| Check | Result |
| --- | --- |
| Hex centre and upper/lower interior points | Before: two misses; after: all three clickable |
| All six interior hex corners | Clickable; an outside bounding-box corner remains excluded |
| Passenger with insufficient Movement | Before: detached without moving; after: stays OnBoard with transport retained |
| Ship green destinations | Adjacent, affordable ocean cells; visible enemy cells excluded |
| Movement zero, distant cells, AI turn | No inappropriate green destinations |
| Separate left selection, then stationary right-only click in upper hex interior | Passed through InputSystemUIInputModule; ship reached the intended destination |
| G grid toggle | Hide and show both passed |
| Native mouse test after restoring default input/focus settings | Passed; user also confirmed single-right-click movement |
| Movement exhaustion during user play | Remaining Movement reached zero; green destinations disappeared |
| Compilation / final gameplay console | No errors or warnings returned |
| Code review | No actionable introduced findings; reviewer did not control Unity |

The native recorder captured the actual button release and subsequent game frames:

```text
NATIVE RIGHT RELEASE leftHeld=False dragPixels=0 green=True from=49,22 pointed=48,21
MOVEMENT STARTED
ARRIVED hex=48,21 remaining=2
```

Simulated tests temporarily used Run in Background, IgnoreFocus, and AllDeviceInputAlwaysGoesToGameView to route test devices while Codex was foreground. All were restored before native verification: Run in Background false, ResetAndDisableNonBackgroundDevices, PointersAndKeyboardsRespectGameViewFocus, and only native devices remaining. No persistent input setting changes are included. The movement overlay was visually inspected at 1104 x 581; standalone builds and other viewport sizes were not tested in this run.

## Earlier gameplay checks

These checks were performed before the grid addition and document the earlier interaction/town fixes; the entire town sequence was not repeated for the latest visual change.

The regression fixture placed the player's ship beside legal land whose centre was inside the ship's collider, selected its pioneer passenger, and sent a right press/release through `InputSystemUIInputModule.Process`. It used the real scene, colliders, EventSystem and movement code, without directly calling the unit's click handler.

| Check | Result |
| --- | --- |
| Before fix: landing through overlapping ship sprite | Failed as expected: passenger stayed OnBoard, did not move, and retained its transport |
| After fix: same overlapping-collider scenario | Passed: correct target, movement started, status None, transport released |
| Landing completes in subsequent game frames | Passed: passenger reached land |
| Left click switches from passenger to ship | Passed |
| Right click moves selected ship to adjacent ocean | Passed; ship reached the destination |
| Tab cycles to the hidden soldier and pioneer passengers | Passed |
| P orders forest clearing; Backspace completes the turn | Passed: tools decreased from 100 to 80 and the forest cleared |
| B orders a town; Backspace completes the turn | Passed: tools decreased to 60 and the town was created |
| Enter opens the town; Escape closes it | Passed |
| Drag a worker onto a surrounding terrain slot | Passed through Input System/EventSystem: labor assigned, status WorkInField, icon reparented |
| Compilation and final Unity console check | No compilation errors or gameplay warnings/errors returned |
| Whitespace check for the changed C# file | Passed |

Those earlier checks used simulated input and coastal placement as test setup. An entire voyage from the initial spawn was not played. Game view and the town panel were inspected at 1104 x 581.

The one-off regression snippet and screenshots were kept in ignored `Temp/InteractionDiagnostics` during this session. They are diagnostic files, not game assets.

## Assignment asset audit

All 57 manifest PNGs matched their recorded SHA-256 hashes and imported as Sprites in the live editor: 11 terrain, 7 forest, 20 land unit, 6 naval unit and 13 faction images. No missing Sprite imports were found. The running map contained all 11 terrain types and both forest/no-forest versions of each of the seven forest-capable terrains. All six naval icons and all 13 faction flag/shield references were present in the registered data.

The existing game registers only **5 land unit data assets** and has **12 land enum entries**. Importing 20 land pictures does not make 20 playable unit types; 15 land pictures are not mapped to a registered land unit. This remains a submission gap if every picture must appear during gameplay. Adding professions, recruitment or a display scene would change the supplied game's scope and is not included in this pointer fix. The artwork provenance/approval limitations in README also remain.

## Git delivery

The student's personal destination is [Eugo-exe/bu_colonization_demo](https://github.com/Eugo-exe/bu_colonization_demo). The local `student` remote points there, and `main` tracks `student/main`. The original `origin` (`watcharayui/bu_colonization_demo`) is retained as a source reference; it returned Repository not found with the current credentials and received no upload. The personal destination contains the assignment artwork checkpoint, the earlier pointer/town control fixes, this passenger landing fix, and the documentation. Unrelated pre-existing Unity/package/render setting changes and untracked SVG sources are outside these commits.
