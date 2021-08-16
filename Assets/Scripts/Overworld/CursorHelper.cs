using UnityEngine;

namespace Overworld {

public class CursorHelper : Cursor
{

    private static Texture2D currentCursor;
    public static new void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode) {
        if(texture != currentCursor) {
            Cursor.SetCursor(texture, hotspot, cursorMode);
            currentCursor = texture;
        }
    }   
}

}