using TMPro;
using UnityEngine;

public class HealTextMoving : MonoBehaviour
{
    [SerializeField] private float timeOfLiving;
    [SerializeField] private float speedOfMoving;
    private float timer;

    public void Initialization(float _heal)
    {
        this.GetComponent<TextMeshProUGUI>().color = Color.green;
        this.GetComponent<TextMeshProUGUI>().text = "+" + _heal;
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
