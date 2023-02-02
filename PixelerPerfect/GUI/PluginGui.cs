using ImGuiNET;
using System.Numerics;
using System;


namespace PixelerPerfect.GUI;

public class PluginGui
{
    private readonly Config _config;
    private readonly WorldHelper _world;

    private float _arrowChevronDistanceOffsetFromPlayer;
    private float _arrowChevronLength;
    private Vector4 _arrowChevronOverrideColor;
    private float _arrowChevronRadius;
    private float _arrowChevronSin;
    private float _arrowChevronThickness;
    private Vector4 _arrowColor;
    private float _arrowLineDistanceOffsetFromPlayer;
    private float _arrowLineLength;
    private Vector4 _arrowLineOverrideColor;
    private float _arrowLineThickness;
    private Vector4 _hitboxPixelBorderColor;
    private Vector4 _hitboxPixelPrimaryColor;
    private bool _onlyShowArrowInCombat;
    private bool _onlyShowArrowInInstances;
    private bool _onlyShowHitboxPixelInCombat;
    private bool _onlyShowHitboxPixelInInstances;
    private bool _onlyShowRingInCombat;
    private bool _onlyShowRingInInstances;
    private Vector4 _ringColor;
    private int _ringSmoothness;
    private float _ringThickness;
    private float _ringYalmRadius;
    private bool _showArrowChevron;
    private bool _showArrowLine;
    private bool _showHitboxPixel;
    private bool _showHitboxPixelBorder;
    private bool _showNorthFacingArrow;
    private bool _showRingAroundPlayer;

    public PluginGui(Config config, WorldHelper world)
    {
        _config = config;
        _world = world;

        _arrowChevronDistanceOffsetFromPlayer = _config.ArrowChevronDistanceOffsetFromPlayer;
        _arrowChevronLength = _config.ArrowChevronLength;
        _arrowChevronOverrideColor = _config.ArrowChevronOverrideColor;
        _arrowChevronRadius = _config.ArrowChevronRadius;
        _arrowChevronSin = _config.ArrowChevronSin;
        _arrowChevronThickness = _config.ArrowChevronThickness;
        _arrowColor = _config.ArrowColor;
        _arrowLineDistanceOffsetFromPlayer = _config.ArrowLineDistanceOffsetFromPlayer;
        _arrowLineLength = _config.ArrowLineLength;
        _arrowLineOverrideColor = _config.ArrowLineOverrideColor;
        _arrowLineThickness = _config.ArrowLineThickness;
        _hitboxPixelBorderColor = _config.HitboxPixelBorderColor;
        _hitboxPixelPrimaryColor = _config.HitboxPixelPrimaryColor;
        _onlyShowArrowInCombat = _config.OnlyShowArrowInCombat;
        _onlyShowArrowInInstances = _config.OnlyShowArrowInInstances;
        _onlyShowHitboxPixelInCombat = _config.OnlyShowHitboxPixelInCombat;
        _onlyShowHitboxPixelInInstances = _config.OnlyShowHitboxPixelInInstances;
        _onlyShowRingInCombat = _config.OnlyShowRingInCombat;
        _onlyShowRingInInstances = _config.OnlyShowRingInInstances;
        _ringColor = _config.RingColor;
        _ringSmoothness = _config.RingSmoothness;
        _ringThickness = _config.RingThickness;
        _ringYalmRadius = _config.RingYalmRadius;
        _showArrowChevron = _config.ShowArrowChevron;
        _showArrowLine = _config.ShowArrowLine;
        _showHitboxPixel = _config.ShowHitboxPixel;
        _showHitboxPixelBorder = _config.ShowHitboxPixelBorder;
        _showNorthFacingArrow = _config.ShowNorthFacingArrow;
        _showRingAroundPlayer = _config.ShowRingAroundPlayer;
    }

