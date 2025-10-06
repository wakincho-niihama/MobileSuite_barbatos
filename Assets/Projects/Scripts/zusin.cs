using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RobotWalk : MonoBehaviour
{
    public float speed = 2f;
    public float rotateSpeed = 120f;
    public float stepHeight = 0.1f;
    public float stepFrequency = 2f;

    private Animator animator;
    private float stepTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // WASDで移動
        float move = 0f;
        if (Input.GetKey(KeyCode.W)) move = 1f;
        else if (Input.GetKey(KeyCode.S)) move = -1f;

        float turn = 0f;
        if (Input.GetKey(KeyCode.A)) turn = -1f;
        else if (Input.GetKey(KeyCode.D)) turn = 1f;

        // 前後移動・左右回転
        transform.Translate(Vector3.forward * move * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * turn * rotateSpeed * Time.deltaTime);

        bool isWalking = Mathf.Abs(move) > 0.01f || Mathf.Abs(turn) > 0.01f;
        animator.SetBool("isWalking", isWalking);

        // 上下振動（前後移動はそのまま）
        if (isWalking)
        {
            stepTimer += Time.deltaTime * stepFrequency * Mathf.PI * 2;
            float offsetY = Mathf.Abs(Mathf.Sin(stepTimer)) * stepHeight;

            // 現在の transform.position の y に加算
            Vector3 pos = transform.position;
            pos.y = offsetY; // 地面からの高さに合わせる場合はオフセットを加える
            transform.position = new Vector3(pos.x, offsetY, pos.z);
        }
        else
        {
            stepTimer = 0f;
        }
    }
}
