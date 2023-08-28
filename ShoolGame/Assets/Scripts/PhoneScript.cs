using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneScript : MonoBehaviour
{
    private int message = 0;
    [SerializeField] private float speed;
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
            StartCoroutine(RotatePhone(new Vector3(-55, 58, -34.5f), new Vector3(35, -30, 0) + mainCamera.parent.rotation.eulerAngles));
            Debug.Log(new Vector3(35, 150, 0) - mainCamera.parent.rotation.eulerAngles);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(RotatePhone(new Vector3(-90, 0, 0), mainCamera.parent.rotation.eulerAngles));
        }

    }

    public void ActionForMessages()
    {
        phoneObject.GetChild(message).gameObject.SetActive(false);
        message++;
        phoneObject.GetChild(message).gameObject.SetActive(true);
    }

    private IEnumerator RotatePhone(Vector3 angleOfHand, Vector3 angleOfCamera)
    {
        var me = transform;
        var to = Quaternion.Euler(angleOfHand);

        var meCam = mainCamera;
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


    //это все для того чтобы нормально вбивать в движке значения поворота
    //private IEnumerator RotatePhone1(Vector3 angleOfHand, Vector3 angleOfCamera)
    //{
    //    var meHand = transform.rotation;
    //    var toHand = Quaternion.Euler(angleOfHand);

    //    var meCam = mainCamera.localRotation;
    //    var toCam = Quaternion.Euler(angleOfCamera);

    //    //meCam.rotation *= mainCamera.parent.rotation;
    //    //toCam *= mainCamera.parent.rotation;

    //    Debug.Log(meCam.eulerAngles);

    //    while (true)
    //    {
    //        toCam = Quaternion.RotateTowards(meHand, toHand, speed * Time.deltaTime);
    //        meCam = Quaternion.RotateTowards(meCam, toCam, 2 * speed * Time.deltaTime);

    //        if (Quaternion.Angle(meHand, toHand) < 0.01f && Quaternion.Angle(meCam, toCam) < 0.01f)
    //        {
    //            meHand = toHand;
    //            meCam = toCam;
    //            yield break;
    //        }

    //        yield return null;
    //    }
    //}
}
