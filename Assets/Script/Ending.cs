using System.Collections;
using UnityEngine;

public class Ending : MonoBehaviour 
{
    public float speed = 0.1f;
    private float targetX = 25.48f;
    private Vector3 targetPosition;


    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup ending;
    public float fadeDuration = 1f;


    bool isPlayerAtExit;
    float m_Timer;
    bool m_HasAudioPlayed;

    void Start() 
    {
        targetPosition = new Vector3(targetX, this.transform.position.y, this.transform.position.z);
        StartCoroutine(MoveToTarget());
    }

    void Update ()
    {
        if (isPlayerAtExit)
        {
            EndLevel(ending, false);
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart)
    {            
        m_Timer += Time.deltaTime;
        
        ending.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            Application.Quit ();
        }
    }

     IEnumerator MoveToTarget() 
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;

    }

    
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Ending"))
        {
            isPlayerAtExit = true;
            Debug.Log("End");
        }
    }

}
