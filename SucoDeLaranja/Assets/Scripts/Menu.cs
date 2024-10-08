
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class Menu : MonoBehaviour {

    private Vector2Int _selectedLayout {
        get {
           return new Vector2Int(int.Parse(
                _xDropdown.options[_xDropdown.value].text),
                int.Parse(_yDropdown.options[_yDropdown.value].text)
                );

        }
    }

    [SerializeField] private Vector2Int layoutLimit;
    [SerializeField] private TMP_Dropdown _xDropdown;
    [SerializeField] private TMP_Dropdown _yDropdown;
    [SerializeField] private GameObject _selectLayoutPanel;
    [SerializeField] private Button _startGameButton;


    private void Start() {
        _xDropdown.options = new List<TMP_Dropdown.OptionData>();
        List<TMP_Dropdown.OptionData> xOptions = new List<TMP_Dropdown.OptionData>();
        for(int x = 2; x <= layoutLimit.x; x++) {
            xOptions.Add(new TMP_Dropdown.OptionData(x.ToString()));
        }
        _xDropdown.AddOptions(xOptions);

        _yDropdown.options = new List<TMP_Dropdown.OptionData>();
        List<TMP_Dropdown.OptionData> yOptions = new List<TMP_Dropdown.OptionData>();
        for(int y = 2; y <= layoutLimit.y; y++) {
            yOptions.Add(new TMP_Dropdown.OptionData(y.ToString()));
        }
        _yDropdown.AddOptions(yOptions);
        EvaluateDropdownOptions();
    }
    public void ClickNewGameButton() {
        _selectLayoutPanel.SetActive(true);
    }

    public void ClickLoadButton() {
        SucoDeLaranja.SceneManager.Instance.LoadGameScene((SceneManager.GetActiveScene().buildIndex + 1), true);
    }

    public void ClickStartGameButton() {
        SucoDeLaranja.SceneManager.Instance.LoadGameScene((SceneManager.GetActiveScene().buildIndex + 1), _selectedLayout);
    }

    public void EvaluateDropdownOptions() {
        Vector2Int layout = _selectedLayout;
        _startGameButton.interactable = layout.x*layout.y%2 == 0 && layout.x*layout.y >= 4;

    }
}
