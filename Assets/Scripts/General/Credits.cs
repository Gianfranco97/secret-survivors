using UnityEngine;

public class Credits : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Animator>().SetBool("showWin", SceneInfo.SceneOrigin == SceneOrigin.Game);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
