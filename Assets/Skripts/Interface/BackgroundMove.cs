using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundMove : MonoBehaviour
{
    public BoxCollider2D Bounds;
    private Vector3 min, max;

    void Start()
    {
        Cursor.SetCursor(Resources.Load<Texture2D>("Sprite/InstrumentsPanel/arrow2"), Vector2.zero, CursorMode.Auto);
        min = Bounds.bounds.min;
        max = Bounds.bounds.max;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            float x = Input.GetAxis("Mouse X") * 0.45f;
            float y = Input.GetAxis("Mouse Y") * 0.45f;

            float cameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);
            x = Mathf.Clamp(Camera.main.transform.position.x - x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
            y = Mathf.Clamp(Camera.main.transform.position.y - y, min.y + Camera.main.orthographicSize, max.y - Camera.main.orthographicSize);

            Camera.main.transform.position = new Vector3(x, y, Camera.main.transform.position.z);
        }
    }
}
