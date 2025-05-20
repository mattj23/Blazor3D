using HomagGroup.Blazor3D.Textures;

namespace HomagGroup.Blazor3D.Materials;

public class TextureMaterial:Material 
{
    public TextureMaterial() : base("TextureMaterial")
    {
    }
    public Texture Map { get; set; }
}