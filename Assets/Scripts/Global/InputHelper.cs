using UnityEngine;

namespace Global {

public static class InputHelper {

    public static bool Interact() =>
        Input.GetButtonDown("Interact");
}

}