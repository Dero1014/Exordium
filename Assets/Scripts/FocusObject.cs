using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FocusObject : MonoBehaviour
{
    //public
    public float FocusTime;

    [Space(10)]

    public Transform FocusedObject;

    //private
    private CinemachineVirtualCamera _cVM;
    private PlayerInput _playerInput;
    private Transform _player;

    private void Start()
    {
        _cVM = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        _player = _cVM.Follow;
        _playerInput = GameObject.FindObjectOfType<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //start focusing on object
            _cVM.Follow = FocusedObject;
            _playerInput.InputOn = false;
            StartCoroutine(FocusingObject());
        }
    }

    IEnumerator FocusingObject()
    {

        yield return new WaitForSeconds(FocusTime);
        _cVM.Follow = _player;
        _playerInput.InputOn = true;
    }

}
