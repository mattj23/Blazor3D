using System.Globalization;
using System.Text.RegularExpressions;
using HomagGroup.Blazor3D.Core;
using HomagGroup.Blazor3D.Maths;

namespace HomagGroup.Blazor3D.Geometires
{
    /// <summary>
    /// <para>Class for rectangular cuboid with a given 'width', 'height', and 'depth'.</para>
    /// <para>This class inherits from <see cref="BufferGeometry"/></para>
    /// <para>Wrapper for three.js <a target="_blank" href="https://threejs.org/docs/index.html#api/en/geometries/BoxGeometry">BoxGeometry</a></para>
    /// </summary>
    /// <inheritdoc><see cref="BufferGeometry"/></inheritdoc>
    public sealed class RawBufferGeometry : BufferGeometry
    {
        public RawBufferGeometry() : base("RawBufferGeometry")
        {

        }
        
        public List<Vector3> Vertices { get; set; } = [];
        public List<int> Indices { get; set; } = [];
        public List<Vector2> Uvs { get; set; } = [];
        public List<Vector3> Normals { get; set; } = [];
        
        public void FromWaveFrontObjectString(string objContent)
    {
        // Temp storage
        var tempVertices = new List<Vector3>();
        var tempUvs = new List<Vector2>();
        var tempNormals = new List<Vector3>();
        var indexMap = new Dictionary<string, int>(); // map from "v/vt/vn" string to vertex index

        var lines = objContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var rawLine in lines)
        {
            var line = rawLine.Trim();
            if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line))
                continue;

            var tokens = Regex.Split(line, @"\s+");

            switch (tokens[0])
            {
                case "v": // Vertex
                    tempVertices.Add(new Vector3(
                        float.Parse(tokens[1], CultureInfo.InvariantCulture),
                        float.Parse(tokens[2], CultureInfo.InvariantCulture),
                        float.Parse(tokens[3], CultureInfo.InvariantCulture)
                    ));
                    break;

                case "vt": // Texture coord
                    tempUvs.Add(new Vector2(
                        float.Parse(tokens[1], CultureInfo.InvariantCulture),
                        float.Parse(tokens[2], CultureInfo.InvariantCulture)
                    ));
                    break;

                case "vn": // Normal
                    tempNormals.Add(new Vector3(
                        float.Parse(tokens[1], CultureInfo.InvariantCulture),
                        float.Parse(tokens[2], CultureInfo.InvariantCulture),
                        float.Parse(tokens[3], CultureInfo.InvariantCulture)
                    ));
                    break;

                case "f": // Face
                    var faceIndices = tokens[1..].Select(vertex =>
                    {
                        if (!indexMap.TryGetValue(vertex, out int idx))
                        {
                            var parts = vertex.Split('/');
                            int vIndex = int.Parse(parts[0]) - 1;
                            int vtIndex = parts.Length > 1 && parts[1] != "" ? int.Parse(parts[1]) - 1 : -1;
                            int vnIndex = parts.Length > 2 ? int.Parse(parts[2]) - 1 : -1;

                            // Store or duplicate vertex
                            Vertices.Add(tempVertices[vIndex]);
                            if (vtIndex >= 0) Uvs.Add(tempUvs[vtIndex]);
                            if (vnIndex >= 0) Normals.Add(tempNormals[vnIndex]);

                            idx = Vertices.Count - 1;
                            indexMap[vertex] = idx;
                        }
                        return idx;
                    }).ToList();

                    // Triangulate polygonal faces (assumes convex)
                    for (int i = 1; i < faceIndices.Count - 1; i++)
                    {
                        Indices.Add(faceIndices[0]);
                        Indices.Add(faceIndices[i]);
                        Indices.Add(faceIndices[i + 1]);
                    }

                    break;
            }
        }
    }
    }
}
