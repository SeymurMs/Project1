using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideController : MonoBehaviour
{
    // Slide variables // Inputs // references

    [Header("Slider Variables")]
    [SerializeField] float _slideForce = 200f;
    [SerializeField] float _maxSlidetime = .75f;
    [SerializeField] float _currentSlideTime = 0;
    //
    [SerializeField] float _startYScale;
    [SerializeField] float _slideYScale = 0.2f;


    [Space]
    [Header("Inputs")]
    float _horizontalMove;
    float _verticalMove;
    KeyCode _slideKey = KeyCode.LeftControl;

    [Space]
    [Header("References")]
    [SerializeField] Transform _orientation;
    [SerializeField] Transform _playerRig;
    [SerializeField] Rigidbody _rb;
    [SerializeField] PlayerController _pcMaster;
    private void Start()
    {
        
        _startYScale = _playerRig.localScale.y;
    }
    private void FixedUpdate()
    {
        if (_pcMaster.IsSlide)
            ContineSlide();
    }
    private void Update()
    {
        MyInputs();
    }
    void MyInputs()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal");
        _verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(_slideKey) && (_horizontalMove != 0 || _verticalMove != 0))
        {
            if (!_pcMaster.IsSlide)
                StartSlide();
        }
        if (Input.GetKeyUp(_slideKey) && _pcMaster.IsSlide)
            EndSlide();
    }
    void StartSlide()
    {
        _pcMaster.IsSlide = true;
        _playerRig.localScale = new Vector3(_playerRig.localScale.x, _slideYScale, _playerRig.localScale.z);
        _currentSlideTime = _maxSlidetime;
        _rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
    }
    void EndSlide()
    {
        _pcMaster.IsSlide = false;
        _playerRig.localScale = new Vector3(_playerRig.localScale.x, _startYScale, _playerRig.localScale.z);
    }
    void ContineSlide()
    {
        Vector3 inputDirection = _orientation.right * _horizontalMove + _orientation.forward * _verticalMove;
        if (_pcMaster.OnSlope())
        {
            _rb.AddForce(_pcMaster.GetSlopeDirection(inputDirection) * _slideForce, ForceMode.Force);
        }
        else
        {
            _rb.AddForce(_pcMaster.GetSlopeDirection(inputDirection) * _slideForce, ForceMode.Force);
            _currentSlideTime -= Time.deltaTime;
        }
        if (_currentSlideTime <= 0)
            EndSlide();
    }



}
