using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Events", menuName = "ScriptableObjects/Events", order = 4)]
public class Events: ScriptableObject {

    public void Empty() {}

    public void Dummy() => 
        Debug.Log("Dummy Event has been triggered");

    public void StartBattle() =>
        SceneManager.LoadSceneAsync("Battle");
}