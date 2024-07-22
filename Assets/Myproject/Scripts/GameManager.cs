using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreNumbers
{
    public int[] nums;
}
public class GameManager : MonoBehaviour
{
    public static GameManager current;

    AudioSource audioSource;
    [SerializeField]
    AudioClip[] audioClips;

    [SerializeField]
    GameObject[] backgroundParentPrefabs;
    GameObject backgroundParent;
    [SerializeField]
    GameObject[] subBackgroundParentPrefabs;
    GameObject subBackgroundParent;
    [SerializeField]
    GameObject[] waveManagerPrefabs;
    GameObject waveManager;
    [SerializeField]
    ScoreNumbers[] _scoreNumbers;
    public ScoreNumbers[] scoreNumbers {  get { return _scoreNumbers; } }
    
    public int stage { get; set; } = 1;
    public int difficulty { get; set; }
    [SerializeField]
    UnityEngine.UI.Text stageText;
    [SerializeField]
    UnityEngine.UI.Text scoreText;
    public int score { get; set; }

    [SerializeField]
    UnityEngine.UI.Image fadeInOutImage;
    [SerializeField]
    Transform _bossHpbarTrans;
    public Transform bossHpbarTrans { get {  return _bossHpbarTrans; } }
    [SerializeField] 
    EndScreen endScreen;
    [SerializeField]
    GameObject pauseScreen;
    [SerializeField]
    GameObject expScreen;
    [SerializeField]
    GameObject godModScreen;
    public void Died()
    {
        endScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
        endScreen.Died();
    }
    public void Clear()
    {
        endScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
        endScreen.Clear();
    }
    
    //int Bestscore = 0;
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
        audioSource.Play();
        backgroundParent = Instantiate(backgroundParentPrefabs[stage - 1]);
        subBackgroundParent = Instantiate(subBackgroundParentPrefabs[stage - 1]);
        waveManager = Instantiate(waveManagerPrefabs[stage - 1]);
        fadeInOutImage.gameObject.SetActive(false);
        pauseScreen.SetActive(false);
        expScreen.SetActive(false);
        endScreen.gameObject.SetActive(false);
        godModScreen.SetActive(false);
    }
    private void Update()
    {
        if (pauseScreen.activeSelf)
        {
            Time.timeScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            InventoryUI.current.InventorySwitch();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Player.current.godMod = !Player.current.godMod;
            godModScreen.gameObject.SetActive(Player.current.godMod);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Player.current.LevelUp();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Player.current.LevelDown();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Player.current.Heal(Player.current.maxHp);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            GameObject obj = DropManager.current.InstanWeaponDrop();
            obj.transform.parent = Camera.main.transform;
            obj.transform.position = Camera.main.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            GameObject obj = DropManager.current.IstanNormalDrop();
            obj.transform.parent = Camera.main.transform;
            obj.transform.position = Camera.main.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            AddScore(50);
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            expScreen.SetActive(!expScreen.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseScreen.activeSelf == false)    
            {
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
            }
            else
            {
                pauseScreen.SetActive(false);
                Time.timeScale = 1; 
            }
        }
    }

    public void AddScore(int point)
    {
        score += point;
        scoreText.text = $"점수: {score}";
        if (stage > _scoreNumbers.Length)
        {
            return;
        }
        if (score >= _scoreNumbers[stage - 1].nums[difficulty])
        {
            difficulty++;
            if (difficulty >= _scoreNumbers[stage - 1].nums.Length)
            {
                difficulty = 0;
                stage++;
                stageText.text = $"현재 스테이지: {stage}";
                StartCoroutine(ChangeStage());
            }
        }
        //Bullet, Monster, DropItem, Ingredient, WaveScript
    }
    public void BossArive()
    {
        subBackgroundParent.GetComponent<ShaderBackground>().enabled = false;
    }
    IEnumerator ChangeStage()
    {
        Time.timeScale = 0;
        float timer = 0;
        fadeInOutImage.gameObject.SetActive(true);
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(1);
        while (timer <= 0.5f)
        {
            fadeInOutImage.color = new Color(0f, 0f, 0f, timer * 2);
            audioSource.volume = (0.5f - timer) / 2;
            timer += Time.unscaledDeltaTime;
            yield return waitForEndOfFrame;
        }
        fadeInOutImage.color = new Color(0f, 0f, 0f, 1f);
        if (backgroundParent != null) 
        {
            backgroundParent.GetComponent<BackgroundParent>().DestroyBackground();
            Destroy(backgroundParent);
            backgroundParent = null;
        }
        if (subBackgroundParent != null)
        {
            Destroy(subBackgroundParent);
            subBackgroundParent = null;
        }
        if (waveManager != null) 
        {
            Destroy(waveManager);
            waveManager = null;
        }
        Bullet[] bullets = FindObjectsOfType<Bullet>(true);
        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i].gameObject);
        }
        DropItem[] dropItems = FindObjectsOfType<DropItem>(true);
        for (int i = 0; i < dropItems.Length; i++)
        {
            if (dropItems[i].gameObject.activeSelf == false)
            {
                Destroy(dropItems[i].gameObject);
            }
        }
        IngredientObj[] ingredientObjs = FindObjectsOfType<IngredientObj>(true);
        for (int i = 0; i < ingredientObjs.Length; i++)
        {
            Destroy(ingredientObjs[i].gameObject);
        }
        WaveScript[] waveScripts = FindObjectsOfType<WaveScript>(true);
        for (int i = 0; i < waveScripts.Length; i++)
        {
            Destroy(waveScripts[i].gameObject);
        }
        Monster[] monsters = FindObjectsOfType<Monster>(true);
        for (int i = 0; i < monsters.Length; i++)
        {
            Destroy(monsters[i].gameObject);
        }
        DisableTimer[] disableTimers = FindObjectsOfType<DisableTimer>(true);
        for (int i = 0; i < disableTimers.Length; i++)
        {
            Destroy(disableTimers[i].gameObject);
        }
        Camera.main.transform.position = Vector3.zero;
        
        backgroundParent = Instantiate(backgroundParentPrefabs[stage - 1]);
        subBackgroundParent = Instantiate(subBackgroundParentPrefabs[stage - 1]);
        if (stage <= waveManagerPrefabs.Length)
        {
            waveManager = Instantiate(waveManagerPrefabs[stage - 1]);
        }
        audioSource.clip = audioClips[stage - 1];
        audioSource.Play();
        yield return waitForSeconds;
        while (timer >= 0)
        {
            fadeInOutImage.color = new Color(0f, 0f, 0f, timer * 2);
            audioSource.volume = (0.5f - timer) / 2;
            timer -= Time.unscaledDeltaTime;
            yield return waitForEndOfFrame;
        }
        fadeInOutImage.color = new Color(0f, 0f, 0f, 0f);
        fadeInOutImage.gameObject.SetActive(false);
        Time.timeScale = 1;
        
    }
}
