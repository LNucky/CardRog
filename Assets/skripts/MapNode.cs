using UnityEngine;
using System.Collections.Generic;

public class MapNode : MonoBehaviour
{
    public enum NodeType { Start, Battle, Boss, Event }
    public NodeType nodeType;
    public List<MapNode> connectedNodes = new List<MapNode>();

    // Метод для визуализации связей в редакторе
    private void OnDrawGizmos()
    {
        Gizmos.color = GetNodeColor();
        Gizmos.DrawSphere(transform.position, 0.3f);
        foreach (MapNode node in connectedNodes)
        {
            if (node != null)
                Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }

    // Возвращает цвет в зависимости от типа узла
    private Color GetNodeColor()
    {
        switch (nodeType)
        {
            case NodeType.Start:
                return Color.green;
            case NodeType.Battle:
                return Color.red;
            case NodeType.Boss:
                return Color.magenta;
            case NodeType.Event:
                return Color.yellow;
            default:
                return Color.white;
        }
    }
} // <-- Убедитесь, что класс закрыт!