    private void SaveConfig()
    {
        _config.ArrowChevronDistanceOffsetFromPlayer = _arrowChevronDistanceOffsetFromPlayer;
        _config.ArrowChevronLength = _arrowChevronLength;
        _config.ArrowChevronOverrideColor = _arrowChevronOverrideColor;
        _config.ArrowChevronRadius = _arrowChevronRadius;
        _config.ArrowChevronSin = _arrowChevronSin;
        _config.ArrowChevronThickness = _arrowChevronThickness;
        _config.ArrowColor = _arrowColor;
        _config.ArrowLineDistanceOffsetFromPlayer = _arrowLineDistanceOffsetFromPlayer;
        _config.ArrowLineLength = _arrowLineLength;
        _config.ArrowLineOverrideColor = _arrowLineOverrideColor;
        _config.ArrowLineThickness = _arrowLineThickness;
        _config.HitboxPixelBorderColor = _hitboxPixelBorderColor;
        _config.HitboxPixelPrimaryColor = _hitboxPixelPrimaryColor;
        _config.OnlyShowArrowInCombat = _onlyShowArrowInCombat;
        _config.OnlyShowArrowInInstances = _onlyShowArrowInInstances;
        _config.OnlyShowHitboxPixelInCombat = _onlyShowHitboxPixelInCombat;
        _config.OnlyShowHitboxPixelInInstances = _onlyShowHitboxPixelInInstances;
        _config.OnlyShowRingInCombat = _onlyShowRingInCombat;
        _config.OnlyShowRingInInstances = _onlyShowRingInInstances;
        _config.RingColor = _ringColor;
        _config.RingSmoothness = _ringSmoothness;
        _config.RingThickness = _ringThickness;
        _config.RingYalmRadius = _ringYalmRadius;
        _config.ShowArrowChevron = _showArrowChevron;
        _config.ShowArrowLine = _showArrowLine;
        _config.ShowHitboxPixel = _showHitboxPixel;
        _config.ShowHitboxPixelBorder = _showHitboxPixelBorder;
        _config.ShowNorthFacingArrow = _showNorthFacingArrow;
        _config.ShowRingAroundPlayer = _showRingAroundPlayer;

        _config.Save();
    }

