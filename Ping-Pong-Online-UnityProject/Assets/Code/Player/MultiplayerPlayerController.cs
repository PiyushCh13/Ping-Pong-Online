using UnityEngine;
using SocketIOClient;
using Newtonsoft.Json.Linq;

public class PlayerController : MonoBehaviour
{
    public bool isLocalPlayer;
    private SocketIOUnity socket;
    private Rigidbody2D rb;
    private float moveSpeed = 5f;
    private string username;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Get the existing Socket.IO instance
        socket = SocketManager.Instance.socket;

        if (isLocalPlayer)
        {
            socket.On("updatePlayerPosition", OnPlayerMove);
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            float moveY = Input.GetAxis("Vertical"); 
            rb.linearVelocity = new Vector2(0, moveY * moveSpeed);

            // Send player movement to the server
            JObject data = new JObject
            {
                ["username"] = username,
                ["position"] = transform.position.y
            };
            socket.Emit("playerMove", data);
        }
    }

    void OnPlayerMove(SocketIOResponse response)
    {
        JObject data = response.GetValue<JObject>();

        string receivedUsername = data["username"]?.ToString();
        float newY = data["position"]?.ToObject<float>() ?? 0f;

        // Only update opponent's position
        if (receivedUsername != username)
        {
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    public void Setup(string name, bool local)
    {
        username = name;
        isLocalPlayer = local;
    }
}
