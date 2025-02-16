using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float maxVidas;
    [SerializeField] private float currentVidas;
    [SerializeField]
    private Image healthBarCurrent;
    [SerializeField]
    private Image healthBarTotal;

    void Update()
    {
        if(healthBarCurrent != null)
        {
            healthBarCurrent.fillAmount = currentVidas / 10;
        }
        if(healthBarTotal != null)
        {
            healthBarTotal.fillAmount = maxVidas / 10;
        }
    }

    public void RecibirDanho(float danhoRecibido)
    {
        currentVidas -= danhoRecibido;
        if(currentVidas <= 0)
        {
            Destroy(this.gameObject);
        }
        //AudioManager.Instace.PlaySfx("hit");
    }

    public void CurarDanho(float vidaRecibida)
    {
        currentVidas += vidaRecibida;
        if(currentVidas >= maxVidas)
        {
            currentVidas = maxVidas;
        }
    }

    public bool isFullLife()
    {
        return currentVidas >= maxVidas;
    }
}
