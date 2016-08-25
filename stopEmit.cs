using UnityEngine;
using System.Collections;

public class stopEmit : MonoBehaviour {

    public float time;
    public ParticleEmitter _this;

    public void OnEnable()
    {
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(time);
        _this.particleEmitter.Emit(0);
    }
}
