using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentFinishAndClearCriteria
{
    static CurrentFinishAndClearCriteria instance = new CurrentFinishAndClearCriteria();

    public static CurrentFinishAndClearCriteria Instance
    {
        get => instance;
    }

    private CurrentFinishAndClearCriteria() { }

    FinishAndClearCriteria criteria;

    public FinishAndClearCriteria Criteria
    {
        get => criteria;
        set => criteria = value;
    }
}
