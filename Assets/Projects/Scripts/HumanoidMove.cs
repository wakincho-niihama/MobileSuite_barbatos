using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HumanoidMove : MonoBehaviour
{
    public float speed = 10.0f;      // 歩く速さ
    public float rotateSpeed = 120f; // 向きの回転速度
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 前後移動入力
        float move = 0f;
        if (Input.GetKey(KeyCode.W))
            move = 1f;
        else if (Input.GetKey(KeyCode.S))
            move = -1f;

        // 左右回転入力
        float turn = 0f;
        if (Input.GetKey(KeyCode.A))
            turn = -1f;
        else if (Input.GetKey(KeyCode.D))
            turn = 1f;

        // 実際に動かす
        transform.Translate(Vector3.forward * move * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * turn * rotateSpeed * Time.deltaTime);

        // アニメーション反映
        bool isWalking = Mathf.Abs(move) > 0.1f || Mathf.Abs(turn) > 0.1f;
        animator.SetBool("isWalking", isWalking);
        Debug.Log("isWalking = " + isWalking);
    }
}
