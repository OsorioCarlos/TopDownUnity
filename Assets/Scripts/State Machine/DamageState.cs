using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : State<Enemy>
{

    public override void OnEnterState(Enemy controller)
    {
        base.OnEnterState(controller);
        StartCoroutine(DamageEffectSequence(controller.GetComponent<SpriteRenderer>(), Color.red, .5f, .5f));
    }

    IEnumerator DamageEffectSequence(SpriteRenderer sr, Color dmgColor, float duration, float delay)
    {
        Color originColor = sr.color;
        sr.color = dmgColor;
        yield return new WaitForSeconds(delay);
        for (float t = 0; t < 1.0f; t += Time.deltaTime/duration)
        {
            sr.color = Color.Lerp(dmgColor, originColor , t);

            yield return null;
        }
        sr.color = originColor;
    }

    public override void OnUpdateState()
    {

    }

    public override void OnExitState()
    {
    }
}
