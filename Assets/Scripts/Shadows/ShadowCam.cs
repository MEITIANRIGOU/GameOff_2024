using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShadowCam : MonoBehaviour
{
    public Tilemap tilemapShadowCaster;

    float camHeight, camWidth;
    Camera cam;
    [SerializeField] Material GLdraw;
    GameObject player;
    public int length;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        cam.aspect = 4f / 3f;
        camHeight = 2 * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
       // Darkness.settings.SetDarkness(0.25f);
    }
   

    void LateUpdate()
    {
        Vector3 playerPosition = player.transform.position;
        playerPosition.z = transform.position.z; // Maintain camera's Z position
        transform.position = playerPosition;

        // Debug camera alignment
        Debug.DrawLine(transform.position - Vector3.up * camHeight / 2,
                       transform.position + Vector3.up * camHeight / 2, Color.green);
        Debug.DrawLine(transform.position - Vector3.right * camWidth / 2,
                       transform.position + Vector3.right * camWidth / 2, Color.green);
    }



    private void OnPostRender()
    {
        float cameraLeft = transform.position.x - cam.orthographicSize * cam.aspect;
        float cameraBottom = transform.position.y - cam.orthographicSize;

        GL.PushMatrix();
        GLdraw.SetPass(0);
        GL.LoadOrtho();

        BoundsInt bounds = tilemapShadowCaster.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (tilemapShadowCaster.HasTile(pos))
            {
                Vector3 cellWorldPos = tilemapShadowCaster.CellToWorld(pos);

                float left = (cellWorldPos.x - cameraLeft) / (cam.orthographicSize * 2 * cam.aspect);
                float right = (cellWorldPos.x + tilemapShadowCaster.cellSize.x - cameraLeft) / (cam.orthographicSize * 2 * cam.aspect);
                float bottom = (cellWorldPos.y - cameraBottom) / (cam.orthographicSize * 2);
                float top = (cellWorldPos.y + tilemapShadowCaster.cellSize.y - cameraBottom) / (cam.orthographicSize * 2);

                // Debug visual lines for the shadow bounds
                Debug.DrawLine(new Vector3(cellWorldPos.x, cellWorldPos.y, 0), new Vector3(cellWorldPos.x + tilemapShadowCaster.cellSize.x, cellWorldPos.y, 0), Color.red);
                Debug.DrawLine(new Vector3(cellWorldPos.x + tilemapShadowCaster.cellSize.x, cellWorldPos.y, 0), new Vector3(cellWorldPos.x + tilemapShadowCaster.cellSize.x, cellWorldPos.y + tilemapShadowCaster.cellSize.y, 0), Color.red);
                Debug.DrawLine(new Vector3(cellWorldPos.x + tilemapShadowCaster.cellSize.x, cellWorldPos.y + tilemapShadowCaster.cellSize.y, 0), new Vector3(cellWorldPos.x, cellWorldPos.y + tilemapShadowCaster.cellSize.y, 0), Color.red);
                Debug.DrawLine(new Vector3(cellWorldPos.x, cellWorldPos.y + tilemapShadowCaster.cellSize.y, 0), new Vector3(cellWorldPos.x, cellWorldPos.y, 0), Color.red);

                // Shadow rendering logic
                if (player.transform.position.x <= cellWorldPos.x && player.transform.position.y <= cellWorldPos.y)
                    DrawShadow(left, bottom, right, top);
                if (player.transform.position.x <= cellWorldPos.x && player.transform.position.y >= cellWorldPos.y)
                    DrawShadow(left, top, right, bottom);
                if (player.transform.position.x >= cellWorldPos.x && player.transform.position.y >= cellWorldPos.y)
                    DrawShadow(right, top, left, bottom);
                if (player.transform.position.x >= cellWorldPos.x && player.transform.position.y <= cellWorldPos.y)
                    DrawShadow(right, bottom, left, top);
            }
        }

        GL.PopMatrix();
    }


    void DrawShadow(float x1, float y1, float x2, float y2)
    {
        // Use player's normalized position for projection origin
        Vector3 playerPos = cam.WorldToScreenPoint(player.transform.position);
        float x = playerPos.x / Screen.width;
        float y = playerPos.y / Screen.height;

        int projected_length = length;
        float projx1 = x2 + (x2 - x) * projected_length;
        float projy1 = y1 + (y1 - y) * projected_length;
        float projx2 = x1 + (x1 - x) * projected_length;
        float projy2 = y2 + (y2 - y) * projected_length;

        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.black);

        GL.Vertex(new Vector3(x1, y1, 0));
        GL.Vertex(new Vector3(x2, y1, 0));
        GL.Vertex(new Vector3(projx1, projy1, 0));

        GL.Vertex(new Vector3(x1, y1, 0));
        GL.Vertex(new Vector3(projx2, projy1, 0));
        GL.Vertex(new Vector3(projx1, projy1, 0));

        GL.Vertex(new Vector3(x1, y1, 0));
        GL.Vertex(new Vector3(x1, y2, 0));
        GL.Vertex(new Vector3(projx2, projy2, 0));

        GL.Vertex(new Vector3(x1, y1, 0));
        GL.Vertex(new Vector3(projx2, projy1, 0));
        GL.Vertex(new Vector3(projx2, projy2, 0));

        GL.End();
    }


}
