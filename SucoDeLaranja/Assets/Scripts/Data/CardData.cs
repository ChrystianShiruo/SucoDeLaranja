using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "GameData/CardData")]
public class CardData : ScriptableObject {
    public int id {
        get {
            if(_id != -1) {
                return _id;
            }
            //trying to assign an id based on asset name (ex: 25 for asset CardData 25) if id was not manually assigned
            string s = new String(Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(this)).Where(Char.IsDigit).ToArray());
            int.TryParse(s, out _id);
            return _id;
        }
    }

    public Sprite sprite;
    public Color color;
    [Range(0, 1f)][Tooltip("how much of its allocated space the card will occupy, 1 means no padding between cards")] public float cellFill= .9f;

    [SerializeField] private int _id = -1;

}
