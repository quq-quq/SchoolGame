using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneScript : MonoBehaviour
{
    private int message = 0;
    [SerializeField] private float speed;
    private Coroutine phoneCoroutine;
    [SerializeField] private Transform phoneObject, mainCamera;

    private static bool isInPhone = false;
    static public bool IsInPhone
    {
        get => isInPhone;
        set { Debug.LogError("Can't because field's (IsInPhone) set is null"); }
    }

    void Start()
    {
        phoneObject.GetChild(0).gameObject.SetActive(true);
        for (int i = 1; i < phoneObject.childCount; i++)
        {
            phoneObject.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isInPhone = true;
        }
        if(Input.GetKeyUp(KeyCode.Q))
        {
            isInPhone = false;
        }

        if (isInPhone)
        {
            if (phoneCoroutine != null)
                StopCoroutine(phoneCoroutine);
            phoneCoroutine = StartCoroutine(CheckPhone(new Vector3(-55, 58, -34.5f)));
        }
        else
        {
            if (phoneCoroutine != null)
                StopCoroutine(phoneCoroutine);
            phoneCoroutine = StartCoroutine(CheckPhone(new Vector3(-70, 8, 0)));
        }
    }

    public void ActionForMessages()
    {
        phoneObject.GetChild(message).gameObject.SetActive(false);
        message++;
        phoneObject.GetChild(message).gameObject.SetActive(true);
    }

    private IEnumerator CheckPhone(Vector3 angleOfHand)
    {
        var me = transform;
        var to = Quaternion.Euler(angleOfHand);

        while (true)
        {
            me.rotation = Quaternion.RotateTowards(me.rotation, to, speed * Time.deltaTime);

            if (Quaternion.Angle(me.rotation, to) < 0.01f)
            {
                me.rotation = to;
                yield break;
            }

            yield return null;
        }
    }
}
