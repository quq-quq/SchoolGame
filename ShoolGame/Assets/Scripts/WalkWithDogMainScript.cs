using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkWithDogMainScript : MonoBehaviour
{
    private bool IsDogChecking = false;
    [SerializeField] float walkSpeed, dogCheckSpeed;
    private Coroutine dogCoroutine;
    [SerializeField] private Transform head;


    void Start()
    {
        
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput > 0 && !PhoneScript.IsInPhone && !IsDogChecking)
        {
            transform.Translate(new Vector3(0, 0, verticalInput) * walkSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.E) && !PhoneScript.IsInPhone)
        {
            IsDogChecking = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            IsDogChecking = false;
        }

        if (IsDogChecking)
        {
            if (dogCoroutine != null)
                StopCoroutine(dogCoroutine);
            dogCoroutine = StartCoroutine(CheckDog(new Vector3(-60, 76.5f, 0)));
        }
        else
        {
            if(dogCoroutine != null)
                StopCoroutine(dogCoroutine);
            dogCoroutine = StartCoroutine(CheckDog(new Vector3(0, 0, 0)));
        }

        
    }

    private IEnumerator CheckDog(Vector3 angleOfCamera)
    {
        var meCam = head;
        var toCam = Quaternion.Euler(angleOfCamera);

        while (true)
        {
            meCam.rotation = Quaternion.RotateTowards(meCam.localRotation, toCam, 2 * dogCheckSpeed * Time.deltaTime);

            if (Quaternion.Angle(meCam.localRotation, toCam) < 0.01f)
            {
                meCam.localRotation = toCam;
                yield break;
            }

            yield return null;
        }
    }
}
