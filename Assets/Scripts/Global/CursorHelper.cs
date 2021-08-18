using UnityEngine;

namespace Global {

public class CursorHelper : Cursor
{

    private static Texture2D currentCursor;
    public static new void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode) {
        if(texture != currentCursor) {
            Cursor.SetCursor(texture, hotspot, cursorMode);
            currentCursor = texture;
        }
    }

    public static RaycastHit2D RaycastCursor(int layerMask) =>
        Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.MaxValue, layerMask);
}

}