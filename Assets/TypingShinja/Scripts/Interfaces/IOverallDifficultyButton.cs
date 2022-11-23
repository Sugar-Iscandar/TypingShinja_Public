using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IOverallDifficultyButton
{
    public UnityAction<OverallDifficulty> OnButtonClicked { get; set; }

    public void DetermineWhetherToEnableButton(OverallDifficulty overallDif);
}
