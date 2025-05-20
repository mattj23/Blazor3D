using HomagGroup.Blazor3D.Maths;

namespace HomagGroup.Blazor3D.Events
{
    /// <summary>
    /// <para>Delegate that handles client rect changes of canvas of viewer</para>
    /// </summary>
    /// <param name="e"><see cref="Vector2"/>pixelsize of canvas</param>
    public delegate void CanvasRectSizedEventHandler(Vector2 e);

}
