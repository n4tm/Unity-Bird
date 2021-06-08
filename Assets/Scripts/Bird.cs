using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = System.Diagnostics.Debug;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _birdWasLaunched;
    private const float _launchPower = 500;
    private float _timeSittingAround;
    private Vector3 _position;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        _position = transform.position;
        GetComponent<LineRenderer>().SetPosition(0, _position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        if (_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1) _timeSittingAround += Time.deltaTime;
        
        if (Mathf.Abs(transform.position.y) > 15  || Mathf.Abs(transform.position.x) > 15 || _timeSittingAround > 2)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        var directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdWasLaunched = true;
        GetComponent<LineRenderer>().enabled = false;
    }

    private void OnMouseDrag()
    {
        Debug.Assert(Camera.main != null, "Camera.main != null");
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }

}
