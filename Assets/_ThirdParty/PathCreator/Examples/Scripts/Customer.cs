using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public float speed = 2f;

    public Material opaqueMat = null;
    public Material transparentMat = null;

    public Renderer renderer = null;

    public Animator animator = null;

    public ParticleSystem positiveParticle=null;
    public ParticleSystem negativeParticle=null;

    private Transform targetToMoveTowards = null;

    private Vector3 moveVector;

    private float curTargetDistance = 10f;
    private float prevTargetDistance = 10f;

    private CustomerManager customerManager = null;

    public Transform TargetToMoveTowards { get => targetToMoveTowards; set => targetToMoveTowards = value; }

    // Start is called before the first frame update
    void Start()
    {

        animator.SetFloat("IdleOffset",Random.Range(0f,1f));
        //Breathing Idle
    }

    private void Update()
    {
        if (targetToMoveTowards != null)
        {
            curTargetDistance = Vector3.Distance(transform.position, targetToMoveTowards.position);
            //if ((curTargetDistance < 0.01f) || (curTargetDistance > prevTargetDistance))   
            if ((curTargetDistance < 0.01f))
            {
                targetToMoveTowards = null;
                customerManager.ReachedTarget(this);
                TriggerIdleWRandomOffset();

                // transform.position = targetPos;
                return;
            }
            prevTargetDistance = curTargetDistance;

            transform.position = transform.position + moveVector.normalized * speed * Time.deltaTime;
            //   Vector3 moveVector=
            //transform.position
        }
    }

    private void TriggerIdleWRandomOffset()
    {
        animator.SetTrigger("Idle");
        //float randomIdleStart = Random.Range(0, animator.GetCurrentAnimatorStateInfo(0).length);
       // animator.Play("BreathingIdle",0,randomIdleStart);
    }

    public void Move(Transform target, CustomerManager manager)
    {
        targetToMoveTowards = target;
        customerManager = manager;

        prevTargetDistance = curTargetDistance = 10f;

        if (target != null)
        {
            moveVector = target.position - transform.position;
            transform.LookAt(target);
            animator.SetTrigger("Walk");
        }
        else
        {
            TriggerIdleWRandomOffset();
        }
    }

    public void MoveAndDie(Transform target, CustomerManager manager, float delayForFadeOut, float fadeOutDuration)
    {
        speed *= 2;
        Move(target, manager);

        FadeOut(delayForFadeOut, fadeOutDuration);
    }

    public void FadeOut(float duration, float startDelay = 0f)
    {
        StartCoroutine(FadeOutCoroutine(duration, startDelay));
    }
    private IEnumerator FadeOutCoroutine(float duration, float startDelay)
    {
        if (startDelay > 0f)
        {
            yield return new WaitForSeconds(startDelay);
        }

        renderer.material = transparentMat;

        Color c = renderer.material.color;
        renderer.material.color = new Color(c.r, c.g, c.b, 1f);

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            renderer.material.color = new Color(c.r, c.g, c.b, 1 - timer / duration);
            yield return null;
        }

        renderer.material.color = new Color(c.r, c.g, c.b, 0f);
    }


    public void FadeIn(float duration)
    {
        renderer.material = transparentMat;

        Color c = renderer.material.color;
        renderer.material.color = new Color(c.r, c.g, c.b, 0f);
        StartCoroutine(FadeInCoroutine(duration, c));
    }

    private IEnumerator FadeInCoroutine(float duration, Color defaultColor)
    {
        float timer = 0f;
        Color c = defaultColor;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            renderer.material.color = new Color(c.r, c.g, c.b, timer / duration);
            yield return null;
        }

        renderer.material.color = new Color(c.r, c.g, c.b, 1f);
        renderer.material = opaqueMat;
    }

    public void PlayPosOrNegParticle(bool isPositive){
        if(isPositive){
            positiveParticle.Play();
        }else{
            negativeParticle.Play();
        }
    }
}
