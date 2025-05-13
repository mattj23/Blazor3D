using HomagGroup.Blazor3D.Maths;
using HomagGroup.Blazor3D.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagGroup.Blazor3D.Materials
{
    public class ShaderMaterial : Material
    {
        public ShaderMaterial() : base("ShaderMaterial")
        {
        }
        
        public required string FragmentShader { get; set; }
        public string? VertexShader { get; set; }
        
        public Dictionary<string, object> Uniforms { get; set; } = new();
    }
}
