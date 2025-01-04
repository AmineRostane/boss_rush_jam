using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSelection : MonoBehaviour
{
    [SerializeField] int bossIndex;
    private static float[] probabilities = { 1f / 3f, 1f / 3f, 1f / 3f }; // Initial equal probabilities
    [SerializeField] private float reductionFactor = 0.5f;

    private static int selectionCount = 0; // Tracks how many times a boss has been selected
    private const int resetThreshold = 3;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //Debug.Log($"1st probas: Probabilities: Boss1={probabilities[0]:F2}, Boss2={probabilities[1]:F2}, Boss3={probabilities[2]:F2}");
    }

    public void SelectBoss()
    {
       
        if (selectionCount >= resetThreshold)
        {
            ResetProbabilities();
            Debug.Log($"1st probas: Probabilities: Boss1={probabilities[0]:F2}, Boss2={probabilities[1]:F2}, Boss3={probabilities[2]:F2}");
        }

        float totalProbability = 0f;
        foreach (var prob in probabilities)
        {
            totalProbability += prob;
            
        }

        float randomValue = Random.value * totalProbability; // Scale random value by total probability
        float cumulativeProbability = 0f;

        // Select boss based on weighted probabilities
        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                bossIndex = i + 1; // Boss index starts from 1
                AdjustProbabilities(i);
                break;
            }
        }
       

    }

    private void AdjustProbabilities(int selectedBossIndex)
    {
        // Reduce the probability of the selected boss
        probabilities[selectedBossIndex] *= reductionFactor;

        // Normalize probabilities to maintain their sum to 1
        float total = 0f;
        
        foreach (var prob in probabilities)
        {
            total += prob;
        }


        for (int i = 0; i < probabilities.Length; i++)
        {
            probabilities[i] /= total;
        }

        selectionCount++;
        Debug.Log("selection count " + selectionCount);
        Debug.Log($"Probabilities: Boss1={probabilities[0]:F2}, Boss2={probabilities[1]:F2}, Boss3={probabilities[2]:F2}");

    }

    private void ResetProbabilities()
    {
        probabilities = new float[] { 1f / 3f, 1f / 3f, 1f / 3f }; // Reset to equal probabilities
        selectionCount = 0; // Reset selection count
        Debug.Log("Probabilities Reset");
    }

    public int GetBossIndex()
    {
        return bossIndex;
    }

    
}
