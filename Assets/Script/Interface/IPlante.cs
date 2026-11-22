using UnityEngine;

public interface IPlante
{
    public void OnEnable(); // pour s'abboner à l'événement jour/nuit de timeManager
    public void OnDisable(); // pour se désabboner à l'événement jour/nuit de timeManager

    public void MutateToEnemy();

    public void Plant();
}
