using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSuppression : MonoBehaviour
{
    [SerializeField] float maxSuppression;
    [SerializeField] float suppression;
    [SerializeField] float suppressionDrain;
    [SerializeField] float recoverDelay;

    private float lastSuppressed;

    private Coroutine recoverCourutine;

    // Start is called before the first frame update
    void Start()
    {
        suppression = 0;
    }

    private void Update()
    {
        if (suppression > 0)
        {
            float timeSinceLastSuppression = Time.time - lastSuppressed;
            if (recoverCourutine == null && timeSinceLastSuppression >= recoverDelay)
            {
                recoverCourutine = StartCoroutine(recover());
            }
        }    
    }

    public void takeSuppression(float suppressionPoints)
    {
        suppression = Mathf.Min(suppression + suppressionPoints, maxSuppression);
        lastSuppressed = Time.time;
        StopCoroutine(recoverCourutine);
    }

    private IEnumerator recover()
    {
        while (suppression > 0)
        {
            suppression = Mathf.Max(suppression - suppressionDrain, 0);
            yield return new WaitForSeconds(1);
        }   
    }
}
