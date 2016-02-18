using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class OctahedronSphereTester : MonoBehaviour
{
	[SerializeField]
	private int _subdivisions = 0;
	[SerializeField]
	private float _radius = 1.0f;

	private void Awake()
	{
		GetComponent<MeshFilter>().mesh = OctahedronSphereCreator.Create(_subdivisions, _radius);
		//StartCoroutine(DebugVertices());
	}

	private IEnumerator DebugVertices()
	{
		var vertices = GetComponent<MeshFilter>().mesh.vertices;
		var normals = GetComponent<MeshFilter>().mesh.normals;
		for (var i = 0; i < vertices.Length; i++)
		{
			Debug.DrawRay(vertices[i], vertices[i] + normals[i], Color.red, 100f);
			yield return new WaitForSeconds(0.2f);
		}
	}
}
