using Dalamud.Game.ClientState.Conditions;
using Dalamud.Interface;
using System.Numerics;


namespace PixelPerfect.GUI
{
    public class WorldHelper
    {
        private readonly Plugin _plugin;

        public WorldHelper(Plugin plugin)
        {
            _plugin = plugin;
        }

        public Vector2 GetMainViewportPos()
        {
            return ImGuiHelpers.MainViewport.Pos;
        }

        public Vector3? GetPlayerCoordinates()
        {
            return _plugin.ClientState.LocalPlayer?.Position;
        }

        public bool IsPlayerInCombat()
        {
            return _plugin.Condition[ConditionFlag.InCombat];
        }

        public bool IsPlayerInInstance()
        {
            return _plugin.Condition[ConditionFlag.BoundByDuty];
        }

        public void SetNextWindowPosRelativeMainViewport(Vector2 position)
        {
            ImGuiHelpers.SetNextWindowPosRelativeMainViewport(position);
        }

        public bool WorldToScreen(Vector3 worldPos, out Vector2 screenPos)
        {
            return _plugin.GameGui.WorldToScreen(worldPos, out screenPos);
        }
    }
}
