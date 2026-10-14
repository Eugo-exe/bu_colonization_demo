# Land unit artwork: sources and historical scope

Reviewed 14 October 2026. The assignment covers 1500–1700; the original `week4UnitIcon.pptx` describes its land illustrations as concepts aimed at approximately 1600–1650. All 20 PNGs in `Assets/Textures/AssignmentArtwork/LandUnits` are byte-identical to the corresponding embedded images in that deck. This update reuses those images; it generates no new images, flags, insignia or portraits.

The deck's Notes distinguish period context from a verified costume reconstruction. That distinction is retained here. Sources below support the existence and context of occupations, tools and activities. They do not authenticate every garment, face, tool shape or pose in these stylized 256 × 256 illustrations, or establish that one costume represents all five European factions. The 15 added entries carry no flags inside their artwork; the prefab continues to display the faction's existing shield separately.

## Review of the 15 newly connected images

| Image / game role | Evidence used | Interpretation and limits |
| --- | --- | --- |
| Scout / Scouts | The assignment specifies a mounted scout. Research on early Jamestown horse remains documents transatlantic horse movement in the early 1600s [H]. | European mounted explorer concept, not a claim about a particular Indigenous nation, horse breed or regiment. Horse equipment is enabled in its data. |
| Farmer | New France's foodways include cultivated cereals and vegetables alongside hunting and fishing [F]. | Hoe and harvested crop communicate agriculture. The image is not evidence that all farm workers were women or wore this outfit. |
| Fisherman | NPS identifies a fisherman/sailor and fish merchant on Smith's 1608 voyages [V]. | Net and catch communicate coastal fishing. It remains a land unit; it cannot stand on open sea without the existing boarding rules. |
| Lumberjack | Jamestown archaeology documents continuing building work in 1607–1624 [J]; its early industries also used timber [T]. | Axe and timber work are period context. No modern powered logging equipment is depicted. The exact axe and clothing are illustrative. |
| Carpenter | NPS records carpenters in the 1608 crew [V] and requests for carpenters in 1630s Virginia [T]. | Mallet, plane and apron distinguish woodworking. No new production bonus or building system is inferred from the image. |
| Ore Miner | NPS records iron ore discoveries and samples sent from Virginia in 1630 [T]. | Pick and ore basket are occupational shorthand. This does not establish that every mountain contains ore or reproduce a specific mine. |
| Blacksmith | NPS identifies a blacksmith in the 1608 crew [V] and smiths requested in the 1630s [T]. | Hammer, tongs and apron identify metalwork; an entire forge is intentionally absent from a unit icon. |
| Fur Trapper | The Canadian Museum of History describes hunting, trapping, processing and Indigenous trade networks in New France [R]. | Pelts and cord identify the role. The European-looking figure does not represent the ethnicity of all trappers or erase Indigenous participation. |
| Fur Trader | The same museum documents French and Dutch settlements linked to existing fur networks in the early 1600s [R]. | Pelt bundle and account book distinguish trade from trapping. No specific merchant or national uniform is asserted. |
| Cotton Planter | The Met's textile history records cotton and textile commerce in sixteenth-century Mexico [C]. | Cotton basket is a game resource cue. The European-style figure is not a reconstruction of an Indigenous Mexican worker, proof of a worker's legal status, or a nineteenth-century cotton plantation scene. |
| Cotton Weaver | The Met documents textile exchange from 1500 onward [C] and precolonial Mexican cotton weaving [W]. | Hand shuttle and folded cloth communicate handwork. No industrial power loom is depicted. The later serape illustrated on [W] is not used as a costume reference. |
| Missionary | NPS discusses Franciscan Alonso de Benavides and his 1630 account [M]. | Brown habit and cord identify a Franciscan interpretation, not every Christian denomination or every European faction. The source also explains the colonial missionary's perspective and Indigenous resistance. |
| Statesman | The Jamestown source describes colonial governors and instructions to develop the settlement in the 1630s [T]. The Met preserves a doublet dated 1600 as a period clothing comparator [D]. | Documents, cloak and white collar identify a generic civil administrator. No named person's likeness, coat of arms or official uniform is claimed. |
| Gunsmith | Jamestown's excavated firearm equipment dates to the company period, 1607–1624 [G]; NPS explains contemporary weapons [A]. | Apron and a long firearm distinguish craft from the soldier role. The tiny painted mechanism is not a technical reconstruction of a specific lock. Carrying a workpiece does not enable combat equipment. |
| Criminal | Virginia Humanities describes the authorization of convict transportation in 1615 and early seventeenth-century practice [P]. | Patched clothing and restrained hands are a game cue, not a universal convict uniform. A convicted person is not synonymous with an indentured servant. No modern striped or orange prison outfit is used. |

