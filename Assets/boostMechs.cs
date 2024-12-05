using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boostMechs : MonoBehaviour
{
    public Slider boostBar;

    private int maxBoost = 100;
    private int curBoost;
    private WaitForSeconds RegenBoostTick = new WaitForSeconds(0.1f);
    private Coroutine RegenBoostRoutine;

    public static boostMechs instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        boostBar.maxValue = maxBoost;
        boostBar.value = maxBoost;
        curBoost = maxBoost;
    }

    public void useBoost(int amount)
    {
        if(curBoost - amount > 0)
        {
            curBoost -= amount;
            boostBar.value = curBoost;

            if(RegenBoostRoutine != null)
            {
                StopCoroutine(RegenBoostRoutine);
            }
            RegenBoostRoutine = StartCoroutine(RegenBoost());
        }
    }

    public bool enoughBoost(int amount)
    {
        if (curBoost - amount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator RegenBoost()
    {
        yield return new WaitForSeconds(0.5f);

        while(curBoost < maxBoost)
        {
            curBoost += maxBoost / 100;
            boostBar.value = curBoost;
            yield return RegenBoostTick;
        }
        RegenBoostRoutine = null;
    }
}
