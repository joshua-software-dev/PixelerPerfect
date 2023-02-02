using Dalamud.Configuration;
using System;
using System.Numerics;


namespace PixelerPerfect;

public class Config : IPluginConfiguration
{
    public string PluginName { get; set; } = null!;
    public int Version { get; set; }

    public bool ShowHitboxPixel { get; set; } = true;
    public bool ShowHitboxPixelBorder { get; set; } = false;
    public bool OnlyShowHitboxPixelInCombat { get; set; } = true;
    public bool OnlyShowHitboxPixelInInstances { get; set; } = false;
    public Vector4 HitboxPixelPrimaryColor { get; set; } = new (1f, 1f, 1f, 1f);
    public Vector4 HitboxPixelBorderColor { get; set; } = new (0.0f, 0.0f, 0.0f, 1f);

    public bool ShowRingAroundPlayer { get; set; } = false;
    public bool OnlyShowRingInCombat { get; set; } = true;
    public bool OnlyShowRingInInstances { get; set; } = false;
    public float RingYalmRadius { get; set; } = 2f;
    public float RingThickness { get; set; } = 10f;
    public int RingSmoothness { get; set; } = 100;
    public Vector4 RingColor { get; set; } = new (0.4f, 0.4f, 0.4f, 0.5f);

    public bool ShowNorthFacingArrow { get; set; } = false;
    public bool OnlyShowArrowInCombat { get; set; } = true;
    public bool OnlyShowArrowInInstances { get; set; } = false;
    public bool ShowArrowChevron { get; set; } = true;
    public bool ShowArrowLine { get; set; } = true;
    public Vector4 ArrowColor { get; set; } = new (0.4f, 0.4f, 0.4f, 0.5f);
    public Vector4 ArrowChevronOverrideColor { get; set; } = new (0.4f, 0.4f, 0.4f, 0.5f);
    public Vector4 ArrowLineOverrideColor { get; set; } = new (0.4f, 0.4f, 0.4f, 0.5f);

    public float ArrowChevronDistanceOffsetFromPlayer { get; set; } = 1f;
    public float ArrowChevronLength { get; set; } = 1f;
    public float ArrowChevronRadius { get; set; } = 11.5f;
    public float ArrowChevronSin { get; set; } = -1.5f;
    public float ArrowChevronThickness { get; set; } = 5f;

    public float ArrowLineDistanceOffsetFromPlayer { get; set; } = 0.5f;
    public float ArrowLineLength { get; set; } = 1f;
    public float ArrowLineThickness { get; set; } = 5f;

    [NonSerialized]
    private Plugin _plugin = null!;

    public void Init(Plugin plugin)
    {
        _plugin = plugin;
        PluginName = _plugin.Name;
    }

    public void Save()
    {
        _plugin.PluginInterface.SavePluginConfig(this);
    }
}
