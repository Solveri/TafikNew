using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{

    public static DamagePopUp Create(Vector3 position, int damageAmount)
    {
        GameObject damagePopupObj = Instantiate(GameManager.Instance.prefabPop, position, Quaternion.identity);
        DamagePopUp damagePopUp = damagePopupObj.GetComponent<DamagePopUp>();
        damagePopUp.SetUp(damageAmount);
        return damagePopUp; // Return the created instance
    }

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void SetUp(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        textColor = textMesh.color;
        dissappearTimer = 1f;
    }
    private float dissappearTimer;
    private Color textColor;
    private TextMeshPro textMesh;

    
    private void Update()
    {
        float moveYSpeed = 20f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        dissappearTimer -= Time.deltaTime;
        if (dissappearTimer<0)
        {
            float disappaerSpeed = 3f;
            textColor.a -= disappaerSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }

        }
    }
}
