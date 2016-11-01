using UnityEngine;
using System.Collections.Generic;


namespace Assets.Core
{

    
    public class MeshData
    {
        // Form attributes
        public List<Vector3> vertices = new List<Vector3>();
        public List<int> triangles = new List<int>();
        public List<Color> colors = new List<Color>();
        // public List<Vector2> uv = new List<Vector2>();

        // Colider attributes.
        public List<Vector3> colVertices = new List<Vector3>();
        public List<int> colTriangles = new List<int>();

        //
        public bool useRenderDataForCol;


        public MeshData()
        {
        }


        public void AddQuadTriangles()
        {
            /* Take the last four added vertices and creates two triangles to make up the
             * quad with those vertices.
             */
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 2);

            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 1);

            // Repeate the process for the collision mesh if desired.
            if (useRenderDataForCol)
            {
                colTriangles.Add(colVertices.Count - 4);
                colTriangles.Add(colVertices.Count - 3);
                colTriangles.Add(colVertices.Count - 2);

                colTriangles.Add(colVertices.Count - 4);
                colTriangles.Add(colVertices.Count - 2);
                colTriangles.Add(colVertices.Count - 1);
            }
        }


        public void AddVertex(Vector3 vertex)
        {
            AddVertex(vertex, Color.grey);
        }


        public void AddVertex(Vector3 vertex, Color vertexColor)
        {
            vertices.Add(vertex);
            colors.Add(vertexColor);
            if (useRenderDataForCol)
                colVertices.Add(vertex);
        }


        public void AddTriangle(int triangle)
        {
            triangles.Add(triangle);
            if (useRenderDataForCol)
                colTriangles.Add(triangle - (vertices.Count - colVertices.Count));
        }
    }
}