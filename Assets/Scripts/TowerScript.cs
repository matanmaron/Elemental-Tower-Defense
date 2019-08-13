using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public bool ShowField = false;
	ParticleSystem field;
	void Start()
    {
        field = GameObject.Find("FieldEffect").GetComponent<ParticleSystem>();
        field.Stop();
    }

    void Update()
    {

    }

    public void ToggleField()
    {
        ShowField = !ShowField;
        if (field != null)
        {

            if (ShowField)
            {
                field.Play();
            }
            else
            {
                field.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }

            Debug.Log("Field is " + ShowField.ToString());
        }
    }
}
