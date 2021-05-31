using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class DamageText : MonoBehaviour
{
    [SerializeField]
    private TextMesh txtDamageTaken;

    [SerializeField]
    private float upVelocity = 1f;

    [Range(0.1f, 2f)]
    [SerializeField]
    private float xOffSetRange = 1f;

    [Range(0.1f, 2f)]
    [SerializeField]
    private float yOffSetRange = 1f;

    private void Awake()
    {
        GetTextMeshReferenceIfNotSet();
    }

    private void OnEnable()
    {
        SetOffSetPosition();
    }

    private void OnDisable()
    {
        ResetText();
    }

    private void Update()
    {
        MoveTextUp();
    }

    void GetTextMeshReferenceIfNotSet() 
    {
        if (!txtDamageTaken)
            txtDamageTaken = GetComponent<TextMesh>();
    }

    public void SetDamageText(string damageTaken, Color colorToSetText)
    {
        txtDamageTaken.color = colorToSetText;
        txtDamageTaken.text = damageTaken;
    }

    void SetOffSetPosition()
    {
        transform.localPosition += OffSetPositionRange();
    }
    void MoveTextUp()
    {
        float moveDirection = upVelocity * Time.deltaTime;

        transform.Translate(Vector2.up * moveDirection);
    }

    void ResetText() 
    {
        txtDamageTaken.text = string.Empty;
    }

    float ValueInRange(float rangeTarget)
    {
        return Random.Range(-rangeTarget, rangeTarget);
    }

    Vector3 OffSetPositionRange() 
    {
        return new Vector3(ValueInRange(xOffSetRange), ValueInRange(yOffSetRange), 0);
    }
}