    public bool DrawPluginConfig()
    {
        var drawConfig = true;
        var scale = ImGui.GetIO().FontGlobalScale;

        ImGui.SetNextWindowSize(new Vector2(x: 400 * scale, y: 390), ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowSizeConstraints(
            new Vector2(x: 420 * scale, y: 390),
            new Vector2(x: 420 * scale, y: 390)
        );
        ImGui.Begin(
            "Pixeler Perfect Config",
            ref drawConfig,
            ImGuiWindowFlags.NoCollapse |
            ImGuiWindowFlags.NoResize
        );

        var buttonTriggered = false;
        
        ImGui.BeginTable(string.Empty, 2, ImGuiTableFlags.BordersInnerV);
        ImGui.TableSetupColumn(string.Empty, ImGuiTableColumnFlags.None, 48);
        ImGui.TableSetupColumn(string.Empty, ImGuiTableColumnFlags.None, 52);
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        buttonTriggered |= ImGui.Checkbox("Show hitbox pixel", ref _showHitboxPixel);

        if (_showHitboxPixel)
        {
            ImGui.Indent();
            buttonTriggered |= ImGui.Checkbox("Show only in combat", ref _onlyShowHitboxPixelInCombat);
            buttonTriggered |= ImGui.Checkbox("Show only in instances", ref _onlyShowHitboxPixelInInstances);
            ImGui.Unindent();
        }

        ImGui.TableNextColumn();
        buttonTriggered |= ImGui.ColorEdit4("Hitbox pixel primary color", ref _hitboxPixelPrimaryColor, ImGuiColorEditFlags.NoInputs);
        
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        buttonTriggered |= ImGui.Checkbox("Show hitbox pixel border", ref _showHitboxPixelBorder);
        ImGui.TableNextColumn();
        buttonTriggered |= ImGui.ColorEdit4("Hitbox pixel border color", ref _hitboxPixelBorderColor, ImGuiColorEditFlags.NoInputs);
        ImGui.EndTable();
        ImGui.Separator();
        
        ImGui.BeginTable(string.Empty, 2, ImGuiTableFlags.BordersInnerV);
        ImGui.TableSetupColumn(string.Empty, ImGuiTableColumnFlags.None, 48);
        ImGui.TableSetupColumn(string.Empty, ImGuiTableColumnFlags.None, 52);
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        buttonTriggered |= ImGui.Checkbox("Show ring around player", ref _showRingAroundPlayer);

        if (_showRingAroundPlayer)
        {
            ImGui.Indent();
            buttonTriggered |= ImGui.Checkbox("Show only in combat", ref _onlyShowRingInCombat);
            buttonTriggered |= ImGui.Checkbox("Show only in instances", ref _onlyShowRingInInstances);
            ImGui.Unindent();

            buttonTriggered |= ImGui.DragFloat("Ring radius in yalms", ref _ringYalmRadius);
            buttonTriggered |= ImGui.DragFloat("Ring thickness", ref _ringThickness);
            buttonTriggered |= ImGui.DragInt("Ring smoothness", ref _ringSmoothness);
        }

        ImGui.TableNextColumn();
        buttonTriggered |= ImGui.ColorEdit4("Ring color", ref _ringColor, ImGuiColorEditFlags.NoInputs);
        ImGui.EndTable();
        ImGui.Separator();
        
        ImGui.BeginTable(string.Empty, 2, ImGuiTableFlags.BordersInnerV);
        ImGui.TableSetupColumn(string.Empty, ImGuiTableColumnFlags.None, 48);
        ImGui.TableSetupColumn(string.Empty, ImGuiTableColumnFlags.None, 52);
        ImGui.TableNextRow();
        ImGui.TableNextColumn();
        buttonTriggered |= ImGui.Checkbox("Show north facing arrow", ref _showNorthFacingArrow);
        if (_showNorthFacingArrow)
        {
            ImGui.Indent();
            buttonTriggered |= ImGui.Checkbox("Show only in combat", ref _onlyShowArrowInCombat);
            buttonTriggered |= ImGui.Checkbox("Show only in instances", ref _onlyShowArrowInInstances);
            ImGui.Unindent();
        }

        ImGui.TableNextColumn();
        if (ImGui.ColorEdit4("Arrow color", ref _arrowColor, ImGuiColorEditFlags.NoInputs))
        {
            buttonTriggered |= true;
            _arrowChevronOverrideColor = _arrowColor;
            _arrowLineOverrideColor = _arrowColor;
        }
        ImGui.EndTable();

        if (_showNorthFacingArrow)
        {
            ImGui.Separator();
            ImGui.BeginTable(string.Empty, 2, ImGuiTableFlags.BordersInnerV);
            ImGui.TableSetupColumn(string.Empty, ImGuiTableColumnFlags.None, 48);
            ImGui.TableSetupColumn(string.Empty, ImGuiTableColumnFlags.None, 52);
            ImGui.TableNextRow();

            ImGui.TableNextColumn();
            buttonTriggered |= ImGui.Checkbox("Show arrow chevron", ref _showArrowChevron);
            ImGui.TableNextColumn();
            buttonTriggered |= ImGui.ColorEdit4("Arrow chevron override color", ref _arrowChevronOverrideColor, ImGuiColorEditFlags.NoInputs);
            ImGui.EndTable();

            if (_showArrowChevron)
            {
                ImGui.PushItemWidth(150);
                buttonTriggered |= ImGui.DragFloat("Arrow chevron distance offset from player", ref _arrowChevronDistanceOffsetFromPlayer);
                ImGui.PushItemWidth(150);
                buttonTriggered |= ImGui.DragFloat("Arrow chevron length", ref _arrowChevronLength);
                ImGui.PushItemWidth(150);
                buttonTriggered |= ImGui.DragFloat("Arrow chevron radius", ref _arrowChevronRadius);
                ImGui.PushItemWidth(150);
                buttonTriggered |= ImGui.DragFloat("Arrow chevron sin", ref _arrowChevronSin);
                ImGui.PushItemWidth(150);
                buttonTriggered |= ImGui.DragFloat("Arrow chevron thickness", ref _arrowChevronThickness);
            }

            ImGui.Separator();
            ImGui.BeginTable(string.Empty, 2, ImGuiTableFlags.BordersInnerV);
            ImGui.TableSetupColumn(string.Empty, ImGuiTableColumnFlags.None, 48);
            ImGui.TableSetupColumn(string.Empty, ImGuiTableColumnFlags.None, 52);
            ImGui.TableNextRow();
            
            ImGui.TableNextColumn();
            buttonTriggered |= ImGui.Checkbox("Show arrow line", ref _showArrowLine);
            ImGui.TableNextColumn();
            buttonTriggered |= ImGui.ColorEdit4("Arrow line override color", ref _arrowLineOverrideColor, ImGuiColorEditFlags.NoInputs);
            ImGui.EndTable();

            if (_showArrowLine)
            {
                ImGui.PushItemWidth(150);
                buttonTriggered |= ImGui.DragFloat("Arrow line distance offset from player", ref _arrowLineDistanceOffsetFromPlayer);
                ImGui.PushItemWidth(150);
                buttonTriggered |= ImGui.DragFloat("Arrow line length", ref _arrowLineLength);
                ImGui.PushItemWidth(150);
                buttonTriggered |= ImGui.DragFloat("Arrow line thickness", ref _arrowLineThickness);
            }
        }

        ImGui.Separator();

        if (buttonTriggered)
        {
            SaveConfig();
        }

        ImGui.End();

        return drawConfig;
    }

    private void DrawHitboxPixelInWorld(Vector2 screenPositionOfPlayer)
    {
        if (!_config.ShowHitboxPixel)
        {
            return;
        }

        if (_config.OnlyShowHitboxPixelInCombat)
        {
            if (!_world.IsPlayerInCombat())
            {
                return;
            }
        }

        if (_config.OnlyShowHitboxPixelInInstances)
        {
            if (!_world.IsPlayerInInstance())
            {
                return;
            }
        }

        var viewportPos = _world.GetMainViewportPos();
        _world.SetNextWindowPosRelativeMainViewport(
            new Vector2(
                x: screenPositionOfPlayer.X - 10 - viewportPos.X,
                y: screenPositionOfPlayer.Y - 10 - viewportPos.Y
            )
        );

        ImGui.Begin(
            "Pixeler Perfect Hitbox",
            ImGuiWindowFlags.NoTitleBar |
            ImGuiWindowFlags.NoScrollbar |
            ImGuiWindowFlags.AlwaysAutoResize |
            ImGuiWindowFlags.NoInputs |
            ImGuiWindowFlags.NoBackground
        );

        ImGui.GetWindowDrawList().AddCircleFilled(
            new Vector2(x: screenPositionOfPlayer.X, y: screenPositionOfPlayer.Y),
            2f,
            ImGui.GetColorU32(_config.HitboxPixelPrimaryColor),
            100
        );

        if(_config.ShowHitboxPixelBorder)
        {
            ImGui.GetWindowDrawList().AddCircle(
                new Vector2(x: screenPositionOfPlayer.X, y: screenPositionOfPlayer.Y),
                2.2f,
                ImGui.GetColorU32(_config.HitboxPixelBorderColor),
                100
            );
        }

        ImGui.End();
    }

    private void DrawRing(Vector3 worldPositionOfPlayer)
    {
        var seg = _config.RingSmoothness / 2;
        for (var i = 0; i < _config.RingSmoothness; i++)
        {
            _world.WorldToScreen(
                new Vector3(
                    x: worldPositionOfPlayer.X + (_config.RingYalmRadius * (float)Math.Sin((Math.PI / seg) * i)),
                    z: worldPositionOfPlayer.Z + (_config.RingYalmRadius * (float)Math.Cos((Math.PI / seg) * i)),
                    y: worldPositionOfPlayer.Y
                ),
                out var pos
            );

            ImGui.GetWindowDrawList().PathLineTo(pos);
        }

        ImGui.GetWindowDrawList().PathStroke(
            ImGui.GetColorU32(_config.RingColor),
            ImDrawFlags.Closed,
            _config.RingThickness
        );
    }

    private void DrawRingInWorld(Vector3 worldPositionOfPlayer)
    {
        if (!_config.ShowRingAroundPlayer)
        {
            return;
        }

        if (_config.OnlyShowRingInCombat)
        {
            if (!_world.IsPlayerInCombat())
            {
                return;
            }
        }

        if (_config.OnlyShowRingInInstances)
        {
            if (!_world.IsPlayerInInstance())
            {
                return;
            }
        }

        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(x: 0, y: 0));
        _world.SetNextWindowPosRelativeMainViewport(new Vector2(x: 0, y: 0));

        ImGui.Begin(
            "Pixeler Perfect Ring",
            ImGuiWindowFlags.NoInputs |
            ImGuiWindowFlags.NoNav |
            ImGuiWindowFlags.NoTitleBar |
            ImGuiWindowFlags.NoScrollbar |
            ImGuiWindowFlags.NoBackground
        );

        ImGui.SetWindowSize(ImGui.GetIO().DisplaySize);

        DrawRing(worldPositionOfPlayer);

        ImGui.End();
        ImGui.PopStyleVar();
    }

