using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneScript : MonoBehaviour
{
    private int message = 0;
    private bool isInPhone = false;
    [SerializeField] private float speed;
    [SerializeField] private GameObject phoneObject, mainCamera;

    void Start()
    {
        phoneObject.transform.GetChild(0).gameObject.SetActive(true);
        for (int i = 1; i < phoneObject.transform.childCount; i++)
        {
            phoneObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInPhone = true;
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            isInPhone = false;
        }

        if (isInPhone)
        {
            StopAllCoroutines();
            StartCoroutine(RotatePhone(new Vector3(-55, 58, -34.5f), new Vector3(35, 150, 0)));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(RotatePhone(new Vector3(-90, 0, 0), new Vector3(0, 180, 0)));
        }

    }

    public void ActionForMessages()
    {
        phoneObject.transform.GetChild(message).gameObject.SetActive(false);
        message++;
        phoneObject.transform.GetChild(message).gameObject.SetActive(true);
    }

    private IEnumerator RotatePhone(Vector3 angleOfHand, Vector3 angleOfCamera)
    {
        var me = transform;
        var to = Quaternion.Euler(angleOfHand);

        var meCam = mainCamera.transform;
        var toCam = Quaternion.Euler(angleOfCamera);

        while (true)
        {
            me.rotation = Quaternion.RotateTowards(me.rotation, to, speed * Time.deltaTime);
            meCam.rotation = Quaternion.RotateTowards(meCam.rotation, toCam, 2 *speed * Time.deltaTime);

            if (Quaternion.Angle(me.rotation, to) < 0.01f && Quaternion.Angle(meCam.rotation, toCam) < 0.01f)
            {
                me.rotation = to;
                meCam.rotation = toCam;
                yield break;
            }

            yield return null;
        }
    }
}
