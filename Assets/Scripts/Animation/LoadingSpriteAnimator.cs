using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

// Will Min

/*
 * Aniamtor for Loading Sprite
 */
public class LoadingSpriteAnimator : MonoBehaviour
{
    private float m_Timer;

    // The amount of seconds per loop
    [SerializeField]
    private float m_TimeLoop = .5f;

    // The scale that the loading sprite starts as
    [SerializeField]
    private Vector3 m_InitialScale;

    // The maximum change of scale of the loading sprite
    [SerializeField]
    private Vector3 m_MaxScaleChange;

    // The TextMeshPro object the loading sprite will use
    [SerializeField]
    private TMP_Text m_LoadingText;
    
    private void FixedUpdate()
    {
        AnimateUI();
    }

    private void AnimateUI()
    {
        m_Timer -= Time.deltaTime;

        if (m_Timer > m_TimeLoop * 2f / 3f)
            m_LoadingText.text = ChangeText(1);
        else if (m_Timer > m_TimeLoop / 3f)
            m_LoadingText.text = ChangeText(2);
        else if (m_Timer > 0)
            m_LoadingText.text = ChangeText(3);

        ChangeScaling(-2f / m_TimeLoop * Mathf.Abs(m_Timer - m_TimeLoop / 2f) + 1f);

        if (m_Timer <= 0)
            m_Timer = m_TimeLoop;
    }

    private void ChangeScaling(float scale)
    {
        transform.localScale = m_InitialScale + scale * m_MaxScaleChange;
    }

    private string ChangeText(int numberOfDots)
    {
        string result = "Loading";

        for (int i = 0; i < numberOfDots; i++)
            result += '.';

        return result;
    }
}
