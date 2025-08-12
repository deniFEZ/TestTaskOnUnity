using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DamageTextMoving : MonoBehaviour
{
    [SerializeField] private float timeOfLiving;
    [SerializeField] private float speedOfMoving;
    private float timer;

    public void Initialization(float _damage)
    {
        this.GetComponent<TextMeshProUGUI>().color = Color.red;
        this.GetComponent<TextMeshProUGUI>().text = "-" + _damage;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < timeOfLiving)
        {
            this.transform.position += new Vector3(0, speedOfMoving, 0);
            changeAlphaValue();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void changeAlphaValue()
    {
        var col = this.GetComponent<TextMeshProUGUI>().color;
        col.a = 1f - (timer / timeOfLiving);
        this.GetComponent<TextMeshProUGUI>().color = col;
    }
}
