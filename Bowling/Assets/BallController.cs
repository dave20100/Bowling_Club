using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    static Vector3 startPosition = new Vector3(-115, 14, 15.7f);
    static Quaternion startRotation = new Quaternion(90f, 0f, 0f, 0f);

    private BallParam _horizontalPosition = new BallParam(startPosition.z - 5, startPosition.z + 5);
    private BallParam _high = new BallParam(startPosition.y, startPosition.y + 6);
    private BallParam _angle = new BallParam(-7, 7);
    private BallParam _streinght = new BallParam(0.0f, 600.0f);
    private BallParam _rotationX = new BallParam(-1000.0f, 1000.0f);
    private BallParam _rotationY = new BallParam(-1000.0f, 1000.0f);
    private BallParam _rotationZ = new BallParam(-1000.0f, 1000.0f);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        GetComponent<Rigidbody>().isKinematic = true;

        transform.position = startPosition;
        transform.rotation = startRotation;
        _high.Value = 1;
        
    }

    public void Shoot()
    {
        var vForce = Quaternion.AngleAxis(_angle.GetVal(), Vector3.up) * Vector3.right;

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(vForce * _streinght.GetVal(), ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(new Vector3(_rotationX.GetVal(), _rotationY.GetVal(), _rotationZ.GetVal()));
    }

    public void BallSetup()
    {
        if ((Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.KeypadPlus)) || (Input.GetAxis("XboxHorizontal2") < -0.8))
            _horizontalPosition.Value += 1;
        else if ((Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.KeypadMinus)) || (Input.GetAxis("XboxHorizontal2") > 0.8))
            _horizontalPosition.Value -= 1;
        else if ((Input.GetKey(KeyCode.Alpha2) && Input.GetKey(KeyCode.KeypadPlus)) || (Input.GetAxis("XboxVertical2") > 0.8))
            _high.Value += 1;
        else if ((Input.GetKey(KeyCode.Alpha2) && Input.GetKey(KeyCode.KeypadMinus)) || (Input.GetAxis("XboxVertical2") < -0.8))
            _high.Value -= 1;
        else if ((Input.GetKey(KeyCode.Alpha3) && Input.GetKey(KeyCode.KeypadPlus)) || (Input.GetKey(KeyCode.JoystickButton2) && Input.GetAxis("XboxHorizontal") > 0.8))
            _angle.Value += 1;
        else if ((Input.GetKey(KeyCode.Alpha3) && Input.GetKey(KeyCode.KeypadMinus)) || (Input.GetKey(KeyCode.JoystickButton2) && Input.GetAxis("XboxHorizontal") < -0.8))
            _angle.Value -= 1;
        else if ((Input.GetKey(KeyCode.Alpha4) && Input.GetKey(KeyCode.KeypadPlus)) || (Input.GetKey(KeyCode.JoystickButton1) && Input.GetAxis("XboxHorizontal") > 0.8))
            _streinght.Value += 1;
        else if ((Input.GetKey(KeyCode.Alpha4) && Input.GetKey(KeyCode.KeypadMinus)) || (Input.GetKey(KeyCode.JoystickButton1) && Input.GetAxis("XboxHorizontal") < -0.8))
            _streinght.Value -= 1;
        else if ((Input.GetKey(KeyCode.Alpha5) && Input.GetKey(KeyCode.KeypadPlus)) || (Input.GetKey(KeyCode.JoystickButton3) && (Input.GetAxis("XboxRotationX") > 0.8)))
            _rotationX.Value += 1;           
        else if ((Input.GetKey(KeyCode.Alpha5) && Input.GetKey(KeyCode.KeypadMinus)) || (Input.GetKey(KeyCode.JoystickButton3) && (Input.GetAxis("XboxRotationX") < -0.8)))
            _rotationX.Value -= 1;
        else if ((Input.GetKey(KeyCode.Alpha6) && Input.GetKey(KeyCode.KeypadPlus)) || (Input.GetKey(KeyCode.JoystickButton3) && (Input.GetAxis("XboxRotationY") > 0.8)))
            _rotationY.Value += 1;
        else if ((Input.GetKey(KeyCode.Alpha6) && Input.GetKey(KeyCode.KeypadMinus)) || (Input.GetKey(KeyCode.JoystickButton3) && (Input.GetAxis("XboxRotationY") < -0.8)))
            _rotationY.Value -= 1;
        else if ((Input.GetKey(KeyCode.Alpha7) && Input.GetKey(KeyCode.KeypadPlus)) || ( Input.GetKey(KeyCode.JoystickButton3) && (Input.GetAxis("XboxAxisZ") > 0.8)))
            _rotationZ.Value += 1;
        else if ((Input.GetKey(KeyCode.Alpha7) && Input.GetKey(KeyCode.KeypadMinus)) || (Input.GetKey(KeyCode.JoystickButton3) && Input.GetAxis("XboxAxisZ") < -0.8))
            _rotationZ.Value -= 1;

        Refresh();
    }

    public void Refresh()
    {
        transform.position = new Vector3(transform.position.x, _high.GetVal(), _horizontalPosition.GetVal());
    }

    public string GetCoords()
    {
        return
            string.Format(
                "Horizontal: {0}\n" +
                "High: {1}\n" +
                "Angle: {2}\n" +
                "Streinght: {3}\n" +
                "Rotation: {4}, {5}, {6}"
                ,_horizontalPosition.Value
                ,_high.Value
                ,_angle.Value
                ,_streinght.Value
                ,_rotationX.Value
                ,_rotationY.Value
                ,_rotationZ.Value);
    }

    public bool HasDone()
    {
        return transform.position.y < -2;
    }
}

public class BallParam
{
    private readonly int _step;
    private readonly float _thresholdDown;
    private readonly float _thresholdUp;
    private readonly float _rangeStep;
    private int _value;
    public int Value
    {
        get { return _value; }
        set
        {
            _value = value > _step ? _step : value < 1 ? 1 : value;
        }
    }

    public BallParam(float thresholdDown, float thresholdUp, int step = 100)
    {
        _thresholdDown = thresholdDown;
        _thresholdUp = thresholdUp;
        _rangeStep = (thresholdUp - thresholdDown)/(float)step;
        _step = step;
        _value = _step / 2;
    }

    public float GetVal()
    {
        return (_rangeStep * Value) + _thresholdDown;
    }
}