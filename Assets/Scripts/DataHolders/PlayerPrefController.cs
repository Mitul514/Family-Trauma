using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerPrefController : MonoBehaviour
{
    public static PlayerPrefController Instance { get; private set; }
    private List<string> narrativeIdLists;
    private List<string> objectiveIdLists;

    public List<string> NarrativeIdLists => narrativeIdLists;
    public List<string> ObjectiveIdLists => objectiveIdLists;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetPlayerPrefToList(KeyConstants.OBJECTIVE_KEY);
        SetPlayerPrefToList(KeyConstants.NARRATIVE_KEY);
    }

    public void SetPlayerPrefToList(string key)
    {
        if (objectiveIdLists == null)
            objectiveIdLists = new List<string>();
        if (narrativeIdLists == null)
            narrativeIdLists = new List<string>();

        if (PlayerPrefs.HasKey(key))
        {
            string ids = PlayerPrefs.GetString(key);
            string[] idArray = ids.Split(',');

            for (int i = 0; i < idArray.Length; i++)
            {
                if (key == KeyConstants.NARRATIVE_KEY && !string.IsNullOrEmpty(idArray[i]))
                    narrativeIdLists.Add(idArray[i]);
                else if (key == KeyConstants.OBJECTIVE_KEY && !string.IsNullOrEmpty(idArray[i]))
                    objectiveIdLists.Add(idArray[i]);
            }
        }
    }

    public void UpdateObjectiveList(string val)
    {
        if (objectiveIdLists == null)
            objectiveIdLists = new List<string>();

        if (!objectiveIdLists.Contains(val))
        {
            objectiveIdLists.Add(val);
            SetListToPlayerPref(KeyConstants.OBJECTIVE_KEY);
        }
    }

    public void UpdateNarrativeList(string val)
    {
        if (narrativeIdLists == null)
            narrativeIdLists = new List<string>();

        if (!narrativeIdLists.Contains(val))
        {
            narrativeIdLists.Add(val);
            SetListToPlayerPref(KeyConstants.NARRATIVE_KEY);
        }
    }

    public void SetListToPlayerPref(string key)
    {
        if (key == KeyConstants.NARRATIVE_KEY)
        {
            UpdateStringBuilder(narrativeIdLists, out StringBuilder sb);
            PlayerPrefs.SetString(key, sb.ToString());
        }
        else if (key == KeyConstants.OBJECTIVE_KEY)
        {
            UpdateStringBuilder(objectiveIdLists, out StringBuilder sb);
            PlayerPrefs.SetString(key, sb.ToString());
        }
    }

    private void UpdateStringBuilder(List<string> idLists, out StringBuilder sb)
    {
        sb = new StringBuilder();
        if (idLists != null)
        {
            foreach (var id in idLists)
            {
                sb.Append(id);
                sb.Append(",");
            }
        }
    }

    public bool hasIdInListForKey(string key, string id)
    {
        if (key == KeyConstants.NARRATIVE_KEY && narrativeIdLists.Contains(id))
            return true;
        else if (key == KeyConstants.OBJECTIVE_KEY && objectiveIdLists.Contains(id))
            return true;

        return false;
    }

    public void UpdateTrustMeterKey(float val)
    {
        PlayerPrefs.SetFloat(KeyConstants.TRUSTMETER_KEY, val);
    }

    public float GetMeterValue()
	{
		if (PlayerPrefs.HasKey(KeyConstants.TRUSTMETER_KEY))
		{
            return PlayerPrefs.GetFloat(KeyConstants.TRUSTMETER_KEY);
		}
        return 0;
	}

    [ContextMenu("Check Player Pref values")]
    public void TestPlayerPrefValues()
    {
        Debug.Log($"{KeyConstants.NARRATIVE_KEY} : {PlayerPrefs.GetString(KeyConstants.NARRATIVE_KEY)}");
        Debug.Log($"{KeyConstants.OBJECTIVE_KEY} : {PlayerPrefs.GetString(KeyConstants.OBJECTIVE_KEY)}");
    }
}
