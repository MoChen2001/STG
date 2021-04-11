using System;
using UnityEngine;
using UnityEngine.UI;



public class ShipControl : MonoBehaviour
{
    // 飞船属性
    private int currentLife;
    private int maxLife;
    private int moveSpeed;
    private float moveSpeedInWindos;
    private GameObject judgePoint;
    private bool whetherDisapperJudgePoint;
    private Slider lifeSlider;
    private Text lifeText;



    // 通用技能相关变量
    private int skillCD;
    private Image skillCDMask;
    private Button button_Skill;
    private Text skillCD_Text;
    private float fillAmount;
    private Image skillImage;
    private bool useSkilling;


    // 射击相关变量
    // 两个 bool 类型的值，都是和暂停相关的，
    // 第一个是保证只结束一次射击，
    // 第二个是保证不结束默认射击
    private bool stopShot;
    private bool shotDefault;
    private BulletShotHelper m_Shot;
    private GameObject bullets;
    private GameObject bullet;
    private int shotIndex;
    private int shotNumber;
    private int shotSpeed;
    private float shotRange;
    private float waitSeconds;
    private float eulerAngle;




    private string index;

    private void Start()
    {
        InitObjectValue();
        InitShot();
        InitSkillUI();
        if (PlayerPrefs.GetInt("NotDie",0) == 1)
        {
            NotDieStateStart();
        }
    }



    private void Update()
    {
        ShipSkill();
        ShipShotEndWhenParse();
        UpdateSpeed();
    }



    private void InitObjectValue()
    {
        maxLife = GameData.Instance.Life;
        currentLife = maxLife;
        SetDefaultSpeed();
        judgePoint = GameObject.Find("JudgePoint");
        whetherDisapperJudgePoint = false;
        lifeSlider = GameObject.Find("Slider").GetComponent<Slider>();
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        UpdateLife();
    }


    //-----------------------------------技能相关函数-----------------------------------------------

    // 初始化技能UI的函数
    private void InitSkillUI()
    {
        skillCD = Convert.ToInt32(JsonPlayerData.Instance.GetDataSkillCD());
        skillCDMask = GameObject.Find("SkillCDMask").GetComponent<Image>();
        button_Skill = GameObject.Find("Skill").GetComponent<Button>();
        skillImage = GameObject.Find("Skill").GetComponent<Image>();
        skillCD_Text = GameObject.Find("SkillCDText").GetComponent<Text>();



        button_Skill.onClick.AddListener(UseSkill);
        skillImage.sprite = Resources.Load<GameObject>("UI/Skill_Ship_" + index).GetComponent<Image>().sprite;


        fillAmount = skillCD;
        useSkilling = false;
        skillCD_Text.enabled = false;
        skillCDMask.enabled = false;
    }


    /// 使用技能的函数
    private void ShipSkill()
    {
        if (useSkilling)
        {
            button_Skill.enabled = false;
            skillCDMask.enabled = true;
            skillCD_Text.enabled = true;
            skillCDMask.fillAmount = fillAmount / skillCD;
            fillAmount -= Time.deltaTime;
            float text = skillCDMask.fillAmount * skillCD;
            SkillHelper.Instance.UseSkill(gameObject);
            skillCD_Text.text = string.Format("{0:f2}", text);
            if (fillAmount <= 0)
            {
                useSkilling = false;
                fillAmount = skillCD;
                skillCDMask.enabled = false;
                skillCD_Text.enabled = false;
                button_Skill.enabled = true;

            }
        }
    }


    // 点击按钮时使用技能
    public void UseSkill()
    {
        useSkilling = true;
    }




    //-----------------------------------工具函数-----------------------------------------------


    private void UpdateLife()
    {
        lifeText.text = currentLife.ToString();
        lifeSlider.value = (float)currentLife / maxLife;
    }


    // 生命值减少
    public void MinusLife()
    {
        currentLife--;
        UpdateLife();
        if (currentLife == 0)
        {
            m_Shot.StopAllCoroutines();
            UIStateController.Instance.GameOverState();
        }
    }


