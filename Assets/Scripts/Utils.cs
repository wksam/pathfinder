using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default, int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color != null ? (Color)color : Color.white;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 position = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        position.z = 0f;
        return position;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) => worldCamera.ScreenToWorldPoint(screenPosition);

    public static void CreateEmptyMeshArrays(int quadCount, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
    {
        vertices = new Vector3[4 * quadCount];
        uvs = new Vector2[4 * quadCount];
        triangles = new int[6 * quadCount];
    }

    public static void AddMeshArrays(Vector3[] vertices, Vector2[] uvs, int[] triangles, int index, Vector3 position, float rotation, Vector3 baseSize, Vector2 uv00, Vector2 uv11)
    {
        // Relocate vertices
        int vIndex = index * 4;
        int vInde0 = vIndex;
        int vInde1 = vIndex + 1;
        int vInde2 = vIndex + 2;
        int vInde3 = vIndex + 3;

        baseSize *= .5f;

        bool skewed = baseSize.x != baseSize.y;
        if(skewed)
        {
            vertices[vInde0] = position + GetQuaternionEuler(rotation) * new Vector3(-baseSize.x, baseSize.y);
            vertices[vInde1] = position + GetQuaternionEuler(rotation) * new Vector3(-baseSize.x, -baseSize.y);
            vertices[vInde2] = position + GetQuaternionEuler(rotation) * new Vector3(baseSize.x, -baseSize.y);
            vertices[vInde3] = position + GetQuaternionEuler(rotation) * baseSize;
        }
        else
        {
            vertices[vInde0] = position + GetQuaternionEuler(rotation - 270) * baseSize;
            vertices[vInde1] = position + GetQuaternionEuler(rotation - 180) * baseSize;
            vertices[vInde2] = position + GetQuaternionEuler(rotation - 90) * baseSize;
            vertices[vInde3] = position + GetQuaternionEuler(rotation) * baseSize;
        }

        // Relocate UVs
        uvs[vInde0] = new Vector2(uv00.x, uv11.y);
        uvs[vInde1] = new Vector2(uv00.x, uv00.y);
        uvs[vInde2] = new Vector2(uv11.x, uv00.y);
        uvs[vInde3] = new Vector2(uv11.x, uv11.y);
    }

    private static Quaternion GetQuaternionEuler(float rotation) => Quaternion.Euler(new Vector3(0, rotation, 0));
}
