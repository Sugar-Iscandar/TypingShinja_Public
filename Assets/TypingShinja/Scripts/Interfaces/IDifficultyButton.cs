using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDifficultyButton
{
    public UnityAction<Difficulty> OnButtonClicked { get; set; }

    public void DetermineWhetherToEnableButton(Difficulty dif);
}
