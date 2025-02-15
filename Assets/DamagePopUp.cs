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
        dissappearTimer = 0.3f;
    }
    private float dissappearTimer;
    private Color textColor;
    private TextMeshPro textMesh;

    
    private void Update()
    {
        if (GameManager.Instance.isGamePasued)
        {
            return;
        }
        float moveYSpeed = 5f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        dissappearTimer -= Time.deltaTime;
        if (dissappearTimer<0)
        {
            float disappaerSpeed = 5f;
            textColor.a -= disappaerSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }

        }
    }
}
