using UnityEngine;

public class BuildBGM : MonoBehaviour
{
    [SerializeField] AudioClip _bgm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SoundManager.Instance.PlayBGM(_bgm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        SoundManager.Instance.PlayBGM(_bgm);
    }
    private void OnDisable()
    {
        //SoundManager.Instance.StopBGM();
    }
}
