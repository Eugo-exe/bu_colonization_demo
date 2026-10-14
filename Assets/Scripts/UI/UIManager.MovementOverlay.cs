using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class UIManager
{
    [SerializeField] private bool showMovementGrid = true;
    public bool ShowMovementGrid { get { return showMovementGrid; } }

    private Texture2D movementHexFill;
    private GUIStyle movementTitleStyle;
    private GUIStyle movementTextStyle;
    private GUIStyle movementHintStyle;
    private GUIStyle movementCostStyle;

    private static readonly Color ReachableColor = new Color(0.35f, 1f, 0.58f);
    private static readonly Color CurrentHexColor = new Color(0.28f, 0.83f, 1f);
    private static readonly Vector2[] MovementHexCorners =
    {
        new Vector2(0, 0.5f), new Vector2(0.5f, 0.25f), new Vector2(0.5f, -0.25f),
        new Vector2(0, -0.5f), new Vector2(-0.5f, -0.25f), new Vector2(-0.5f, 0.25f)
    };

    public List<Hex> GetMoveDestinations(Unit unit)
    {
        var destinations = new List<Hex>(6);
        var gm = GameManager.instance;
        if (gm == null || unit == null || unit.CurHex == null || !gm.PlayerTurn
            || unit.Faction != gm.PlayerFaction || unit.IsMoving || !unit.gameObject.activeInHierarchy)
            return destinations;

        foreach (Hex hex in HexCalculator.GetHexAround(gm.AllHexes, unit.CurHex))
        {
            if (!unit.CanMoveToHex(hex))
                continue;

            // Clicking a visible enemy invokes the existing attack handler, not movement.
            bool enemyPresent = false;
            foreach (Faction faction in gm.Factions)
            {
                if (faction == gm.PlayerFaction)
                    continue;
                foreach (Unit other in faction.Units)
                {
                    if (other.CurHex == hex && other.gameObject.activeInHierarchy
                        && other.UnitSprite.gameObject.activeInHierarchy)
                    {
                        enemyPresent = true;
                        break;
                    }
                }
                if (enemyPresent)
                    break;
            }
            if (!enemyPresent)
                destinations.Add(hex);
        }
        return destinations;
    }

    private void OnGUI()
    {
        var gm = GameManager.instance;
        var cam = Camera.main;
        if (Event.current.type != EventType.Repaint || gm == null || cam == null
            || gm.CurUnit == null || gm.CurUnit.CurHex == null || townPanel.activeSelf)
            return;

        EnsureMovementOverlayStyles();
        Unit unit = gm.CurUnit;
        List<Hex> destinations = GetMoveDestinations(unit);
        Hex hovered = GetHoveredHex(cam);
        Vector2 size = gm.HexCellSize;

        if (showMovementGrid)
        {
            foreach (Hex hex in gm.AllHexes)
            {
                if (hex == null)
                    continue;
                Rect rect = HexScreenRect(hex, cam, size);
                if (rect.xMax < 0 || rect.yMax < 0 || rect.xMin > Screen.width || rect.yMin > Screen.height)
                    continue;
                DrawHexOutline(hex, cam, size, new Color(0.83f, 0.94f, 1f, 0.25f), 1f, true);
            }
        }

        foreach (Hex hex in destinations)
        {
            Rect rect = HexScreenRect(hex, cam, size);
            DrawOverlayTexture(rect, movementHexFill, new Color(0.16f, 0.95f, 0.39f, hex == hovered ? 0.34f : 0.16f));
            DrawHexOutline(hex, cam, size, ReachableColor, hex == hovered ? 3f : 2f, false);
            Vector2 center = ScreenGuiPoint(cam, hex.Pos);
            DrawOverlayTexture(new Rect(center.x - 10, center.y - 10, 20, 20), movementHexFill, new Color(0.03f, 0.12f, 0.1f, 0.94f));
            GUI.Label(new Rect(center.x - 10, center.y - 10, 20, 20), hex.MoveCost.ToString(), movementCostStyle);
        }

        DrawHexOutline(unit.CurHex, cam, size, CurrentHexColor, 2.5f, false);
        if (hovered != null && hovered != unit.CurHex && !destinations.Contains(hovered))
            DrawHexOutline(hovered, cam, size, new Color(1f, 0.48f, 0.4f, 0.9f), 2f, false);

        DrawMovementHud(unit, destinations, hovered);
    }

    private static Hex GetHoveredHex(Camera cam)
    {
        if (Mouse.current == null)
            return null;
        Vector2 mouse = Mouse.current.position.ReadValue();
        Vector3 world = cam.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, -cam.transform.position.z));
        foreach (Collider2D collider in Physics2D.OverlapPointAll(world))
        {
            Hex hex = collider.GetComponent<Hex>();
            if (hex != null)
                return hex;
        }
        return null;
    }

    private void EnsureMovementOverlayStyles()
    {
        if (movementHexFill == null)
        {
            const int resolution = 64;
            movementHexFill = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);
            movementHexFill.name = "Movement hex fill";
            movementHexFill.hideFlags = HideFlags.DontSave;
            movementHexFill.filterMode = FilterMode.Bilinear;
            var pixels = new Color[resolution * resolution];
            for (int y = 0; y < resolution; y++)
                for (int x = 0; x < resolution; x++)
                {
                    float nx = Mathf.Abs((x + 0.5f) / resolution * 2 - 1);
                    float ny = Mathf.Abs((y + 0.5f) / resolution * 2 - 1);
                    pixels[y * resolution + x] = ny <= 1 - nx * 0.5f ? Color.white : Color.clear;
                }
            movementHexFill.SetPixels(pixels);
            movementHexFill.Apply(false, true);
        }

        if (movementTitleStyle != null)
            return;
        movementTitleStyle = new GUIStyle(GUI.skin.label) { fontSize = 19, fontStyle = FontStyle.Bold };
        movementTitleStyle.normal.textColor = Color.white;
        movementTextStyle = new GUIStyle(GUI.skin.label) { fontSize = 15 };
        movementTextStyle.normal.textColor = new Color(0.8f, 0.96f, 1f);
        movementHintStyle = new GUIStyle(GUI.skin.label) { fontSize = 12, wordWrap = true };
        movementHintStyle.normal.textColor = new Color(0.85f, 0.89f, 0.94f);
        movementCostStyle = new GUIStyle(GUI.skin.label) { fontSize = 12, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter };
        movementCostStyle.normal.textColor = Color.white;
    }

    private void DrawMovementHud(Unit unit, List<Hex> destinations, Hex hovered)
    {
        float width = Mathf.Min(370, Screen.width - 24);
        var panel = new Rect(12, 12, width, 152);
        DrawOverlayTexture(panel, Texture2D.whiteTexture, new Color(0.025f, 0.055f, 0.085f, 0.93f));
        DrawOverlayTexture(new Rect(panel.x, panel.y, 3, panel.height), Texture2D.whiteTexture, CurrentHexColor);
        GUI.Label(new Rect(26, 22, width - 26, 26), unit.UnitName, movementTitleStyle);
        string status = unit.UnitStatus == UnitStatus.OnBoard ? "Passenger aboard" : unit.UnitType == UnitType.Naval ? "Ship" : "Land unit";
        GUI.Label(new Rect(26, 51, width - 26, 23), $"Movement {unit.MovePoint} / {unit.MovePointMax}   |   {status}", movementTextStyle);
        GUI.Label(new Rect(26, 81, width - 26, 20), "Left: select    Right: move    Tab: next unit", movementHintStyle);
        GUI.Label(new Rect(26, 102, width - 26, 20), "Green: legal move + cost    G: toggle grid", movementHintStyle);

        string hint;
        if (!GameManager.instance.PlayerTurn)
            hint = "Waiting for the other factions...";
        else if (unit.IsMoving)
            hint = "Moving...";
        else if (unit.MovePoint <= 0)
            hint = "No movement left. Backspace: end turn.";
        else if (unit.UnitStatus == UnitStatus.OnBoard && destinations.Count == 0)
            hint = "Bring the ship to the coast first. Tab: select ship.";
        else if (destinations.Count == 0)
            hint = "No adjacent move fits this unit's terrain or movement.";
        else if (hovered != null && destinations.Contains(hovered))
            hint = $"{hovered.HexName}: right click once to move (cost {hovered.MoveCost}).";
        else
            hint = "Right click a GREEN hex once. No dragging needed.";
        GUI.Label(new Rect(26, 125, width - 26, 32), hint, movementHintStyle);
    }

    private static Vector2 ScreenGuiPoint(Camera cam, Vector3 world)
    {
        Vector3 screen = cam.WorldToScreenPoint(world);
        return new Vector2(screen.x, Screen.height - screen.y);
    }

    private static Rect HexScreenRect(Hex hex, Camera cam, Vector2 size)
    {
        Vector2 topLeft = ScreenGuiPoint(cam, hex.transform.position + new Vector3(-size.x * 0.5f, size.y * 0.5f));
        Vector2 bottomRight = ScreenGuiPoint(cam, hex.transform.position + new Vector3(size.x * 0.5f, -size.y * 0.5f));
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    private static void DrawHexOutline(Hex hex, Camera cam, Vector2 size, Color color, float width, bool grid)
    {
        Vector3 center = hex.transform.position;
        for (int i = 0; i < 6; i++)
        {
            // Share internal grid edges; retain every exterior map edge.
            if (grid && i >= 3)
            {
                HexDirection dir = i == 3 ? HexDirection.BottomLeft : i == 4 ? HexDirection.Left : HexDirection.TopLeft;
                if (HexCalculator.FindHexByDir(hex, dir, GameManager.instance.AllHexes) != null)
                    continue;
            }
            Vector2 from = Vector2.Scale(MovementHexCorners[i], size);
            Vector2 to = Vector2.Scale(MovementHexCorners[(i + 1) % 6], size);
            DrawOverlayLine(ScreenGuiPoint(cam, center + (Vector3)from), ScreenGuiPoint(cam, center + (Vector3)to), color, width);
        }
    }

    private static void DrawOverlayLine(Vector2 from, Vector2 to, Color color, float width)
    {
        Matrix4x4 matrix = GUI.matrix;
        Vector2 delta = to - from;
        GUIUtility.RotateAroundPivot(Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg, from);
        DrawOverlayTexture(new Rect(from.x, from.y - width * 0.5f, delta.magnitude, width), Texture2D.whiteTexture, color);
        GUI.matrix = matrix;
    }

    private static void DrawOverlayTexture(Rect rect, Texture texture, Color color)
    {
        Color previous = GUI.color;
        GUI.color = color;
        GUI.DrawTexture(rect, texture);
        GUI.color = previous;
    }

    private void OnDestroy()
    {
        if (movementHexFill != null)
            Destroy(movementHexFill);
    }
}
