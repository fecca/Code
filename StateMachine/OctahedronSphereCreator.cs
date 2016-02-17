using UnityEngine;

public static class OctahedronSphereCreator
{
	private static Vector3[] _directions =
	{
		Vector3.left,
		Vector3.back,
		Vector3.right,
		Vector3.forward
	};

	public static Mesh Create(int _subdivisions, float _radius)
	{
		_subdivisions = Mathf.Clamp(_subdivisions, 0, 6);

		var resolution = 1 << _subdivisions;
		var vertices = new Vector3[(resolution + 1) * (resolution + 1) * 4 - (resolution * 2 - 1) * 3];
		var triangles = new int[(1 << (_subdivisions * 2 + 3)) * 3];

		CreateOctahedron(vertices, triangles, resolution);

		var normals = new Vector3[vertices.Length];

		for (var i = 0; i < vertices.Length; i++)
		{
			var normalizedVertex = vertices[i].normalized;
			normals[i] = normalizedVertex;
			vertices[i] = normalizedVertex * _radius;

			//vertices[i] *= Mathf.PerlinNoise((float)i / vertices.Length, vertices.Length - (float)i / vertices.Length);
		}

		var mesh = new Mesh
		{
			vertices = vertices,
			triangles = triangles,
			normals = normals
		};

		return mesh;
	}

	private static void CreateOctahedron(Vector3[] vertices, int[] triangles, int resolution)
	{
		int v = 0, vBottom = 0, t = 0;

		for (int i = 0; i < 4; i++)
		{
			vertices[v++] = Vector3.down;
		}

		for (int i = 1; i <= resolution; i++)
		{
			float progress = (float)i / resolution;
			Vector3 from, to;
			vertices[v++] = to = Vector3.Lerp(Vector3.down, Vector3.forward, progress);
			for (int d = 0; d < 4; d++)
			{
				from = to;
				to = Vector3.Lerp(Vector3.down, _directions[d], progress);
				t = CreateLowerStrip(i, v, vBottom, t, triangles);
				v = CreateVertexLine(from, to, i, v, vertices);
				vBottom += i > 1 ? (i - 1) : 1;
			}
			vBottom = v - 1 - i * 4;
		}

		for (int i = resolution - 1; i >= 1; i--)
		{
			float progress = (float)i / resolution;
			Vector3 from, to;
			vertices[v++] = to = Vector3.Lerp(Vector3.up, Vector3.forward, progress);
			for (int d = 0; d < 4; d++)
			{
				from = to;
				to = Vector3.Lerp(Vector3.up, _directions[d], progress);
				t = CreateUpperStrip(i, v, vBottom, t, triangles);
				v = CreateVertexLine(from, to, i, v, vertices);
				vBottom += i + 1;
			}
			vBottom = v - 1 - i * 4;
		}

		for (int i = 0; i < 4; i++)
		{
			triangles[t++] = vBottom;
			triangles[t++] = v;
			triangles[t++] = ++vBottom;
			vertices[v++] = Vector3.up;
		}
	}

	private static int CreateVertexLine(Vector3 from, Vector3 to, int steps, int v, Vector3[] vertices)
	{
		for (int i = 1; i <= steps; i++)
		{
			vertices[v++] = Vector3.Lerp(from, to, (float)i / steps);
		}
		return v;
	}

	private static int CreateLowerStrip(int steps, int vTop, int vBottom, int t, int[] triangles)
	{
		for (int i = 1; i < steps; i++)
		{
			triangles[t++] = vBottom;
			triangles[t++] = vTop - 1;
			triangles[t++] = vTop;

			triangles[t++] = vBottom++;
			triangles[t++] = vTop++;
			triangles[t++] = vBottom;
		}
		triangles[t++] = vBottom;
		triangles[t++] = vTop - 1;
		triangles[t++] = vTop;
		return t;
	}

	private static int CreateUpperStrip(int steps, int vTop, int vBottom, int t, int[] triangles)
	{
		triangles[t++] = vBottom;
		triangles[t++] = vTop - 1;
		triangles[t++] = ++vBottom;
		for (int i = 1; i <= steps; i++)
		{
			triangles[t++] = vTop - 1;
			triangles[t++] = vTop;
			triangles[t++] = vBottom;

			triangles[t++] = vBottom;
			triangles[t++] = vTop++;
			triangles[t++] = ++vBottom;
		}
		return t;
	}
}