using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class NavNPC : MonoBehaviour
{
    public NavMeshAgent agent;
    // public Transform targetTrans;
    public Vector3 targetPos;
    private Animator animController;
    private bool isFinding; // 是否开启寻路

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animController = transform.Find("Avatar").GetComponent<Animator>();
    }

    void Update()
    {
        if(isFinding && Time.frameCount%5==0) // 开启寻路后每5帧调用一次
        {
            if((transform.position - targetPos).magnitude<1.0f) // 很接近目标位置时直接消失
            {
                this.gameObject.SetActive(false);
            }           
        }
    }

    /// <summary>
    /// 移动到targetPos，可在Inspector面板设置
    /// </summary>
    public void MoveToTargetPos()
    {
        if(agent!=null) // 安全检验
        {
            isFinding = true;
            animController.Play("jump"); // 播放跳跃动画
            agent.SetDestination(targetPos);
        }
    }
}
