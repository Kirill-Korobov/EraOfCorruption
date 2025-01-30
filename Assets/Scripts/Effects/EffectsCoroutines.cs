using Cinemachine;
using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectsCoroutines : MonoBehaviour
{
    [SerializeField] private StatEffects statEffects;
    [SerializeField] private MC_HealthManager healthManager;
    [SerializeField] private MC_MovementManager movementManager;
    [SerializeField] private MC_ManaManager manaManager;
    [SerializeField] private MC_StatisticsManager statisticsManager;
    [SerializeField] private MC_SatietyManager satietyManager;
    [SerializeField] private Image[] effectsImage;
    [SerializeField] private TMP_Text[] effectsTexts;
    [SerializeField] private CinemachineVirtualCamera[] mainCamera = new CinemachineVirtualCamera[3];
    [SerializeField] private Camera camera1;

    private Coroutine[] effectsCoroutine = new Coroutine[16];
    private Coroutine[] effectsCoroutineTimer = new Coroutine[16];

    private Action[] resets;
    private Action[] start;
    private IEnumerator[] startCoroutine;
    private float[] blindess = new float[3];
    private int[] timers = new int[16];
    private string path;
    private bool isa = false;
    private bool[] timersgo = new bool[16];
    private int[] n = new int[16];
    private int[] timersCoroutine = new int[16];
    private bool startedGame =false;

    private void OnApplicationQuit()
    {
        Save();
    }
    public void Save()
    {
        TimersSave ts = new TimersSave();
        ts.timers = timers;
        ts.n = n;
        ts.timersCoroutine = timersCoroutine;
        using (var writer = new StreamWriter(path))
        {
            writer.WriteLine(JsonUtility.ToJson(ts));
        }
        if (LoadedSettings.ifAnyOpen || LoadedSettings.ifInventoryOpen || LoadedSettings.ifMapOpen || LoadedSettings.ifQuestsOpen || LoadedSettings.ifStatsOpen)
        {
            gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        camera1.clearFlags = CameraClearFlags.Skybox;
        path = $"{Application.persistentDataPath}/Effects.json";
        resets = new Action[16];
        start = new Action[16];
        startCoroutine = new IEnumerator[16];
        isa = false;

        resets[0] = PoisonStop;
        resets[1] = WeaknessStop;
        resets[2] = SlownessStop;
        resets[3] = HungerStop;
        resets[4] = PartialpenetrationStop;
        resets[5] = BurnStop;
        resets[6] = BlidnessStop;
        resets[7] = CursedStop;
        resets[8] = HexStop;
        resets[9] = RegenerationStop;
        resets[10] = StrengthStop;
        resets[11] = SpeedStop;
        resets[12] = VampirismHPStop;
        resets[13] = VampirismManaStop;
        resets[14] = ResistanceStop;
        resets[15] = ShocksStop;

        startCoroutine[0] = PoisonCoroutine();
        start[1] = WeaknessCoroutine;
        start[2] = SlownessCoroutine;
        startCoroutine[3] = HungerCoroutine();
        start[4] = PartialpenetrationCoroutine;
        startCoroutine[5] = BurnCoroutine();
        start[6] = BlindnessCoroutine;
        start[7] = CursedCoroutine;
        start[8] = HexCoroutine;
        startCoroutine[9] = RegenerationCoroutine();
        start[10] = StrengthCoroutine;
        start[11] = SpeedCoroutine;
        start[12] = VampirisimHPCoroutine;
        start[13] = VampirisimManaCoroutine;
        start[14] = ResistanceCoroutine;
        start[15] = ShocksCoroutine;
        StaticEffects.weaknessRate = 1;
        StaticEffects.strengthRate = 1;
        StaticEffects.hunger = 1;
        StaticEffects.vampirismHPRate = statEffects.VampirismHP;
        StaticEffects.vampirismHP = false;
        StaticEffects.shock = false;
        StaticEffects.coroutines = this;
    }
    public void StartGame()
    {
        string json = "";
        using (var reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null) { json += line; }
        }
        timers = JsonUtility.FromJson<TimersSave>(json).timers;
        n = JsonUtility.FromJson<TimersSave>(json).n;
        timersCoroutine = JsonUtility.FromJson<TimersSave>(json).timersCoroutine;
        for (int i = 0; i < timers.Length; i++)
        {
            if (timers[i] != 0)
            {
                if (start[i] == null)
                {
                    StartEffectCoroutine(startCoroutine[i], i, timers[i]);
                }
                else
                {
                    StartEffectCoroutine(start[i], i, timers[i]);
                }
            }
        }
    }
    private void OnEnable()
    {
        if (startedGame)
        {
            StartGame();
        }
    }
    private void Start()
    {
        startedGame = true;
    }
    public void CheckBlindness()
    {
        if (isa)
        {
            mainCamera[0].m_Lens.FarClipPlane = blindess[0];
            mainCamera[1].m_Lens.FarClipPlane = blindess[1];
            mainCamera[2].m_Lens.FarClipPlane = blindess[2];
            camera1.clearFlags = CameraClearFlags.Skybox;
            RenderSettings.fog = false;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogDensity = 0.5f;
        }
        isa = false;
    }
    public void ResetEffect()
    {
        for (int i = 0; i < timers.Length; i++)
        {
            isa = true;
            resets[i]();
            timers[i] = 0;
            timersgo[i] = false;
            if (effectsCoroutine[i] != null)
            {
                StopCoroutine(effectsCoroutine[i]);
                EffectsPosition.DeleteImageAsync(effectsImage[i]);
            }
            if (effectsCoroutineTimer[i] != null)
            {
                StopCoroutine(effectsCoroutineTimer[i]);
                EffectsPosition.DeleteImageAsync(effectsImage[i]);
            }
            effectsImage[i].gameObject.SetActive(false);

        }

    }
    private IEnumerator ShowATimer(int i, int timer)
    {
        int minutes = timer / 60;
        int seconds = timer % 60;
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (seconds == 0)
            {
                if (minutes == 0)
                {
                    break;
                }
                else
                {
                    minutes--;
                    seconds = 59;
                }
            }
            else
            {
                seconds--;
            }
            if (seconds >= 10)
            {
                effectsTexts[i].text = $"{minutes}:{seconds}";
            }
            else
            {
                effectsTexts[i].text = $"{minutes}:0{seconds}";
            }
            timers[i]--;
        }
        n[i] = 0;
        timersCoroutine[i] = 0;
        if (i == 0 || i == 3 || i == 5 || i == 9)
        {
            StopCoroutine(effectsCoroutine[i]);
        }

        resets[i]();
        EffectsPosition.DeleteImageAsync(effectsImage[i]);
    }

    public void StartEffectCoroutine(IEnumerator coroutineMethod, int i, int timer)
    {
        if (!timersgo[i] || effectsCoroutineTimer[i] == null)
        {
            Debug.Log(1);
            effectsCoroutine[i] = StartCoroutine(coroutineMethod);
            effectsCoroutineTimer[i] = StartCoroutine(ShowATimer(i, timer));
            EffectsPosition.SetImageAsync(effectsImage[i]);
            timers[i] = timer;
            timersgo[i] = true;
            int minutes = timer / 60;
            int seconds = timer % 60;
            effectsTexts[i].text = $"{minutes}:0{seconds}";
            return;
        }

        else
        {
            timersgo[i] = true;
            StopCoroutine(effectsCoroutineTimer[i]);
            effectsCoroutineTimer[i] = StartCoroutine(ShowATimer(i, timer));
            timers[i] = timer;
            int minutes = timer / 60;
            int seconds = timer % 60;
            effectsTexts[i].text = $"{minutes}:0{seconds}";

            return;
        }
    }
    public void StartEffectCoroutine(Action coroutineMethod, int i, int timer)
    {
        Debug.Log(5);
        if (!timersgo[i])
        {
            timersgo[i] = true;
            timers[i] = timer;
            coroutineMethod();
            effectsCoroutineTimer[i] = StartCoroutine(ShowATimer(i, timer));
            EffectsPosition.SetImageAsync(effectsImage[i]);
            int minutes = timer / 60;
            int seconds = timer % 60;
            effectsTexts[i].text = $"{minutes}:0{seconds}";
            return;
        }

        else
        {
            timersgo[i] = true;
            timers[i] = timer;
            StopCoroutine(effectsCoroutineTimer[i]);
            effectsCoroutineTimer[i] = StartCoroutine(ShowATimer(i, timer));
            int minutes = timer / 60;
            int seconds = timer % 60;
            effectsTexts[i].text = $"{minutes}:0{seconds}";
            return;
        }
    }
    public void Poison() => StartEffectCoroutine(PoisonCoroutine(), 0, statEffects.PoisionTime);
    private IEnumerator PoisonCoroutine()
    {


        WaitForSeconds a = new WaitForSeconds(1);
        effectsImage[0].gameObject.SetActive(true);
        while (true)
        {
            healthManager.Health = healthManager.Health - (statEffects.PoisionDMG + statEffects.PoisionDIS * healthManager.Health / 100);
            n[0]++;
            while (statEffects.PoisionCD * n[0] > timersCoroutine[0])
            {
                timersCoroutine[0]++;
                yield return a;
            }
        }
    }
    private void PoisonStop()
    {
        effectsImage[0].gameObject.SetActive(true);
    }
    public void Weakness() => StartEffectCoroutine(() => WeaknessCoroutine(), 1, statEffects.WeaknessTime);

    private void WeaknessCoroutine()
    {
        effectsImage[1].gameObject.SetActive(true);
        StaticEffects.weaknessRate = statEffects.WeaknessRate;
    }
    private void WeaknessStop()
    {
        effectsImage[1].gameObject.SetActive(false);
        StaticEffects.weaknessRate = 1;
    }
    public void Slowness() => StartEffectCoroutine(() => SlownessCoroutine(), 2, statEffects.SlownessTime);


    private void SlownessCoroutine()
    {
        effectsImage[2].gameObject.SetActive(true);
        movementManager.walkSlowness = statEffects.SlownessRate / 100;
    }

    private void SlownessStop()
    {
        movementManager.walkSlowness = 1;
        effectsImage[2].gameObject.SetActive(false);
    }
    public void Hunger() => StartEffectCoroutine(HungerCoroutine(), 3, statEffects.HungerTime);
    private IEnumerator HungerCoroutine()
    {
        Debug.Log(2);
        effectsImage[3].gameObject.SetActive(true);
        WaitForSeconds a = new WaitForSeconds(1);
        while (true)
        {
            Debug.Log(3);
            satietyManager.SpendSatiety(statEffects.HungerRate);
            Debug.Log(4);
            n[3]++;
            while (statEffects.HungerCD * n[3] > timersCoroutine[3])
            {
                Debug.Log(6);
                timersCoroutine[3]++;
                yield return a;
                Debug.Log(7);
            }
            Debug.Log(8);
        }
    }
    private void HungerStop()
    {
        effectsImage[3].gameObject.SetActive(false);
    }
    public void Partialpenetration() => StartEffectCoroutine(() => PartialpenetrationCoroutine(), 4, statEffects.PenetrationTime);
    private void PartialpenetrationCoroutine()
    {
        healthManager.penetration = statEffects.PenetrationRate / 100;
        effectsImage[4].gameObject.SetActive(true);
    }
    private void PartialpenetrationStop()
    {
        healthManager.penetration = 1;
        effectsImage[4].gameObject.SetActive(false);
    }
    public void Burn() => StartEffectCoroutine(BurnCoroutine(), 5, statEffects.BurnTime);
    private IEnumerator BurnCoroutine()
    {
        int i = statisticsManager.HPLevel;
        float timer = statEffects.BurnCD;
        int n = 0;
        effectsImage[5].gameObject.SetActive(true);
        WaitForSeconds a = new WaitForSeconds(statEffects.BurnCD);
        while (true)
        {
            healthManager.Health -= statEffects.BurnDMG[i];
            n++;
            while (statEffects.PoisionCD * n > timer)
            {
                timer++;
                yield return a;
            }
        }
    }
    private void BurnStop()
    {
        effectsImage[5].gameObject.SetActive(false);
    }
    public void Blindness() => StartEffectCoroutine(() => BlindnessCoroutine(), 6, statEffects.BlindnessTime);
    private void BlindnessCoroutine()
    {
        effectsImage[6].gameObject.SetActive(true);
        blindess[0] = mainCamera[0].m_Lens.FarClipPlane;
        blindess[1] = mainCamera[1].m_Lens.FarClipPlane;
        blindess[2] = mainCamera[2].m_Lens.FarClipPlane;
        camera1.clearFlags = CameraClearFlags.SolidColor;
        RenderSettings.fog = true; 
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = 0.5f; 



        for (int i = 0; i < 3; i++)
        {
            mainCamera[i].m_Lens.FarClipPlane = statEffects.BlindnessRate;
        }
    }
    private void BlidnessStop()
    {
        if (!isa)
        {
            mainCamera[0].m_Lens.FarClipPlane = blindess[0];
            mainCamera[1].m_Lens.FarClipPlane = blindess[1];
            mainCamera[2].m_Lens.FarClipPlane = blindess[2];
            camera1.clearFlags = CameraClearFlags.Skybox;
            RenderSettings.fog = false;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogDensity = 0.5f;
        }
        effectsImage[6].gameObject.SetActive(false);
    }
    public void Cursed() => StartEffectCoroutine(() => CursedCoroutine(), 7, statEffects.CursedTime);
    private void CursedCoroutine()
    {
        healthManager.cursed = true;
        effectsImage[7].gameObject.SetActive(true);
    }
    private void CursedStop()
    {
        healthManager.cursed = false;
        effectsImage[7].gameObject.SetActive(false);
    }
    public void Hex() => StartEffectCoroutine(() => HexCoroutine(), 8, statEffects.HexTime);
    private void HexCoroutine()
    {
        healthManager.hex = statEffects.HexRate;
        effectsImage[8].gameObject.SetActive(true);
    }
    private void HexStop()
    {
        healthManager.hex = 1;
        effectsImage[8].gameObject.SetActive(false);
    }
    public void Regeneration() => StartEffectCoroutine(RegenerationCoroutine(), 9, statEffects.RegenerationTime);
    private IEnumerator RegenerationCoroutine()
    {
        Debug.Log(1);
        WaitForSeconds a = new WaitForSeconds(statEffects.RegenerationCD);
        float timer = 0;
        int n = 0;
        effectsImage[9].gameObject.SetActive(true);
        while (true)
        {
            healthManager.GetHealth(statEffects.Regeneration);
            n++;
            while (statEffects.RegenerationCD * n > timer)
            {
                timer++;
                yield return a;
            }
        }
    }
    private void RegenerationStop()
    {
        effectsImage[9].gameObject.SetActive(false);
    }
    public void Strength() => StartEffectCoroutine(() => StrengthCoroutine(), 10, statEffects.StrengthTime);

    private void StrengthCoroutine()
    {
        effectsImage[10].gameObject.SetActive(true);
        StaticEffects.strengthRate = statEffects.StrengthRate;
    }
    private void StrengthStop()
    {
        effectsImage[10].gameObject.SetActive(false);
        StaticEffects.strengthRate = statEffects.StrengthRate;
    }
    public void Speed() => StartEffectCoroutine(() => SpeedCoroutine(), 11, statEffects.SpeedTime);


    private void SpeedCoroutine()
    {
        effectsImage[11].gameObject.SetActive(true);
        movementManager.walkSpeed = statEffects.SpeedRate / 100;
    }
    private void SpeedStop()
    {
        effectsImage[11].gameObject.SetActive(false);
        movementManager.walkSpeed = 1;
    }

    public void VampirismHP() => StartEffectCoroutine(() => VampirisimHPCoroutine(), 12, statEffects.VampirismHPTime);

    private void VampirisimHPCoroutine()
    {
        StaticEffects.vampirismHP = true;
        effectsImage[12].gameObject.SetActive(true);
    }
    private void VampirismHPStop()
    {
        effectsImage[12].gameObject.SetActive(false);
        StaticEffects.vampirismHP = false;
    }

    public void VampirismHPLogic(int value)
    {
        healthManager.GetHealth(value * statEffects.VampirismHP / 100);
    }
    public void VampirismMana() => StartEffectCoroutine(() => VampirisimManaCoroutine(), 13, statEffects.VampirismManaTime);

    private void VampirisimManaCoroutine()
    {
        StaticEffects.vampirismMana = true;
        effectsImage[13].gameObject.SetActive(true);
    }
    private void VampirismManaStop()
    {
        effectsImage[13].gameObject.SetActive(false);
        StaticEffects.vampirismMana = false;
    }

    public void VampirismManaLogic(int value)
    {
        manaManager.ReplenishMana(value * statEffects.VampirismMana / 100);
    }
    public void Resistance() => StartEffectCoroutine(() => ResistanceCoroutine(), 14, statEffects.ResistanceTime);

    private void ResistanceCoroutine()
    {
        effectsImage[14].gameObject.SetActive(true);
        healthManager.resistance = statEffects.ResistanceRate / 100;
    }
    private void ResistanceStop()
    {
        healthManager.resistance = 1;
        effectsImage[14].gameObject.SetActive(false);
    }
    public void Shocks() => StartEffectCoroutine(() => ShocksCoroutine(), 15, statEffects.ShocksTime);

    private void ShocksCoroutine()
    {
        effectsImage[15].gameObject.SetActive(true);
        StaticEffects.shock = true;
    }
    private void ShocksStop()
    {
        StaticEffects.shock = false;
        effectsImage[15].gameObject.SetActive(false);
    }

    private class TimersSave
    {
        public int[] timers;
        public int[] n;
        public int[] timersCoroutine;
    }

}
