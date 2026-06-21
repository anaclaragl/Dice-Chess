using UnityEngine;

public class DiceRoll : MonoBehaviour
{

    Rigidbody body;
    [SerializeField] private float maxRandomForceValue, startRollingForce;

    [SerializeField] private float forceX, forceY, forceZ;

    private KeyCode keyToRoll = KeyCode.E;

    public int diceFaceNum;

    private void Awake()
    {
        Initialize();
    }

    private bool IsDiceAtRest() =>
    body.GetComponent<Rigidbody>().linearVelocity == Vector3.zero;

    private bool IsClickedOnSelf()
    {
        if (!Input.GetMouseButtonDown(0)) return false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.name == gameObject.name;

    }

    // Update is called once per frame
    private void Update()
    {
        {
            if (body != null)

                if (IsDiceAtRest() && (Input.GetKeyDown(keyToRoll) || IsClickedOnSelf()))
                {
                    RollDice();
                }
        }
    }

    private void RollDice()
    {
        body.isKinematic = false;

        forceX = Random.Range(0, maxRandomForceValue);
        forceY = Random.Range(0, maxRandomForceValue);
        forceZ = Random.Range(0, maxRandomForceValue);

        body.AddForce(Vector3.up * startRollingForce);
        body.AddTorque(forceX, forceY, forceZ);
    }

    private void Initialize()
    {
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 0);
    }
}