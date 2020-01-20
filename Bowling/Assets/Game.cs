using UnityEngine;

public class Game : MonoBehaviour
{
    BallController ballController;
    PinsGameControler pinsControler;
    CourtainControler courtainControler;
    string ballConfig;

    public enum State { WAIT, AIM, SHOOT, COURTAIN, BALLGONE, Length };
    public State state { get; set; }
   

    // Start is called before the first frame update
    void Start()
    {
        state = State.AIM;
        ballController = GameObject.Find("Icosphere").GetComponent<BallController>(); // TODO tu można brać kolejene kule jak będą 
        ballController.Reset();
        pinsControler = GameObject.Find("PinHolder").GetComponent<PinsGameControler>();
        courtainControler = GameObject.Find("curtainGame").GetComponent<CourtainControler>();
    }

    private PinsGameControler GetPins()
    {
        return GameObject.Find("PinHolder").GetComponent<PinsGameControler>();
    }

    // Update is called once per frame
    void Update()   
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ballController.Reset();
            state = State.AIM;
        }
        if (Input.GetKeyDown(KeyCode.Z) && state == State.AIM)
        {
            courtainControler.MoveCourtine(pinsControler);
        }


        switch (state)
        {
            case State.AIM:
                if (Input.anyKey)
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))
                    {
                        ballController.Shoot();
                        state = State.SHOOT;
                    }

                    ballController.BallSetup();
                    ballConfig = ballController.GetCoords();  // print all ball config
                }
                break;
            case State.SHOOT:
                if (ballController.HasDone())
                {
                    state = State.BALLGONE;
                }
                break;
            case State.BALLGONE:
                courtainControler.MoveCourtine(pinsControler);
                ballController.Reset();
                state = State.AIM;
                break;
          
            default:
                return;
        }
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        ballConfig = ballController.GetCoords();

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperRight;
        style.fontSize = h * 3 / 100;
        style.normal.textColor = Color.green;
        GUI.Label(rect, ballConfig, style);
    }
}
