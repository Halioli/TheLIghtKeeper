using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gecko : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    [SerializeField] EnemyAudio enemyAudio;

    private int routeToGo;

    private float tParam;
    private Vector2 geckoPos;
    private float speeddModifier;
    private bool coroutineAllowed;

    private Animator geckoAnimator;
    private HealthSystem geckoHealthSystem;

    public bool playerIsNear;
    public bool touched;
    public bool reachedEnd;
    private bool isGetsTouchedStarted;

    public bool geckoDead;

    private void Awake()
    {
        SaveSystem.geckos.Add(this);
    }
    void Start()
    {
        coroutineAllowed = true;
        touched = false;
        reachedEnd = false;
        isGetsTouchedStarted = false;
        geckoAnimator = GetComponent<Animator>();
        routeToGo = 0;
        tParam = 0f;
        speeddModifier = 0.5f;
        transform.position = routes[0].GetChild(0).position;
        geckoHealthSystem = GetComponent<HealthSystem>();
    }


    void Update()
    {
        if (coroutineAllowed && touched && !reachedEnd && !geckoHealthSystem.IsDead())
        {
            geckoAnimator.SetBool("running", true);
            StartCoroutine(GoByTheRoute(routeToGo));
        }

    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;
        touched = false;

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        enemyAudio.PlayFootstepsAudio();

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speeddModifier;

            geckoPos = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) *tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = geckoPos;
            
            yield return new WaitForEndOfFrame();
        }
        geckoAnimator.SetBool("running", false);
        tParam = 0f;
        routeToGo += 1;
        if (routeToGo > routes.Length - 1)
        {
            reachedEnd = true;
            routeToGo = 0;
        }

        coroutineAllowed = true;

        enemyAudio.StopFootstepsAudio();
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        GetsTouched();
    //    }
    //}


    public void GetsTouched()
    {
        if (coroutineAllowed && !isGetsTouchedStarted)
        {
            isGetsTouchedStarted = true;
            StartCoroutine(StartGetsTouched());
        }
    }


    IEnumerator StartGetsTouched()
    {
        yield return new WaitForSeconds(0.5f);

        isGetsTouchedStarted = false;
        touched = true;
    }


    public void DeactivateSelf()
    {
        gameObject.SetActive(false);
    }

}
