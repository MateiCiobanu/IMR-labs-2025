using UnityEngine;
using TMPro;  

public class StickScoreCounter : MonoBehaviour
{
    public int ringsOnStick = 0;           
    public float stayTime = 1.0f;          
    public TextMeshProUGUI scoreText;     
    private readonly string ringTag = "Ring";

    private void Start()
    {
        UpdateScoreText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ringTag))
        {
            StartCoroutine(CheckIfRingStays(other));
        }
    }

    private System.Collections.IEnumerator CheckIfRingStays(Collider ring)
    {
        float elapsed = 0f;
        Vector3 startPos = ring.transform.position;

        while (elapsed < stayTime)
        {
            yield return null;
            elapsed += Time.deltaTime;
            
            if (Vector3.Distance(startPos, ring.transform.position) > 0.05f)
                yield break;
        }

        ringsOnStick++;
        UpdateScoreText();
        Debug.Log("Ring landed! Total: " + ringsOnStick);
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + ringsOnStick;
    }
}