    // 留一个接口用来增加可发射的子弹数
    public void AddShotPoint()
    {
        if(shotNumber < GameData.Instance.MaxShotNumber)
        {
            shotNumber++;
        }

        if(shotRange < GameData.Instance.MaxRange)
        {
            shotRange += 2.5f;
        }
        if(eulerAngle < GameData.Instance.MaxAngle)
        {
            eulerAngle += 10;
        }
        if (shotDefault)
        {
            StopDefaultShot();
            DefaultShot();
        }
    }


    private void UpdateSpeed()
    {
        if(UIStateController.Instance.controlDirty)
        {
            SetDefaultSpeed();
        }
    }

    private void SetDefaultSpeed()
    {
        moveSpeed = 5;
        moveSpeedInWindos = 1.0f;
    }


    public float GetSpeedInWindows()
    {
        return moveSpeedInWindos;
    }

    // 返回飞船的移动速度
    public int GetSpeed()
    {
        return moveSpeed;
    }


    // 开启无敌模式
    public void NotDieStateStart()
    {
        whetherDisapperJudgePoint = true;
        judgePoint.SetActive(false);
    }

    // 关闭无敌模式
    public void NotDieStateEnd()
    {
        whetherDisapperJudgePoint = false;
        judgePoint.SetActive(true);
    }

    // 判定点的显示与隐藏
    private void SetJudgePointMesh(bool flag)
    {
        if(!whetherDisapperJudgePoint)
        {
            judgePoint.GetComponent<MeshRenderer>().enabled = flag;
        }
    }




    //-----------------------------------射击相关函数-----------------------------------------------

    // 初始化射击相关变量
    private void InitShot()
    {
        index = JsonPlayerData.Instance.GetDataIndex();
        m_Shot = gameObject.GetComponent<BulletShotHelper>();
        bullets = GameObject.Find("BulletParent");
        bullet = Resources.Load<GameObject>("Bullet/Bullet_Player_" + index);
        shotNumber = 1;
        shotSpeed = 500;
        shotRange = 10f;
        waitSeconds = 0.1f;
        eulerAngle = 0;
        stopShot = false;
        shotDefault = true;
        DefaultShot();
    }

    // 默认进行欧拉角射击
    private void DefaultShot()
    {
        shotDefault = true;
        waitSeconds = 0.1f;
        SetJudgePointMesh(false);
        shotIndex = m_Shot.StartEuler(bullet, bullets, shotNumber, eulerAngle, shotSpeed, waitSeconds);
    }

    // 停止默认射击
    private void StopDefaultShot()
    {
        shotDefault = false;
        m_Shot.StopCoroutineWithIndex(shotIndex);
    }

    // 开始射击
    public void ShipShotStart()
    {
        StopDefaultShot();
        waitSeconds = 0.03f;
        moveSpeedInWindos /= 2;
        moveSpeed /= 2;
        SetJudgePointMesh(true);
        if (shotNumber == 1)
        {
            shotIndex = m_Shot.StartMoreLine(bullet, bullets,  shotNumber, 0, shotSpeed, 0.03f);
        }
        else
        {
            shotIndex = m_Shot.StartMoreLine(bullet, bullets,  shotNumber, shotRange, shotSpeed, 0.03f);
        }
    }


    // 停止射击
    public void ShipShotEnd()
    {
        m_Shot.StopCoroutineWithIndex(shotIndex);
        SetDefaultSpeed();
        DefaultShot();
    }

    // 暂停时停止射击
    private void ShipShotEndWhenParse()
    {
        if (UIStateController.Instance.gameParse && !stopShot)
        {
            stopShot = true;
            if(!shotDefault)
            {
                m_Shot.StopCoroutineWithIndex(shotIndex);
            }
        }
        else if (!UIStateController.Instance.gameParse)
        {
            stopShot = false;
        }
    }

}
