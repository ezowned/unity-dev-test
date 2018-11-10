using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallsSwitcher : MonoBehaviour
{

    public CameraController cameraController;
    public BallController[] balls = new BallController[0];
    public UnityEngine.UI.Slider ballSpeedSlider;


    private int currentBallIndex = 0;
    void Start()
    {
        if (balls.Length == 0)
        {
            return;
        }
        balls[0].SetPositions(TrajectoryLoader.Load("ball_path"));
        balls[1].SetPositions(TrajectoryLoader.Load("ball_path2"));
        balls[2].SetPositions(TrajectoryLoader.Load("ball_path3"));
        balls[3].SetPositions(TrajectoryLoader.Load("ball_path4"));
        cameraController.target = balls[0].transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            SetBall((balls.Length + currentBallIndex - 1) % balls.Length);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            SetBall((currentBallIndex + 1) % balls.Length);
        }
        ballSpeedSlider.value = balls[currentBallIndex].speed;

    }

    void SetBall(int index)
    {
        balls[index].speed = balls[currentBallIndex].speed;
        balls[currentBallIndex].speed = 0;
        currentBallIndex = index;
        ballSpeedSlider.value = balls[index].speed;
        cameraController.target = balls[index].transform;
    }

    public void OnUpdateSliderValue()
    {
        balls[currentBallIndex].speed = ballSpeedSlider.value;
    }
}
