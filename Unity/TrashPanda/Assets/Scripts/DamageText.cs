using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DamageText : MonoBehaviour {

   public TextMeshProUGUI text;
  

	// Use this for initialization
	void Start () {
        text = GetComponent<TMPro.TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowDamage(int amount, DamageType damageType)
    {
        string colorText = GetColorText(damageType);
        text.SetText(colorText + amount);
        StartCoroutine(FadeUp());
    }

    private IEnumerator FadeUp()
    {
        transform.localPosition = Vector3.zero;
        float elapsed = 0f;
        while (elapsed < 0.5f)
        {
            transform.position += Vector3.up * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        text.SetText(string.Empty);
    }

   private string GetColorText(DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Range: return "<color=red>";
            case DamageType.Physical: return "<color=white>";
            case DamageType.Enviroment: return "<color=yellow>";

            default: return string.Empty;
        }
    }
    
}
