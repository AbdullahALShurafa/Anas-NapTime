using UnityEngine;
using System.Collections;

public class LevelTriggers : MonoBehaviour {

	public ParticleSystem RainVFX;

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			RainVFX.Play();
		}
	}
}
