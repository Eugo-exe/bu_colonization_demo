# Ship and unit interaction verification

Verified on 14 October 2026 in the open `Map01` scene using Unity 6000.3.26f1 and Coplay MCP. The change fixes passenger landing when the ship artwork overlaps a neighbouring hex. Only `Assets/Scripts/Units/Unit.cs` changes gameplay code; scene hierarchy, prefabs, asset references, unit stats and gameplay types are unchanged by this fix.

## Cause and fix

The ship's sprite collider extends over adjacent terrain. With a passenger selected using Tab, right clicking that terrain can hit the ship first. The previous handler only resolved the hex beneath the sprite when the clicked unit itself was selected. It checked the ship's own hex against the passenger's hex, found no adjacent destination and did nothing.

The handler now also resolves clicks received by another unit on the selected unit's hex. It identifies the underlying hex first, forwards only to another unit located on that destination hex, and otherwise orders the selected unit to move there. Units on the passenger's original hex no longer intercept the landing command.

## Results

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

Remote tests temporarily enabled runtime Run in Background and IgnoreFocus input behavior because Unity was behind the chat window. These test settings and virtual input devices are restored/removed at the end. Tests used simulated input, not physical mouse operation. Coastal placement was test setup; an entire voyage from the initial spawn was not played. Game view and the town panel were inspected at 1104 x 581; other resolutions and standalone builds were not tested in this run.

The one-off regression snippet and screenshots were kept in ignored `Temp/InteractionDiagnostics` during this session. They are diagnostic files, not game assets.

## Assignment asset audit

All 57 manifest PNGs matched their recorded SHA-256 hashes and imported as Sprites in the live editor: 11 terrain, 7 forest, 20 land unit, 6 naval unit and 13 faction images. No missing Sprite imports were found. The running map contained all 11 terrain types and both forest/no-forest versions of each of the seven forest-capable terrains. All six naval icons and all 13 faction flag/shield references were present in the registered data.

The existing game registers only **5 land unit data assets** and has **12 land enum entries**. Importing 20 land pictures does not make 20 playable unit types; 15 land pictures are not mapped to a registered land unit. This remains a submission gap if every picture must appear during gameplay. Adding professions, recruitment or a display scene would change the supplied game's scope and is not included in this pointer fix. The artwork provenance/approval limitations in README also remain.

## Git delivery

The student's private destination is [Eugo-exe/bu_colonization_demo](https://github.com/Eugo-exe/bu_colonization_demo). The local `student` remote points there, and `main` tracks `student/main`. The original `origin` (`watcharayui/bu_colonization_demo`) is retained as a source reference; it returned Repository not found with the current credentials and received no upload. The personal destination contains the assignment artwork checkpoint, the earlier pointer/town control fixes, this passenger landing fix, and the documentation. Unrelated pre-existing Unity/package/render setting changes and untracked SVG sources are outside these commits.
