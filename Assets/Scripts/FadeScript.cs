using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    Image img;
    Color tempColor;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
            tempColor = img.color;
            tempColor.a = 1f;
            img.color = tempColor;
        StartCoroutine(FadeOut(0.05f));
    }

    IEnumerator FadeOut(float seconds)
    {
        for (float a = 1f; a >= 0f; a -= 0.05f)
        {
            tempColor = img.color;
            tempColor.a = a;
            img.color = tempColor;
            yield return new WaitForSeconds(seconds);
        }
        
        img.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0f); // Ensure full transparency
        img.raycastTarget = false;
        gameObject.SetActive(false); // Completely disable overlay
    }


    public IEnumerator FadeIn(float seconds)
    {
        img.raycastTarget = true;
        for (float a = 0f; a <= 1.05f; a += 0.05f)
        {
            tempColor = img.color;
            tempColor.a = a;
            img.color = tempColor;
            yield return new WaitForSeconds(seconds);
        }
    }
}
