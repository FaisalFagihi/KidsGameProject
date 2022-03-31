using UnityEngine;

public class Level : MonoBehaviour
{
    public Sprite goalSprite;
    public string question;
    public string option1;
    public string option2;
    public string option3;
    [Range(1,3)]
    public int answer;
    public bool hasSpriteOptions;
    public Sprite option1Sprite;
    public Sprite option2Sprite;
    public Sprite option3Sprite;
}
