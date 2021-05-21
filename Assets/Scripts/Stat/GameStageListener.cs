
public interface GameStageListener
{
    void OnTimerTick(int minutes, int seconds);

    void OnLevelChanged(string level);
}
