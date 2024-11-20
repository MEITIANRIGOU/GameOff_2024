using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "NewEvidence", menuName = "Evidence/Item")]
    public class EvidenceItem : ScriptableObject
    {
        public string evidenceName;
        [TextArea] public string evidenceDescription;
        public Sprite evidenceIcon;
    }
