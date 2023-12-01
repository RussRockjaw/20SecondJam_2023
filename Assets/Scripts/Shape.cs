using UnityEngine;

public class Shape : MonoBehaviour
{
    private Vector2[] nodePositions;
    private float pivotOffset = 0.5f;
    private Vector3 initialPos;
    
    void Start()
    {
        transform.position += new Vector3(pivotOffset, pivotOffset);    
    }

    public void CreateMesh(Vector2[] nodePositions)
    {
        this.nodePositions = nodePositions;
        //Add components to GameObjects
        MeshFilter meshFilter = this.gameObject.transform.GetChild(0).gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshCollider meshCollider = this.gameObject.transform.GetChild(0).gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        MeshRenderer meshRenderer = this.gameObject.transform.GetChild(0).gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
        meshRenderer.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);
        
        //Data for mesh generation
        Vector3[] verts = new Vector3[nodePositions.Length * 4];
        Vector2[] uvs = new Vector2[nodePositions.Length * 4];
        int[] tris = new int[nodePositions.Length * 2 * 3];
        int triCount = 0;
        int nodeCount = 0;

        for(int i = 0; i < verts.Length; i += 4)
        {
            //Set quad's vertices. Subtract the offset to fit to grid visually.
            verts[i] = new Vector3(nodePositions[nodeCount].x - pivotOffset, nodePositions[nodeCount].y - pivotOffset);
            verts[i + 1] = new Vector3(nodePositions[nodeCount].x - pivotOffset, 1 + nodePositions[nodeCount].y - pivotOffset);
            verts[i + 2] =  new Vector3(1 + nodePositions[nodeCount].x - pivotOffset, 1 + nodePositions[nodeCount].y - pivotOffset);
            verts[i + 3] = new Vector3(1 + nodePositions[nodeCount].x - pivotOffset, + nodePositions[nodeCount].y - pivotOffset);
            
            //Make new UVs to match new vertices
            uvs[i] = new Vector3(pivotOffset + nodePositions[nodeCount].x, pivotOffset + nodePositions[nodeCount].y);
            uvs[i + 1] = new Vector3(pivotOffset + nodePositions[nodeCount].x, pivotOffset + 1 + nodePositions[nodeCount].y);
            uvs[i + 2] =  new Vector3(pivotOffset + 1 + nodePositions[nodeCount].x, pivotOffset + 1 + nodePositions[nodeCount].y);
            uvs[i + 3] = new Vector3(pivotOffset + 1 + nodePositions[nodeCount].x, pivotOffset + nodePositions[nodeCount].y);
            
            //Create triangle 1 of new quad
            tris[triCount] = i;
            tris[triCount + 1] = i + 1;
            tris[triCount + 2] = i + 2;
            
            //Create triangle 2 onew quad
            tris[triCount + 3] = i;
            tris[triCount + 4] = i + 2;
            tris[triCount + 5] = i + 3;

            triCount += 6;
            nodeCount++;
        }
        
        Mesh mesh = new Mesh();
        mesh.vertices = verts;
        mesh.uv = uvs;
        mesh.triangles = tris;
        mesh.name = "Piece";
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;

        this.gameObject.transform.GetChild(0).gameObject.tag = "piece";
    }

    public Vector2[] LocalToWorld()
    {
        Vector2[] worldCoords = new Vector2[nodePositions.Length];
        for(int i = 0; i < nodePositions.Length; i++)
            worldCoords[i] = transform.TransformPoint(nodePositions[i]);
        return worldCoords;
    }

    public void SetInitialPos(Vector3 p)
    {
        initialPos = p;
    }

    public void ResetToInitialPos()
    {
        transform.position = initialPos;
    }

    public void SetNodePositions(Vector2[] positions)
    {
        nodePositions = positions;
    }

    public Vector2[] GetNodePositions()
    {
        return nodePositions;
    }    
}
