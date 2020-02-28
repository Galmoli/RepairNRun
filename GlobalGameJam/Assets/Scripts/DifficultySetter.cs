using UnityEngine;

public class DifficultySetter : MonoBehaviour
{
    [Header("Easy")]
    [SerializeField] private float e_FastTorque;
    [SerializeField] private float e_DefaultTorque;
    [SerializeField] private float e_SlowTorque;
    
    [Header("Medium")]
    [SerializeField] private float m_FastTorque;
    [SerializeField] private float m_DefaultTorque;
    [SerializeField] private float m_SlowTorque;
    
    [Header("Difficult")]
    [SerializeField] private float d_FastTorque;
    [SerializeField] private float d_DefaultTorque;
    [SerializeField] private float d_SlowTorque;

    private void Awake()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        SetVelocities(PlayerPrefs.GetInt("Difficulty"), enemies);
    }

    private void SetVelocities(int diff, GameObject[] enemies)
    {
        switch (diff)
        {
            case 0:
                foreach (var e in enemies)
                {
                    var ai = e.GetComponent<CarAI>();
                    ai.fastTorque = e_FastTorque;
                    ai.defaultTorque = e_DefaultTorque;
                    ai.slowTorque = e_SlowTorque;
                }
                break;
            case 1:
                foreach (var e in enemies)
                {
                    var ai = e.GetComponent<CarAI>();
                    ai.fastTorque = m_FastTorque;
                    ai.defaultTorque = m_DefaultTorque;
                    ai.slowTorque = m_SlowTorque;
                }
                break;
            case 2:
                foreach (var e in enemies)
                {
                    var ai = e.GetComponent<CarAI>();
                    ai.fastTorque = d_FastTorque;
                    ai.defaultTorque = d_DefaultTorque;
                    ai.slowTorque = d_SlowTorque;
                }
                break;
        }
    }
}
