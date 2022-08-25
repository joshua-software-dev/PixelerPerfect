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
    public Vector4 HitboxPixelBorderColor { get; set; } = new (0.4f, 0.4f, 0.4f, 1f);

    public bool ShowRingAroundPlayer { get; set; } = false;
    public bool OnlyShowRingInCombat { get; set; } = false;
    public bool OnlyShowRingInInstances { get; set; } = false;
    public float RingYalmRadius { get; set; } = 2f;
    public float RingThickness { get; set; } = 10f;
    public int RingSmoothness { get; set; } = 100;
    public Vector4 RingColor { get; set; } = new (0.4f, 0.4f, 0.4f, 0.5f);

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
