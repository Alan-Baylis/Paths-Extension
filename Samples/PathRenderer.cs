﻿using Paths;
using Paths.Cache;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathRenderer : MonoBehaviour
{
    public GameObject PathObject;
    public int vertexCount = 50;
    public bool Static;

    private Path3D path;
    private LineRenderer lineRenderer;

    private void Start()
    {
        if (PathObject == null)
            PathObject = this.gameObject;
        path = PathUtility.GetPath3D(PathObject);

        lineRenderer = GetComponent<LineRenderer>();
        UpdateLineRendererVertices();
    }

    private void Update()
    {
        if (!Static)
            UpdateLineRendererVertices();
    }

    private void UpdateLineRendererVertices()
    {
        if (vertexCount < 0)
            vertexCount = 0;

        lineRenderer.SetVertexCount(vertexCount);

        var vertices = new EvaluationCache(path, vertexCount - 2).Values;
        for (int i = 0; i < vertices.Length; i++)
            lineRenderer.SetPosition(i, vertices[i]);
    }
}