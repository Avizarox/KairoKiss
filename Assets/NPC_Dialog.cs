using System.Collections;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class NPC_Dialog : MonoBehaviour
{
    [TextArea] public string dialog;
    [SerializeField] Sprite dialogWindow;
    [SerializeField] float fontSize;
    [SerializeField] TMP_FontAsset font;
    private GameObject empty;

    private GameObject Panel, canvasText, canvasPanel, ghostCanvasPanel, col2D;
    private TMP_Text text, ghostText;
    private CircleCollider2D _col;
    RectTransform rectTransform;
    float height;
    void Start()
    {
        empty = new GameObject();

        col2D = Instantiate(empty, transform.position, Quaternion.identity, transform);
        col2D.AddComponent<BoxCollider2D>().size = new Vector2(1,1);

        //Creating Canvas for other UI elements and assigning some properities
        Panel = Instantiate(empty, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity, transform);
        Panel.name = "Canvas";
        Panel.AddComponent<Canvas>();
        Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 0.56f);

        //Creating Text object
        canvasText = Instantiate(empty, Panel.transform);
        canvasText.name = "Text";
        text = canvasText.AddComponent<TMPro.TextMeshPro>();
        canvasText.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 0.56f);
        text.alignment = TextAlignmentOptions.TopLeft;
        text.GetComponent<Renderer>().sortingOrder = 1;

        //Creating Panel object
        canvasPanel = Instantiate(empty, Panel.transform);
        canvasPanel.name = "Panel";
        canvasPanel.AddComponent<Image>().sprite = dialogWindow;
        canvasPanel.GetComponent<Image>().type = Image.Type.Sliced;
        canvasPanel.GetComponent<Image>().pixelsPerUnitMultiplier = 32;
        canvasPanel.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);

        //creating ghostText object to calculate some shit
        ghostCanvasPanel = Instantiate(empty, Panel.transform);
        ghostCanvasPanel.name = "GhostText";
        ghostText = ghostCanvasPanel.AddComponent<TMPro.TextMeshPro>();
        ghostText.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 0.56f);
        ghostText.alignment = TextAlignmentOptions.TopLeft;
        ghostCanvasPanel.GetComponent<MeshRenderer>().enabled = false;
        
        Panel.SetActive(false);

        _col = this.gameObject.AddComponent<CircleCollider2D>();
        _col.isTrigger = true;
        _col.radius = 1f;
        
        //text settings
        text.font = font;
        text.color = new Color(0,0,0);
        text.fontSize = fontSize;
        ghostText.font = font;
        ghostText.fontSize = text.fontSize;
        ghostText.text = dialog;
        
        //calculating some shit
        height = ghostText.preferredHeight;
        text.margin = new Vector4(0, -height - 0.5f, 0,0);
        canvasPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(5, height + 1);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Panel.SetActive(true);
        StartCoroutine(ShowDialog(dialog));
    }

    private void OnTriggerExit2D(Collider2D other) {
        text.text = String.Empty;
        Panel.SetActive(false);
    }

    private IEnumerator ShowDialog(string dialog){
        this.dialog = dialog;
        for(int i = 0; i < dialog.Length; i++){
            if(!Physics2D.OverlapCircle(transform.position, 1f, 1<<9)){
                text.text = String.Empty;
                break;
            }
            else{
                text.text += dialog[i];
            }
            yield return new WaitForSeconds(0.08f);
        }
    }
}