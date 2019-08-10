using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    private bool ShowField = false;
	ParticleSystem field;
	void Start()
    {
        field = GetComponent<ParticleSystem>();
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
                field.Stop();
            }

            Debug.Log("Field is " + ShowField.ToString());
        }
    }
}
