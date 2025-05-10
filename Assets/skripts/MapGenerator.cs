using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    [Header("Settings")]
    public int minNodes = 5;
    public int maxNodes = 8;
    public float horizontalSpacing = 2f;
    public float verticalSpacing = 1f;

    [Header("Prefabs")]
    public GameObject startPointPrefab;
    public GameObject battlePointPrefab;
    public GameObject bossPointPrefab;
    public GameObject eventPointPrefab;

    private List<MapNode> allNodes = new List<MapNode>();

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        ClearMap();
        int nodeCount = Random.Range(minNodes, maxNodes + 1);

        // Стартовая точка смещается вместе со всеми
        Vector3 currentPosition = new Vector3(-5f, 0, 0); // Начальная позиция слева

        MapNode startNode = CreateNode(MapNode.NodeType.Start, currentPosition);
        allNodes.Add(startNode);

        // Генерация остальных точек вправо
        for (int i = 1; i < nodeCount; i++)
        {
            currentPosition += new Vector3(horizontalSpacing, Random.Range(-verticalSpacing, verticalSpacing), 0);

            MapNode.NodeType randomType = (i == nodeCount - 1) ?
                MapNode.NodeType.Boss :
                (Random.value > 0.5f ? MapNode.NodeType.Battle : MapNode.NodeType.Event);

            MapNode newNode = CreateNode(randomType, currentPosition);
            allNodes.Add(newNode);
            allNodes[i - 1].connectedNodes.Add(newNode);
        }
    }

    private MapNode CreateNode(MapNode.NodeType type, Vector3 position)
    {
        GameObject prefab = type switch
        {
            MapNode.NodeType.Start => startPointPrefab,
            MapNode.NodeType.Battle => battlePointPrefab,
            MapNode.NodeType.Boss => bossPointPrefab,
            MapNode.NodeType.Event => eventPointPrefab,
            _ => null
        };

        GameObject nodeObj = Instantiate(prefab, position, Quaternion.identity, transform);
        MapNode node = nodeObj.GetComponent<MapNode>();
        node.nodeType = type;
        return node;
    }

    private void ClearMap()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        allNodes.Clear();
    }
}