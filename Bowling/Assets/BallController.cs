using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    static Vector3 startPosition = new Vector3(-115, 14, 15.7f);
    static Quaternion startRotation = new Quaternion(90f, 0f, 0f, 0f);

    private BallParam _horizontalPosition = new BallParam(startPosition.z - 5, startPosition.z + 5);
    private BallParam _high = new BallParam(startPosition.y, startPosition.y + 6);
    private BallParam _angle = new BallParam(-10, 10);
    private BallParam _streinght = new BallParam(0.0f, 1000.0f);
    private BallParam _rotationX = new BallParam(-10.0f, 10.0f);
    private BallParam _rotationY = new BallParam(-10.0f, 10.0f);
    private BallParam _rotationZ = new BallParam(-10.0f, 10.0f);



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

    private void _Shoot()
    {
        var vForce = Quaternion.AngleAxis(_angle.GetVal(), Vector3.up) * Vector3.right;

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(vForce * _streinght.GetVal(), ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(new Vector3(_rotationX.GetVal(), _rotationY.GetVal(), _rotationZ.GetVal()));
    }

    public void BallSetup()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _Shoot();
        else if (Input.GetKey(KeyCode.L))
            _horizontalPosition.Value += 1;
        else if (Input.GetKey(KeyCode.P))
            _horizontalPosition.Value -= 1;
        else if (Input.GetKey(KeyCode.O))
            _high.Value += 1;
        else if (Input.GetKey(KeyCode.K))
            _high.Value -= 1;
        else if (Input.GetKey(KeyCode.I))
            _angle.Value += 1;
        else if (Input.GetKey(KeyCode.J))
            _angle.Value -= 1;
        else if (Input.GetKey(KeyCode.U))
            _streinght.Value += 1;
        else if (Input.GetKey(KeyCode.H))
            _streinght.Value -= 1;
        else if (Input.GetKey(KeyCode.Y))
            _rotationX.Value += 1;
        else if (Input.GetKey(KeyCode.G))
            _rotationX.Value -= 1;
        else if (Input.GetKey(KeyCode.M))
            _rotationY.Value += 1;
        else if (Input.GetKey(KeyCode.N))
            _rotationY.Value -= 1;
        else if (Input.GetKey(KeyCode.B))
            _rotationZ.Value += 1;
        else if (Input.GetKey(KeyCode.V))
            _rotationZ.Value -= 1;

        else if (Input.GetKey(KeyCode.X))
            Reset();

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