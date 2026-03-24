using TMPro;

public class TitleController
{
    private readonly ScaleEffect scaler;
    private readonly TextMeshProUGUI title;
    private readonly float sceneDuration;

    private float GrowthDuration => sceneDuration / 2f;

    public TitleController(ScaleEffect scaler, TextMeshProUGUI title, int finalFontSize, float sceneDuration)
    {
        this.scaler = scaler;
        this.title = title;
        this.sceneDuration = sceneDuration;

        this.scaler.ActivateWithScale(title.transform, GrowthDuration, 0, DG.Tweening.Ease.InElastic);
    }
}
