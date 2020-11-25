using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emote : MonoBehaviour
{
    Sprite sprite;
    Color _col;
    [SerializeField] Renderer rend1, rend2;
    [SerializeField] SpriteRenderer sRend1, sRend2;
    [SerializeField] float riseSpeed, alphaRate; //riseSpeed - units per sec to go up || alphaRate - alpha to "lose" in a second from 1.0 to 0.0

    float _currentAlpha;
    private void Start()
    {
        _currentAlpha = 1f;
        float deathTime = 1f / alphaRate;

        Destroy(gameObject, deathTime);
    }
    private void Update()
    {
        transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);

        _col = sRend1.material.GetColor("_Color");
        _col.a = _currentAlpha;

        sRend1.material.SetColor("_Color", _col);
        sRend2.material.SetColor("_Color", _col);
        

        _currentAlpha -= alphaRate * Time.deltaTime;
    }
    public void SetEmote(Sprite newSprite)
    {
        sprite = newSprite;
        sRend1.sprite = sprite;
        sRend2.sprite = sprite;
        
        _col = sRend1.material.GetColor("_Color");
    }
}
