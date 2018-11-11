using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Control : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    Vector2 pos1;
    Vector2 pos2;
    Vector2 flightAngle;

    public Image knob;
    public float rad;


    public Character character;


    public void OnPointerDown(PointerEventData data)
    {
        pos1 = data.position;
        knob.gameObject.SetActive(true);
        knob.gameObject.transform.position = pos1;
    }

    public void OnDrag(PointerEventData data)
    {
        StartCoroutine(character.ToMax());

        pos2 = data.position;
        knob.gameObject.transform.position = pos2;

        flightAngle = (pos2 - pos1).normalized;
        character.flyAngle = flightAngle;
        if (Vector2.Distance(pos1, pos2) > rad)
        {
            knob.gameObject.transform.position = pos1 + flightAngle * rad;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        StartCoroutine(character.ToMin());

        knob.gameObject.transform.position = pos1;
        StartCoroutine(Miss());
    }
    

    IEnumerator Miss()
    {
        yield return new WaitForSeconds(2f);
        knob.gameObject.SetActive(false);
    }
}
