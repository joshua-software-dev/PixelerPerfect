using ImGuiNET;
using System.Diagnostics;
using System.Numerics;
using System;


namespace PixelerPerfect.GUI;

public class PluginGui
{
    private readonly Config _config;
    private readonly WorldHelper _world;

    private bool _showHitboxPixel;
    private bool _showHitboxPixelBorder;
    private bool _onlyShowHitboxPixelInCombat;
    private bool _onlyShowHitboxPixelInInstances;
    private Vector4 _hitboxPixelPrimaryColor;
    private Vector4 _hitboxPixelBorderColor;
    private bool _showRingAroundPlayer;
    private bool _onlyShowRingInCombat;
    private bool _onlyShowRingInInstances;
    private float _ringYalmRadius;
    private float _ringThickness;
    private int _ringSmoothness;
    private Vector4 _ringColor;

    public PluginGui(Config config, WorldHelper world)
    {
        _config = config;
        _world = world;

        _showHitboxPixel = _config.ShowHitboxPixel;
        _showHitboxPixelBorder = _config.ShowHitboxPixelBorder;
        _onlyShowHitboxPixelInCombat = _config.OnlyShowHitboxPixelInCombat;
        _onlyShowHitboxPixelInInstances = _config.OnlyShowHitboxPixelInInstances;
        _hitboxPixelPrimaryColor = _config.HitboxPixelPrimaryColor;
        _hitboxPixelBorderColor = _config.HitboxPixelBorderColor;
        _showRingAroundPlayer = _config.ShowRingAroundPlayer;
        _onlyShowRingInCombat = _config.OnlyShowRingInCombat;
        _onlyShowRingInInstances = _config.OnlyShowRingInInstances;
        _ringYalmRadius = _config.RingYalmRadius;
        _ringThickness = _config.RingThickness;
        _ringSmoothness = _config.RingSmoothness;
        _ringColor = _config.RingColor;
    }

    private void SaveConfig()
    {
        _config.ShowHitboxPixel = _showHitboxPixel;
        _config.ShowHitboxPixelBorder = _showHitboxPixelBorder;
        _config.OnlyShowHitboxPixelInCombat = _onlyShowHitboxPixelInCombat;
        _config.OnlyShowHitboxPixelInInstances = _onlyShowHitboxPixelInInstances;
        _config.HitboxPixelPrimaryColor = _hitboxPixelPrimaryColor;
        _config.HitboxPixelBorderColor = _hitboxPixelBorderColor;
        _config.ShowRingAroundPlayer = _showRingAroundPlayer;
        _config.OnlyShowRingInCombat = _onlyShowRingInCombat;
        _config.OnlyShowRingInInstances = _onlyShowRingInInstances;
        _config.RingYalmRadius = _ringYalmRadius;
        _config.RingThickness = _ringThickness;
        _config.RingSmoothness = _ringSmoothness;
        _config.RingColor = _ringColor;
        _config.Save();
    }

    public bool DrawPluginConfig()
    {
        var drawConfig = true;
        var scale = ImGui.GetIO().FontGlobalScale;

        ImGui.SetNextWindowSize(new Vector2(x: 400 * scale, y: 390), ImGuiCond.FirstUseEver);
        ImGui.SetNextWindowSizeConstraints(
            new Vector2(x: 400 * scale, y: 390),
            new Vector2(x: 400 * scale, y: 390)
        );
        ImGui.Begin(
            "Pixel Perfect 5 Config",
            ref drawConfig,
            ImGuiWindowFlags.NoCollapse |
            ImGuiWindowFlags.NoResize
        );

        var buttonTriggered = false;
        buttonTriggered |= ImGui.Checkbox("Show hitbox pixel", ref _showHitboxPixel);

        ImGui.SameLine(230 * scale);
        ImGui.PushStyleColor(ImGuiCol.Button, 0xFF000000 | 0x005E5BFF);
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, 0xDD000000 | 0x005E5BFF);
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, 0xAA000000 | 0x005E5BFF);

        if (ImGui.Button("Buy Haplo a Hot Chocolate"))
        {
            Process.Start(
                new ProcessStartInfo
                {
                    FileName = "https://ko-fi.com/haplo",
                    UseShellExecute = true
                }
            );
        }
        ImGui.PopStyleColor(3);

        buttonTriggered |= ImGui.Checkbox("Show hitbox pixel border", ref _showHitboxPixelBorder);
        buttonTriggered |= ImGui.Checkbox("Only show hitbox pixel in combat", ref _onlyShowHitboxPixelInCombat);
        buttonTriggered |= ImGui.Checkbox("Only show hitbox pixel in instances", ref _onlyShowHitboxPixelInInstances);
        buttonTriggered |= ImGui.ColorEdit4("Hitbox pixel primary color", ref _hitboxPixelPrimaryColor, ImGuiColorEditFlags.NoInputs);
        buttonTriggered |= ImGui.ColorEdit4("Hitbox pixel border color", ref _hitboxPixelBorderColor, ImGuiColorEditFlags.NoInputs);
        ImGui.Separator();
        buttonTriggered |= ImGui.Checkbox("Show ring around player", ref _showRingAroundPlayer);
        buttonTriggered |= ImGui.Checkbox("Only show ring in combat", ref _onlyShowRingInCombat);
        buttonTriggered |= ImGui.Checkbox("Only show ring in instances", ref _onlyShowRingInInstances);
        buttonTriggered |= ImGui.ColorEdit4("Ring color", ref _ringColor, ImGuiColorEditFlags.NoInputs);
        buttonTriggered |= ImGui.DragFloat("Ring radius in yalms", ref _ringYalmRadius);
        buttonTriggered |= ImGui.DragFloat("Ring thickness", ref _ringThickness);
        buttonTriggered |= ImGui.DragInt("Ring smoothness", ref _ringSmoothness);

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
            "Pixel Perfect 5",
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
            "Pixel Perfect 5 Ring",
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
    }
}