## Sources checked

- [T] [NPS — Tradesmen on the Lot of Governor Harvey, 1630s](https://www.nps.gov/jame/learn/historyculture/new-towne-tradesmen-on-the-lot-of-governor-harvey-1630s.htm).
- [V] [NPS — John Smith's Chesapeake Voyages, 1608 crew](https://www.nps.gov/articles/000/john-smith-voyages.htm).
- [J] [Jamestown Rediscovery — excavated hammer, 1607–1624 context](https://historicjamestowne.org/collections/artifacts/hammer/).
- [F] [Canadian Museum of History — Foodways in New France](https://www.historymuseum.ca/virtual-museum-of-new-france/daily-life/foodways/).
- [R] [Canadian Museum of History — Fur Trade](https://www.historymuseum.ca/virtual-museum-of-new-france/economic-activities/fur-trade/).
- [C] [The Met — Interwoven Globe: The Worldwide Textile Trade, 1500–1800](https://www.metmuseum.org/exhibitions/listings/2013/interwoven-globe). Only the sixteenth/seventeenth-century context is relevant here; later industrial developments are outside the game's period.
- [W] [The Met — Saltillo Serape, historical background on cotton weaving](https://www.metmuseum.org/art/collection/search/318971). The object itself dates to 1860–1900 and is **not** used as a 1500–1700 visual reference.
- [M] [NPS — Friar Alonso de Benavides, 1630](https://www.nps.gov/subjects/travelspanishmissions/a-day-in-the-life-of-a-spanish-missionary-friar-alonso-de-benavides-1630.htm).
- [D] [The Met — Doublet, 1600, object 23.30.3](https://www.metmuseum.org/art/collection/search/98163). A period comparator, not the original model for the generated image.
- [G] [Jamestown Rediscovery — Musket Rests](https://historicjamestowne.org/collections/artifacts/musket-rests/).
- [A] [NPS — History of Armour and Weapons Relevant to Jamestown](https://www.nps.gov/jame/learn/historyculture/history-of-armour-and-weapons-relevant-to-jamestown.htm).
- [P] [Virginia Humanities / Encyclopedia Virginia — Convict Labor during the Colonial Period](https://encyclopediavirginia.org/entries/convict-labor-during-the-colonial-period/).
- [H] [Early transatlantic movement of horses and donkeys at Jamestown — original research](https://pmc.ncbi.nlm.nih.gov/articles/PMC12407088/). Search indexing exposed the early Jamestown context; the full-text site returned a browser challenge during this review. This source is not used to verify the pictured saddle or breed.

## Data values and preserved game structure

The supplied production manual's LandUnitData section specifies Strength 1 for nonsoldiers, Movement 3 for units without horses, and Visual Range 2 for scouts / 1 for others. The 15 added assets use Strength 1 and the existing baseline Movement 3. Scouts have Visual Range 2 and `hasHorse = true`; no mounted movement bonus is invented because the supplied section does not specify its value. The other new entries have Visual Range 1 and no combat equipment flags. Existing five data assets and their values are untouched.

The existing enum's values 0–11 are preserved explicitly. Eight occupation names are appended as 12–19, in original assignment order. Fifteen instances of the **same** LandUnitData class are appended to the **same** GameManager array in Map01. There are no new gameplay components, prefab hierarchies, starting units, production rules, recruitment systems or scene objects. Registering all artwork does not implement profession bonuses or recruitment that this demo does not have.

## Historical scope still outside this update

This is a contextual review of retained assignment art, not certification that every detail is historically reconstructed. Existing generic Indigenous land images, shared ship images containing English markings, and contemporary Native faction UI symbols are unchanged. In particular, those UI symbols must not be described as attested colonial-era flags. See the retained limitations in [project notes](docs/PROJECT_NOTES.md).
