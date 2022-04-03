using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    GameManager gameManager => GameManager.Instance;
    BossManager bossManager => BossManager.Instance;

    [Header("HP")]
    [SerializeField] Image hpImg;
    [SerializeField] Text hpTxt;

    [Header("Pain")]
    [SerializeField] Image painImg;
    [SerializeField] Text painTxt;

    [Header("Boss")]
    [SerializeField] Image bossHpImg;
    [SerializeField] Text bossHpTxt;

    [Header("Background")]
    [SerializeField] SpriteRenderer background;
    [SerializeField] Sprite[] backes;

    // Start is called before the first frame update
    void Start()
    {
        bossHPGaugeController(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckGauge();
        CheckBossGauge();
    }

    void CheckGauge()
    {
        hpImg.fillAmount = gameManager.HP / GameManager.MaxHP;
        painImg.fillAmount = gameManager.Pain / GameManager.MaxPain;
        hpTxt.text = ((int)gameManager.HP).ToString();
        painTxt.text = ((int)gameManager.Pain).ToString();
    }

    void CheckBossGauge()
    {
        if (!bossManager.isBossTime)
            return;

        bossHPGaugeController(true);

        if (bossManager.boss != null)
        {
            Boss boss = bossManager.boss.GetComponent<Boss>();
            bossHpImg.fillAmount = (float)boss.HP / boss.MaxHP;
            bossHpTxt.text = boss.HP.ToString() + "/" + boss.MaxHP.ToString();
        }
        else
        {
            CheckMiniBossGauge();
        }
        
    }

    public float MiniMaxHP;
    void CheckMiniBossGauge()
    {
        if (bossManager.mini1 != null && bossManager.mini2 != null)
        {
            MiniBoss mini1 = bossManager.mini1.GetComponent<MiniBoss>();
            MiniBoss mini2 = bossManager.mini2.GetComponent<MiniBoss>();

            bossHpImg.fillAmount = ((float)mini1.HP + (float)mini2.HP) / MiniMaxHP;
            bossHpTxt.text = (mini1.HP + mini2.HP).ToString() + "/" + MiniMaxHP.ToString();

        }
        else if(bossManager.mini1 == null && bossManager.mini2 != null)
        {

            MiniBoss mini2 = bossManager.mini2.GetComponent<MiniBoss>();
            bossHpImg.fillAmount = ((float)mini2.HP) / MiniMaxHP;
            bossHpTxt.text = (mini2.HP).ToString() + "/" + MiniMaxHP.ToString();


        }
        else if(bossManager.mini1 != null && bossManager.mini2 == null)
        {

            MiniBoss mini1 = bossManager.mini1.GetComponent<MiniBoss>();
            bossHpImg.fillAmount = ((float)mini1.HP) / MiniMaxHP;
            bossHpTxt.text = (mini1.HP).ToString() + "/" + MiniMaxHP.ToString();

        }
    }

    public void bossHPGaugeController(bool isShow)
    {
        bossHpImg.enabled = isShow;
        bossHpTxt.enabled = isShow;
    }

    public void ChangeBackground(int stage)
    {
        background.sprite = backes[stage - 1];
    }
}