    private void DrawArrowChevronInWorld(Vector2 chevTip, Vector2 chevOffset1, Vector2 chevOffset2)
    {
        ImGui.GetWindowDrawList().AddLine(
            new Vector2(chevTip.X, chevTip.Y),
            new Vector2(chevOffset1.X, chevOffset1.Y),
            ImGui.GetColorU32(_arrowChevronOverrideColor),
            _arrowChevronThickness
        );
        ImGui.GetWindowDrawList().AddLine(
            new Vector2(chevTip.X, chevTip.Y),
            new Vector2(chevOffset2.X, chevOffset2.Y),
            ImGui.GetColorU32(_arrowChevronOverrideColor),
            _arrowChevronThickness
        );
    }
    
    private void DrawArrowLineInWorld(Vector2 lineTip, Vector2 lineOffset)
    {
        ImGui.GetWindowDrawList().AddLine(
            new Vector2(lineTip.X, lineTip.Y),
            new Vector2(lineOffset.X, lineOffset.Y),
            ImGui.GetColorU32(_arrowLineOverrideColor),
            _arrowLineThickness
        );
    }
    
    private void DrawArrowInWorld(Vector3 worldPositionOfPlayer)
    {
        if (!_showNorthFacingArrow || (!_showArrowChevron && !_showArrowLine)) return;

        // ReSharper disable InlineOutVariableDeclaration
        Vector2 lineTip;
        Vector2 lineOffset;
        Vector2 chevOffset1;
        Vector2 chevOffset2;
        Vector2 chevTip;
        // ReSharper enable InlineOutVariableDeclaration
        
        //Tip of arrow
        _world.WorldToScreen(
            new Vector3(
                x: worldPositionOfPlayer.X + ((_arrowLineLength + _arrowLineDistanceOffsetFromPlayer) * (float) Math.Sin(Math.PI)),
                y: worldPositionOfPlayer.Y,
                z: worldPositionOfPlayer.Z + ((_arrowLineLength + _arrowLineDistanceOffsetFromPlayer) * (float) Math.Cos(Math.PI))
            ),
            out lineTip
        );
        //Player + offset
        _world.WorldToScreen(
            new Vector3(
                x: worldPositionOfPlayer.X + (_arrowLineDistanceOffsetFromPlayer * (float) Math.Sin(Math.PI)),
                y: worldPositionOfPlayer.Y,
                z: worldPositionOfPlayer.Z + (_arrowLineDistanceOffsetFromPlayer * (float) Math.Cos(Math.PI))
            ),
            out lineOffset
        );
        //Chev offset1
        _world.WorldToScreen(
            new Vector3(
                x: worldPositionOfPlayer.X + (_arrowChevronDistanceOffsetFromPlayer * (float) Math.Sin(Math.PI / _arrowChevronRadius) * _arrowChevronSin),
                y: worldPositionOfPlayer.Y,
                z: worldPositionOfPlayer.Z + (_arrowChevronDistanceOffsetFromPlayer * (float) Math.Cos(Math.PI / _arrowChevronRadius) * _arrowChevronSin)
            ),
            out chevOffset1
        );
        //Chev offset2
        _world.WorldToScreen(
            new Vector3(
                x: worldPositionOfPlayer.X + (_arrowChevronDistanceOffsetFromPlayer * (float) Math.Sin(Math.PI / -_arrowChevronRadius) * _arrowChevronSin),
                y: worldPositionOfPlayer.Y,
                z: worldPositionOfPlayer.Z + (_arrowChevronDistanceOffsetFromPlayer * (float) Math.Cos(Math.PI / -_arrowChevronRadius) * _arrowChevronSin)
            ),
            out chevOffset2
        );
        //Chev Tip
        _world.WorldToScreen(
            new Vector3(
                x: worldPositionOfPlayer.X + ((_arrowChevronDistanceOffsetFromPlayer + _arrowChevronLength) * (float) Math.Sin(Math.PI)),
                y: worldPositionOfPlayer.Y,
                z: worldPositionOfPlayer.Z + ((_arrowChevronDistanceOffsetFromPlayer + _arrowChevronLength) * (float) Math.Cos(Math.PI))
            ),
            out chevTip
        );

        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(x: 0, y: 0));
        _world.SetNextWindowPosRelativeMainViewport(new Vector2(x: 0, y: 0));

