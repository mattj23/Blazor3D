using HomagGroup.Blazor3D.Maths;

namespace HomagGroup.Blazor3D.Events
{
    internal class CanvasSizeStaticArgs
    {
        public string ContainerId { get; set; } = null!;

        public required Vector2 Size { get; set; } = new Vector2(0, 0);
    }

    /// <summary>
    /// <para>Arguments for <see cref="Viewers.Viewer"/> ObjectSelected and ObjectLoaded event handlers.</para>
    /// </summary>
    public class CanvasSizeArgs
    {
        public required Vector2 Size { get; set; }
    }
}
