using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridTrigger : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;
    [SerializeField] private float delay;
    private void Start() {
        this.gameObject.AddComponent<BoxCollider2D>();  
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 0.9f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            StartCoroutine(SceneLoad(delay));
        }
    }

    private IEnumerator SceneLoad(float timeDelay)
    {
        float temp = 0;
        while(timeDelay > temp){
            temp += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
