using UnityEngine;

public class Sequence5 : Sequence
{
    protected override void ValidateSequence()
    {
        base.ValidateSequence();
        FindObjectOfType<WebSiteManager>().ActivateWebSiteTab(3);
    }
}
