using System.Collections;
using TMPro;
using UnityEngine;
using System;

public class NPC_Dialog : MonoBehaviour
{
    [TextArea] public string dialog;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject Text;
    private MeshRenderer textMesh;
    private TextMeshPro text;
    private Collider2D _col;
    RectTransform rectTransform;
    void Start()
    {
        panel.SetActive(false);

        _col = this.gameObject.AddComponent<CircleCollider2D>();
        _col.isTrigger = true;

        text = Text.gameObject.AddComponent<TextMeshPro>();
        text.color = new Color(0,0,0);
        text.fontSize = 5;
        text.alignment = TextAlignmentOptions.Center;
        text.alignment = TextAlignmentOptions.Bottom;

        rectTransform = Text.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(5,0.56f);
        
        //ResizeWindow(dialog, rectTransform);

        textMesh = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        panel.SetActive(true);
        StartCoroutine(ShowDialog(dialog));
    }

    private void OnTriggerExit2D(Collider2D other) {
        text.text = String.Empty;
        panel.SetActive(false);
    }

    private IEnumerator ShowDialog(string dialog){
        this.dialog = dialog;
        //ResizeWindow(dialog, rectTransform);
        for(int i = 0; i < dialog.Length; i++){
            if(!Physics2D.OverlapCircle(transform.position, 0.5f, 1<<9)){
                text.text = String.Empty;
                break;
            }
            else{
                text.text += dialog[i];
            }
            yield return new WaitForSeconds(0.08f);
        }
    }
    /*private void ResizeWindow(string Text, RectTransform windowRect){
        windowRect.sizeDelta = new Vector2(5.6f,1f + text.preferredHeight*2f);
    }*/
}
