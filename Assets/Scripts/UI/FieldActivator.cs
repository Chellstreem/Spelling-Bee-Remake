using UnityEngine;
using Zenject;

public class FieldActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] fields;
    
    private IScaler scaler;
    private int initiallyActiveFields;
    private int activeFieldIndex;

    public int ActiveFieldIndex => activeFieldIndex;
    public int FieldsLength => fields.Length;

    [Inject]
    public void Construct(IScaler scaler, int initiallyActiveFields)
    {
        this.scaler = scaler;
        this.initiallyActiveFields = initiallyActiveFields;
    }        

    private void Start()
    {
        for (int i = 0; i < fields.Length; i++)
        {
            fields[i].SetActive(i < initiallyActiveFields);
        }

        activeFieldIndex = initiallyActiveFields - 1;
    }

    public void ActivateNextField()
    {
        if (activeFieldIndex < fields.Length - 1)
        {
            activeFieldIndex++;
            scaler.ActivateWithScale(fields[activeFieldIndex].transform, 0.4f, 0f);            
        }
    }

    public void DeactivateLastField()
    {
        if (activeFieldIndex > 0)
        {
            scaler.DeactivateWithScale(fields[activeFieldIndex].transform, 0.3f, 0f);
            activeFieldIndex--;            
        }
    }    
}
