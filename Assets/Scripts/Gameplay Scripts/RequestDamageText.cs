using UnityEngine;

public static class RequestDamageText
{
    private const string textDamageID = "damageText";

    public static void RequestText(string valueToSet, Color colorText, Vector3 positionToSet)
    {
        DamageText damageTextToSet = ObjectPooler.Instance.RequestObject(textDamageID, positionToSet).GetComponent<DamageText>();

        if (damageTextToSet == null)
            return;

        damageTextToSet.SetDamageText(valueToSet, colorText);
    } 
}