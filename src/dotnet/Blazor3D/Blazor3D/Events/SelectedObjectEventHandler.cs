namespace HomagGroup.Blazor3D.Events
{
    /// <summary>
    /// <para>Delegate that handles <see cref="Viewers.Viewer"/> ObjectSelected event.</para>
    /// </summary>
    /// <param name="e"><see cref="Object3DArgs"/> arguments for ObjectSelected event handler.</param>
    public delegate void SelectedObjectEventHandler(Object3DArgs e);
    public delegate void HoveredObjectEventHandler(Object3DArgs e);
    public delegate void HoverEndObjectEventHandler(Object3DArgs e);
    public delegate void ClickedObjectEventHandler(Object3DArgs e);

}
