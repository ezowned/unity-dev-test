using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class BallController : MonoBehaviour
{

    enum MovementState
    {
        Start,          //движение ещё не начато
        End,            //шар в движении
        Moving,         //достигнута конечна точка траектории
    }
    public float speed = 1f;

    private MovementState _state = MovementState.Start; 
    private Vector3[] _positions = new Vector3[0];
    private LineRenderer _lineRenderer;
    private float _trajectoryLength;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPositions(Vector3[] positions)
    {
        _positions = positions;
        _state = MovementState.Start;
        if (positions.Length != 0)
            transform.position = positions[0];
        _lineRenderer.SetPositions(positions);
        _lineRenderer.positionCount = 0;
        _trajectoryLength = 0;
        for (int i = 0; i < positions.Length - 1; i++)
        {
            _trajectoryLength += (positions[i] - positions[i + 1]).magnitude;
        }
    }




    IEnumerator MoveSphere()
    {
        _state = MovementState.Moving;
        int i0 = 0, i1 = 0;
        float t = 0;

        while (true)
        {

            i0 = (int)Mathf.Floor(t);
            i1 = (int)Mathf.Ceil(t);
            if (i0 < 0 || i1 >= _positions.Length)
            {
                break;
            }
            else
            {
                transform.position = Vector3.Lerp(_positions[i0], _positions[i1], t - Mathf.Floor(t));
                _lineRenderer.positionCount = i1 + 1;
                _lineRenderer.SetPosition(i1, transform.position);

                t += Time.deltaTime * speed / _trajectoryLength * _positions.Length;
                yield return new WaitForEndOfFrame();
            }
        }

        _state = MovementState.End;
    }


    public void OnDoubleClick()
    {
        if (_state != MovementState.Start)
        {
            _state = MovementState.Start;
            transform.position = _positions[0];
            _lineRenderer.positionCount = 0;
            StopAllCoroutines();
        }

    }

    public void OnClick()
    {
        if (_state != MovementState.Moving)
        {
            StartCoroutine(MoveSphere());
        }
        else
        {
            speed = Mathf.Max(speed, 1);
        }
    }

}
