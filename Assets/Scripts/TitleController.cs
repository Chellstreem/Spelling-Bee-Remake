using TMPro;

public class TitleController
{    
    private readonly IScaler scaler;
    private readonly TextMeshProUGUI title;    
    private readonly float sceneDuration;
    
    private float GrowthDuration => sceneDuration / 2f;

    public TitleController(IScaler scaler, TextMeshProUGUI title, int finalFontSize, float sceneDuration)
    {        
        this.scaler = scaler;
        this.title = title;        
        this.sceneDuration = sceneDuration;

        this.scaler.ActivateWithScale(title.transform, GrowthDuration, 0, DG.Tweening.Ease.InElastic);
    }    
}