        ImGui.Begin(
            "Pixeler Perfect Arrow",
            ImGuiWindowFlags.NoInputs |
            ImGuiWindowFlags.NoNav |
            ImGuiWindowFlags.NoTitleBar |
            ImGuiWindowFlags.NoScrollbar |
            ImGuiWindowFlags.NoBackground
        );

        ImGui.SetWindowSize(ImGui.GetIO().DisplaySize);

        if (_showArrowLine) DrawArrowLineInWorld(lineTip, lineOffset);
        if (_showArrowChevron) DrawArrowChevronInWorld(chevTip, chevOffset1, chevOffset2);

        ImGui.End();
        ImGui.PopStyleVar();
    }

    public void DrawInWorld()
    {
        var worldPositionOfPlayer = _world.GetPlayerCoordinates();
        if (worldPositionOfPlayer == null)
        {
            return;
        }

        var cameraViewOfPlayerUnobstructed = _world.WorldToScreen(
            worldPositionOfPlayer.Value,
            out var screenPositionOfPlayer
        );

        if (!cameraViewOfPlayerUnobstructed)
        {
            return;
        }

        DrawHitboxPixelInWorld(screenPositionOfPlayer);
        DrawRingInWorld(worldPositionOfPlayer.Value);
        DrawArrowInWorld(worldPositionOfPlayer.Value);
    }
}
