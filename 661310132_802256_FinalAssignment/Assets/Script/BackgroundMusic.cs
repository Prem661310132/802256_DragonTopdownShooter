using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip bgMusic1; 
    public AudioClip bgMusic2; 
    public Sprite backgroundPhase1; 
    public Sprite backgroundPhase2;

    private AudioSource audioSource;
    private SpriteRenderer backgroundRenderer;
    private DragonController dragonController;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgMusic1;
        audioSource.loop = true; 
        audioSource.Play();

        backgroundRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        if (backgroundRenderer != null && backgroundPhase1 != null)
        {
            backgroundRenderer.sprite = backgroundPhase1; 
        }

  
        dragonController = FindObjectOfType<DragonController>();
    }

    void Update()
    {
        
        if (dragonController != null && dragonController.isPhase2)
        {
            ChangeToPhase2();
        }
    }

    void ChangeToPhase2()
    {
        
        if (bgMusic2 != null && audioSource.clip != bgMusic2)
        {
            PlayMusic(bgMusic2);
        }

       
        if (backgroundRenderer != null && backgroundPhase2 != null)
        {
            backgroundRenderer.sprite = backgroundPhase2;
        }
    }
    
    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void PlayMusic(AudioClip newMusic)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.clip = newMusic;
        audioSource.Play();
    }
}